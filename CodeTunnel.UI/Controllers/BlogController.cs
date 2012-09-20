using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using CodeTunnel.Domain.Entities;
using CodeTunnel.Domain.Repositories;
using CodeTunnel.Domain.Interfaces;
using CodeTunnel.UI.Models;
using CodeTunnel.MvcUtilities.Attributes;
using CodeTunnel.Domain.BindingFilters;
using System.Configuration;
using CodeTunnel.MvcUtilities.Objects;
using CodeTunnel.MvcUtilities.ExtensionMethods;
using System.Text.RegularExpressions;

namespace CodeTunnel.UI.Controllers
{
    /// <summary>
    /// Controller for handling blog related functions.
    /// </summary>
    [ValidateInput(false)]
    public class BlogController : BaseController
    {
        #region Dependencies

        /// <summary>
        /// Repository for retrieving and persisting blog posts.
        /// </summary>
        private IBlogPostRepository _blogPostRepository;

        /// <summary>
        /// Repository for retrieving and persisting post comments.
        /// </summary>
        private IPostCommentRepository _postCommentRepository;

        /// <summary>
        /// Repository for retrieving and persisting users.
        /// </summary>
        private IUserRepository _userRepository;

        /// <summary>
        /// Repository for retrieving and persisting variables.
        /// </summary>
        private IVariableRepository _variableRepository;

        #endregion

        #region Fields

        private int _blogPostsPerPage;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor that accepts dependencies.
        /// </summary>
        public BlogController(
            IBlogPostRepository blogPostRepository,
            IPostCommentRepository postCommentRepository,
            IUserRepository userRepository,
            IVariableRepository variableRepository)
        {
            this._blogPostRepository = blogPostRepository;
            this._postCommentRepository = postCommentRepository;
            this._userRepository = userRepository;
            this._variableRepository = variableRepository;
            this._blogPostsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["BlogPostsPerPage"]);
        }

        #endregion

        #region Actions

