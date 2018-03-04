// <copyright file="Invoice.cs" company="Johannes Ebner">
// Copyright (c) Johannes Ebner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root or https://spdx.org/licenses/MIT.html for full license information.
// </copyright>

using System;

namespace Claimini.Api.Repository
{
    /// <summary>
    /// Exception to be used if an entry has a duplicate within a repository
    /// </summary>
    public class DuplicateEntryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEntryException"/> class.
        /// </summary>
        public DuplicateEntryException()
        {
            this.KeyName = string.Empty;
            this.KeyValue = default(object);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEntryException"/> class.
        /// </summary>
        /// <param name="message">The exception message</param>
        public DuplicateEntryException(string message)
            : base(message)
        {
            this.KeyName = string.Empty;
            this.KeyValue = default(object);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEntryException"/> class.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception thrown</param>
        public DuplicateEntryException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.KeyName = string.Empty;
            this.KeyValue = default(object);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEntryException"/> class.
        /// </summary>
        /// <param name="keyName">The data type of the key that was used to look up the entry</param>
        /// <param name="keyValue">The value of the key that was used to look up the entry</param>
        public DuplicateEntryException(string keyName, object keyValue)
            : base($"The entry was not found. Key: {keyName}={keyValue.ToString()}")
        {
            this.KeyName = keyName;
            this.KeyValue = keyValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEntryException"/> class.
        /// </summary>
        /// <param name="keyName">The data type of the key that was used to look up the entry</param>
        /// <param name="keyValue">The value of the key that was used to look up the entry</param>
        /// <param name="message">The exception message</param>
        public DuplicateEntryException(string keyName, object keyValue, string message)
            : base(message)
        {
            this.KeyName = keyName;
            this.KeyValue = keyValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEntryException"/> class.
        /// </summary>
        /// <param name="keyName">The data type of the key that was used to look up the entry</param>
        /// <param name="keyValue">The value of the key that was used to look up the entry</param>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception thrown</param>
        public DuplicateEntryException(string keyName, object keyValue, string message, Exception innerException)
            : base(message, innerException)
        {
            this.KeyName = keyName;
            this.KeyValue = keyValue;
        }

        /// <summary>
        /// Gets the data type of the key that was used to look up the entry
        /// </summary>
        public string KeyName { get; private set; }

        /// <summary>
        /// Gets the value of the key that was used to look up the entry
        /// </summary>
        public object KeyValue { get; private set; }
    }
}
