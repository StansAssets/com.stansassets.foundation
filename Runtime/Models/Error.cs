using System;
using UnityEngine;

namespace Models
{
    /// <inheritdoc />
    [Serializable]
    public class Error : IError
    {
        [SerializeField]
        int m_Code;
        [SerializeField]
        string m_Message;

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class,
        /// with predefined <see cref="Code"/> and <see cref="Message"/>.
        /// </summary>
        /// <param name="code">Error code.</param>
        public Error(int code)
            : this(code, string.Empty) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class,
        /// with predefined <see cref="Code"/> and <see cref="Message"/>.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <param name="message">Error Message.</param>
        public Error(int code, string message)
        {
            m_Code = code;
            m_Message = message;
        }

        //--------------------------------------
        // Get / Set
        //--------------------------------------

        public int Code => m_Code;
        public string Message => m_Message;

        public string FullMessage => Message.StartsWith(Code.ToString())
            ? Message
            : $"{Code}::{Message}";
    }
}
