using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CodeTunnel.Domain.BindingFilters;
using CodeTunnel.MvcUtilities.Validators;

namespace CodeTunnel.Domain.Entities
{
    /// <summary>
    /// Post comment extension class.
    /// </summary>
    [MetadataType(typeof(PostCommentMetadata))]
    public partial class PostComment : INewPostComment
    {
        partial void OnWebsiteChanged()
        {
            if (!string.IsNullOrEmpty(this.Website) && !this.Website.ToUpper().StartsWith("HTTP://"))
                this.Website = "http://" + this.Website;
        }
    }

    /// <summary>
    /// Post comment metadata for validation.
    /// </summary>
    public class PostCommentMetadata
    {
        /// <summary>
        /// The author is required.
        /// The author name must be between 3 and 100 characters.
        /// </summary>
        [Required(ErrorMessage="Author is required.")]
        [StringLength(100, MinimumLength=3, ErrorMessage="Author must be between 3 and 100 characters.")]
        public string Author;

        /// <summary>
        /// Email is required.
        /// Email must adhere to a valid email format.
        /// </summary>
        [Required(ErrorMessage="Email is required.")]
        [Email(ErrorMessage="Invalid email supplied.")]
        public string Email;

        /// <summary>
        /// Url is not required but must adhere to a valid URL format if supplied.
        /// </summary>
        [Url(ErrorMessage="Invalid website URL supplied.")]
        public string Website;

        /// <summary>
        /// Comment body is required.
        /// Comment must be between 10 and 4000 characters.
        /// </summary>
        [Required(ErrorMessage="Comment is required.")]
        [StringLength(4000, MinimumLength=10, ErrorMessage="Comment must be between 10 and 4000 characters.")]
        public string Body;
    }
}
