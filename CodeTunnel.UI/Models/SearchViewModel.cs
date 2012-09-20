using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeTunnel.Domain.Entities;
using CodeTunnel.MvcUtilities.Objects;

namespace CodeTunnel.UI.Models
{
    /// <summary>
    /// View model for the blog index view.
    /// </summary>
    public class SearchViewModel
    {
        /// <summary>
        /// Information regarding page navigation on the blog.
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

        /// <summary>
        /// A collection of BlogPosts to display on the page.
        /// </summary>
        public IEnumerable<BlogPost> BlogPosts { get; set; }

        /// <summary>
        /// The query used when searching.
        /// </summary>
        public string Query { get; set; }
    }
}