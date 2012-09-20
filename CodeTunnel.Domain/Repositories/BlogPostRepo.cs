using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeTunnel.Domain.Interfaces;
using CodeTunnel.Domain.Entities;
using CodeTunnel.MvcUtilities.Objects;
using System.Linq.Expressions;

namespace CodeTunnel.Domain.Repositories
{
    /// <summary>
    /// Repository for creating, reading, updating, and deleting blog posts.
    /// </summary>
    public class BlogPostRepo : IBlogPostRepository
    {
        /// <summary>
        /// The Entity Framework data context.
        /// </summary>
        CTContext _dataContext;

        /// <summary>
        /// Constructor that accepts a data context as a dependency.
        /// </summary>
        public BlogPostRepo(CTContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Retrieves a blog post by its ID.
        /// </summary>
        /// <param name="Id">The unique ID of the blog post to retrieve.</param>
        /// <returns>A blog post entity.</returns>
        public BlogPost GetBlogPost(int Id)
        {
            var result = _dataContext.BlogPosts
                .SingleOrDefault(x => x.PostID == Id);
            return result;
        }

        /// <summary>
        /// Retrieves a collection of blog posts by page.
        /// </summary>
        /// <param name="page">The desired page.</param>
        /// <param name="itemsPerPage">The number of blog posts to display per page.</param>
        /// <returns>A collection of blog posts.</returns>
        public CollectionPage<BlogPost> GetBlogPosts(int page, int itemsPerPage)
        {
            var result = _dataContext.BlogPosts
                .OrderByDescending(x => x.PostedDate)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage);
            var collectionPage = new CollectionPage<BlogPost>
            {
                Items = result,
                TotalItems = _dataContext.BlogPosts.Count()
            };
            return collectionPage;
        }

        /// <summary>
        /// Retrieves a collection of blog posts related to the supplied search term.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="page">The desired page.</param>
        /// <param name="itemsPerPage">The number of blog posts to display per page.</param>
        /// <returns>A collection of blog posts.</returns>
        public CollectionPage<BlogPost> SearchBlogPosts(string query, int page, int itemsPerPage)
        {
            var result = _dataContext.BlogPosts
                .Where(BlogPostContains(query));
            var collectionPage = new CollectionPage<BlogPost>
            {
                Items = result
                    .OrderByDescending(x => x.PostedDate)
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage),
                TotalItems = result.Count()
            };
            return collectionPage;
        }

        /// <summary>
        /// Adds a new blog post entity to the repository.
        /// </summary>
        /// <param name="blogPost">The blog post to be added.</param>
        public void AddBlogPost(BlogPost blogPost)
        {
            _dataContext.BlogPosts.AddObject(blogPost);
        }

        /// <summary>
        /// Deletes a blog post entity from the repository.
        /// </summary>
        /// <param name="blogPost">The blog post to be deleted.</param>
        public void DeleteBlogPost(BlogPost blogPost)
        {
            _dataContext.BlogPosts.DeleteObject(blogPost);
        }

        /// <summary>
        /// Persist changes in the repository to the data store.
        /// </summary>
        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }

        /// <summary>
        /// Function to check various text fields of a blog post for a specified query.
        /// </summary>
        /// <param name="query">The text to search for.</param>
        /// <returns>A function to be used in place of a lambda.</returns>
        private Expression<Func<BlogPost, bool>> BlogPostContains(string query)
        {
            return x => x.Title.Contains(query) || x.Body.Contains(query) || x.Author.Contains(query);
        }
    }
}
