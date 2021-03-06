﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medico.Model
{
    /// <summary>
    /// Issue entity class.
    /// </summary>
    public class Issue
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
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

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
    }
}