        /// <summary>
        /// Displays the blog post index.
        /// </summary>
        /// <param name="page">The page number to view.</param>
        public ViewResult Index(int page)
        {
            var blogPostsPage = _blogPostRepository.GetBlogPosts(page, _blogPostsPerPage);
            var viewModel = new BlogIndexViewModel
            {
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    TotalItems = blogPostsPage.TotalItems,
                    ItemsPerPage = _blogPostsPerPage
                },
                BlogPosts = blogPostsPage.Items
            };
            return View(viewModel);
        }

        /// <summary>
        /// Displays a list of search results.
        /// </summary>
        /// <param name="query">The query to use in the search.</param>
        /// <param name="page">The page number to view.</param>
        public ViewResult Search(string query, int page)
        {
            var blogPostsPage = _blogPostRepository.SearchBlogPosts(query, page, _blogPostsPerPage);
            var blogPosts = new List<BlogPost>();
            foreach (BlogPost blogPost in blogPostsPage.Items)
            {
                blogPost.Body = Server.HtmlEncode(blogPost.Body).Replace(query, string.Format("<span class='highlight'>{0}</span>", query));
                blogPost.Author = Server.HtmlEncode(blogPost.Author).Replace(query, string.Format("<span class='highlight'>{0}</span>", query));
                blogPosts.Add(blogPost);
            }
            var viewModel = new SearchViewModel
            {
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    TotalItems = blogPostsPage.TotalItems,
                    ItemsPerPage = _blogPostsPerPage
                },
                BlogPosts = blogPosts,
                Query = query
            };
            return View(viewModel);
        }

        /// <summary>
        /// Displays a single blog post.
        /// </summary>
        /// <param name="Id">The ID of the blog post.</param>
        /// <param name="postTitle">The friendly-url version of the blog title.</param>
        public ActionResult Post(int Id, string postTitle)
        {
            var blogPost = _blogPostRepository.GetBlogPost(Id);

            // Create friendly URL from blog post title and compare it to the friendly URL that was passed in.
            // If they do not match then redirect to the same action but with the correct title in the URL.
            string friendlyUrl = MvcUtilities.Utilities.CommonUtils.CreateFriendlyUrl(blogPost.Title);
            if (postTitle != friendlyUrl)
                return RedirectToAction("Post", new { Id = Id, postTitle = friendlyUrl });

            // Create a new post comment and pre-populate it with the current blog post ID, and if the user is
            // logged in then populate it with values from their profile to reduce typing when logged in users
            // comment on blog posts.
            PostComment newComment = new PostComment { PostID = blogPost.PostID };
            if (User.Identity.IsAuthenticated)
            {
                User currentUser = _userRepository.GetUser(User.Identity.Name);
                newComment.Author = currentUser.Username;
                newComment.Email = currentUser.Email;
                newComment.Website = currentUser.Website;
            }

            // Construct a view model and attach the current blog post and post comment.
            var viewModel = new BlogPostViewModel
            {
                BlogPost = blogPost,
                NewComment = newComment
            };

            // Render the "Post" view.
            return View(viewModel);
        }

        /// <summary>
        /// Allows the logged in user to remove a comment from a blog post.
        /// </summary>
        /// <param name="Id">The ID of the post comment to be removed.</param>
        [Authorize]
        public ActionResult DeleteComment(int Id)
        {
            var postComment = _postCommentRepository.GetPostComment(Id);
            BlogPost blogPost = postComment.BlogPost;
            _postCommentRepository.DeletePostComment(postComment);
            _postCommentRepository.SaveChanges();
            if (!Request.IsAjaxRequest())
                return RedirectToAction("Post", new { Id = blogPost.PostID });
            else
                return null;
        }

        /// <summary>
        /// Allows a visitor to post a comment on a blog post.
        /// Verifies values with Akismet service to reduce spam.
        /// </summary>
        /// <param name="formCollection">The collection of form values from the request.</param>
        [HttpPost]
        [AkismetCheck("Author", "Email", "Website", "Body")]
        public ActionResult AddComment(FormCollection formCollection)
        {
            PostComment newComment = new PostComment();
            TryUpdateModel<INewPostComment>(newComment, formCollection);
            newComment.PostedDate = DateTime.UtcNow;
            newComment.IPAddress = Request.UserHostAddress;
            if (ModelState.IsValid)
            {
                newComment.Body = Server.HtmlEncode(newComment.Body);
                _postCommentRepository.AddPostComment(newComment);
                _postCommentRepository.SaveChanges();

                if (!User.Identity.IsAuthenticated || (User.Identity.IsAuthenticated && _userRepository.GetUser(User.Identity.Name).Email.ToUpper() != newComment.Email.ToUpper()))
                {
                    try
                    {
                        SmtpClient smtpClient = new SmtpClient();
                        MailMessage notificationEmail = new MailMessage
                        {
                            IsBodyHtml = true,
                            Subject = string.Format("{0} posted a comment on CodeTunnel!", newComment.Author),
                            Body = string.Format(
                                "{0} posted a comment on your blog post titled <a href='{2}'>{1}</a>.<br /><br />{3}",
                                newComment.Author,
                                newComment.BlogPost.Title,
                                ConfigurationManager.AppSettings["FullUrl"] + Url.Action("Post", new { Id = newComment.PostID }),
                                newComment.Body)
                        };
                        notificationEmail.To.Add(newComment.BlogPost.User.Email);
                        smtpClient.Send(notificationEmail);
                    }
                    catch { }
                }

                if (Request.IsAjaxRequest())
                    return Json(new
                    {
                        Valid = true,
                        Html = MvcUtilities.Utilities.CommonUtils.RenderViewToString(this, "PostComments", newComment.BlogPost.PostComments.OrderBy(x => x.PostedDate), true)
                    });
                else
                    return RedirectToAction("Post", new { Id = newComment.PostID });
            }
            else
            {
                if (ModelState.ContainsKey("spam"))
                {
                    Variable spamCaught = this._variableRepository.GetVariable("spamCaught");
                    bool addNew = (spamCaught == null);
                    int spamCount = 0;
                    if (addNew)
                        spamCaught = new Variable { Name = "spamCaught" };
                    else
                        spamCount = int.Parse(spamCaught.Value);
                    spamCount++;
                    spamCaught.Value = spamCount.ToString();
                    if (addNew)
                        _variableRepository.AddVariable(spamCaught);
                    _variableRepository.SaveChanges();
                }

                if (!Request.IsAjaxRequest())
                {
                    var blogPost = _blogPostRepository.GetBlogPost(newComment.PostID);
                    var viewModel = new BlogPostViewModel
                    {
                        BlogPost = blogPost,
                        NewComment = newComment
                    };
                    return View("Post", viewModel);
                }
                else
                {
                    return Json(new
                    {
                        Valid = false,
                        Html = MvcUtilities.Utilities.CommonUtils.RenderViewToString(this, "NewComment", newComment, true)
                    });
                }
            }
        }

        /// <summary>
        /// Quick way to format Markdown text.
        /// </summary>
        [ValidateInput(false)]
        public string FormatMarkdown(string id)
        {
            return MvcUtilities.Utilities.MarkdownUtils.FormatMarkdown(id, true);
        }

        /// <summary>
        /// Takes the logged in user to the new blog post form.
        /// </summary>
        [Authorize]
        [HttpGet]
        public ViewResult NewPost()
        {
            ViewBag.FormTitle = "New Post";
            return View("ManagePost");
        }

        /// <summary>
        /// Allows the logged in user to create a new blog post.
        /// </summary>
        /// <param name="formCollection">The collection of form values from the request.</param>
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewPost(FormCollection formCollection)
        {
            BlogPost blogPost = new BlogPost();
            TryUpdateModel<INewBlogPost>(blogPost, formCollection);
            blogPost.PostedDate = DateTime.UtcNow;
            blogPost.Author = User.Identity.Name;
            if (!ModelState.IsValid)
            {
                ViewBag.FormTitle = "New Post";
                return View("ManagePost");
            }
            _blogPostRepository.AddBlogPost(blogPost);
            _blogPostRepository.SaveChanges();
            return RedirectToAction("Post", new { Id = blogPost.PostID });
        }

        /// <summary>
        /// Allows the logged in user to edit an existing blog post.
        /// </summary>
        /// <param name="Id">The ID of the blog post to edit.</param>
        [Authorize]
        [HttpGet]
        public ViewResult EditPost(int Id)
        {
            var blogPost = _blogPostRepository.GetBlogPost(Id);
            ViewBag.FormTitle = "Edit Post";
            return View("ManagePost", blogPost);
        }

        /// <summary>
        /// Submits changes after editing an existing blog post.
        /// </summary>
        /// <param name="Id">The ID of the blog post to edit.</param>
        /// <param name="formCollection">The collection of form values from the request.</param>
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPost(int Id, FormCollection formCollection)
        {
            var blogPost = _blogPostRepository.GetBlogPost(Id);
            blogPost.EditedDate = DateTime.UtcNow;
            TryUpdateModel<INewBlogPost>(blogPost, formCollection);
            if (!ModelState.IsValid)
            {
                ViewBag.FormTitle = "Edit Post";
                return View("ManagePost");
            }
            _blogPostRepository.SaveChanges();
            return RedirectToAction("Post", new { Id = blogPost.PostID });
        }

        /// <summary>
        /// Allows the logged in user to delete an existing blog post.
        /// </summary>
        /// <param name="Id">The ID of the blog post to be deleted.</param>
        [Authorize]
        public RedirectToRouteResult DeletePost(int Id)
        {
            var blogPost = _blogPostRepository.GetBlogPost(Id);
            _blogPostRepository.DeleteBlogPost(blogPost);
            _blogPostRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
