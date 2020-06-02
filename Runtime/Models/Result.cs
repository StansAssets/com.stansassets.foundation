using System;
using UnityEngine;

namespace Models
{
    /// <inheritdoc />
    [Serializable]
    public class Result : IResult
    {
        [SerializeField]
        protected Error m_Error;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        public Result() { }

        public Result(Result result)
        {
            m_Error = result.m_Error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class with predefined error
        /// </summary>
        /// <param name="error">A predefined result error object.</param>
        public Result(Error error)
        {
            SetError(error);
        }

        /// <summary>
        /// Sets result error.
        /// </summary>
        /// <param name="error">Error object.</param>
        public void SetError(Error error)
        {
            m_Error = error;
        }

        public IError Error => m_Error;
        public bool HasError => m_Error != null && (!string.IsNullOrEmpty(m_Error.Message) || Error.Code != default(int));

        public bool IsSucceeded => !HasError;

        public bool IsFailed => HasError;

        /// <summary>
        /// Convert Result to JSON string. Uses <see cref="JsonUtility"/>.
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
