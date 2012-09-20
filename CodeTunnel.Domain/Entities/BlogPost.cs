using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CodeTunnel.Domain.BindingFilters;

namespace CodeTunnel.Domain.Entities
{
    /// <summary>
    /// Blog post entity extension class.
    /// </summary>
    [MetadataType(typeof(BlogPostMetadata))]
    public partial class BlogPost : INewBlogPost
    {
    }

    /// <summary>
    /// Blog post metadata for validation attributes.
    /// </summary>
    public class BlogPostMetadata
    {
        /// <summary>
        /// The title is required.
        /// </summary>
        [Required]
        public string Title;

        /// <summary>
        /// The body is required.
        /// </summary>
        [Required]
        public string Body;
    }
}
