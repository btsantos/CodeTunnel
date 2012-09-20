using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeTunnel.Domain.BindingFilters
{
    /// <summary>
    /// Binding filter for new post comments.
    /// </summary>
    public interface INewPostComment
    {
        /// <summary>
        /// The author of the comment.
        /// </summary>
        string Author { get; set; }

        /// <summary>
        /// The website for the visitor posting the comment.
        /// </summary>
        string Website { get; set; }

        /// <summary>
        /// The email of the visitor.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// The body of the new comment.
        /// </summary>
        string Body { get; set; }

        /// <summary>
        /// The ID of the blog post the comment is associated with.
        /// </summary>
        int PostID { get; set; }
    }
}
