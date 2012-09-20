using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeTunnel.Domain.BindingFilters
{
    /// <summary>
    /// Binding filter for new blog posts.
    /// </summary>
    public interface INewBlogPost
    {
        /// <summary>
        /// The title of the blog post.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// The body of the blog post.
        /// </summary>
        string Body { get; set; }
    }
}
