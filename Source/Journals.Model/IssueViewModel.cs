using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Medico.Model
{
    /// <summary>
    /// Issue view entity model
    /// </summary>
    public class IssueViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the journal.
        /// </summary>
        /// <value>
        /// The journal.
        /// </value>
        [ForeignKey("JournalId")]
        public Journal Journal { get; set; }

        /// <summary>
        /// Gets or sets the journal identifier.
        /// </summary>
        /// <value>
        /// The journal identifier.
        /// </value>
        public int JournalId { get; set; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public byte[] Content { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        [Required, ValidateFile]
        public HttpPostedFileBase File { get; set; }
    }
}