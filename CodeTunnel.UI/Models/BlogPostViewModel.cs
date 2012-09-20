using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeTunnel.Domain.Entities;

namespace CodeTunnel.UI.Models
{
    /// <summary>
    /// View model for the blog post view.
    /// </summary>
    public class BlogPostViewModel
    {
        /// <summary>
        /// The blog post to be displayed.
        /// </summary>
        public BlogPost BlogPost { get; set; }

        /// <summary>
        /// The values to populate the comment form with.
        /// </summary>
        public PostComment NewComment { get; set; }
    }
}