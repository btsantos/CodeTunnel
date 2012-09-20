using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeTunnel.Domain.Interfaces;
using CodeTunnel.Domain.Entities;
using CodeTunnel.Domain.Entities;

namespace CodeTunnel.Domain.Repositories
{
    /// <summary>
    /// Repository for creating, reading, updating, and deleting post comments.
    /// </summary>
    public class PostCommentRepo : IPostCommentRepository
    {
        /// <summary>
        /// The Entity Framework data context.
        /// </summary>
        CTContext _dataContext;

        /// <summary>
        /// Constructor that accepts a data context as a dependency.
        /// </summary>
        public PostCommentRepo(CTContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Retrieves a post comment by ID.
        /// </summary>
        /// <param name="Id">The unique ID of the post comment to retrieve.</param>
        /// <returns>A post comment entity.</returns>
        public PostComment GetPostComment(int Id)
        {
            var result = _dataContext.PostComments
                .SingleOrDefault(x => x.CommentID == Id);
            return result;
        }

        /// <summary>
        /// Adds a post comment entity to the repository.
        /// </summary>
        /// <param name="postComment">The post comment to be added.</param>
        public void AddPostComment(PostComment postComment)
        {
            _dataContext.PostComments.AddObject(postComment);
        }

        /// <summary>
        /// Deletes a post comment entity from the repository.
        /// </summary>
        /// <param name="postComment">The post comment to be deleted.</param>
        public void DeletePostComment(PostComment postComment)
        {
            _dataContext.PostComments.DeleteObject(postComment);
        }

        /// <summary>
        /// Persists changes in the repository to the data store.
        /// </summary>
        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }
    }
}
