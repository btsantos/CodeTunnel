using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeTunnel.Domain.Entities;

namespace CodeTunnel.Domain.Interfaces
{
    /// <summary>
    /// Repository for creating, reading, updating, and deleting post comments.
    /// </summary>
    public interface IPostCommentRepository
    {
        /// <summary>
        /// Retrieves a post comment by ID.
        /// </summary>
        /// <param name="Id">The unique ID of the post comment to retrieve.</param>
        /// <returns>A post comment entity.</returns>
        PostComment GetPostComment(int Id);

        /// <summary>
        /// Adds a post comment entity to the repository.
        /// </summary>
        /// <param name="postComment">The post comment to be added.</param>
        void AddPostComment(PostComment postComment);

        /// <summary>
        /// Deletes a post comment entity from the repository.
        /// </summary>
        /// <param name="postComment">The post comment to be deleted.</param>
        void DeletePostComment(PostComment postComment);

        /// <summary>
        /// Persists changes in the repository to the data store.
        /// </summary>
        void SaveChanges();
    }
}
