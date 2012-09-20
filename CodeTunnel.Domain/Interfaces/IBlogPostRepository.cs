using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeTunnel.Domain.Entities;
using CodeTunnel.MvcUtilities.Objects;

namespace CodeTunnel.Domain.Interfaces
{
    /// <summary>
    /// Repository for creating, reading, updating, and deleting blog posts.
    /// </summary>
    public interface IBlogPostRepository
    {
        /// <summary>
        /// Retrieves a blog post by its ID.
        /// </summary>
        /// <param name="Id">The unique ID of the blog post to retrieve.</param>
        /// <returns>A blog post entity.</returns>
        BlogPost GetBlogPost(int Id);

        /// <summary>
        /// Retrieves a collection of blog posts by page.
        /// </summary>
        /// <param name="page">The desired page.</param>
        /// <param name="itemsPerPage">The number of blog posts to display per page.</param>
        /// <returns>A collection of blog posts.</returns>
        CollectionPage<BlogPost> GetBlogPosts(int page, int itemsPerPage);

        /// <summary>
        /// Retrieves a collection of blog posts related to the supplied search term.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="page">The desired page.</param>
        /// <param name="itemsPerPage">The number of blog posts to display per page.</param>
        /// <returns>A collection of blog posts.</returns>
        CollectionPage<BlogPost> SearchBlogPosts(string query, int page, int itemsPerPage);

        /// <summary>
        /// Adds a new blog post entity to the repository.
        /// </summary>
        /// <param name="blogPost">The blog post to be added.</param>
        void AddBlogPost(BlogPost blogPost);

        /// <summary>
        /// Deletes a blog post entity from the repository.
        /// </summary>
        /// <param name="blogPost">The blog post to be deleted.</param>
        void DeleteBlogPost(BlogPost blogPost);

        /// <summary>
        /// Persist changes in the repository to the data store.
        /// </summary>
        void SaveChanges();
    }
}
