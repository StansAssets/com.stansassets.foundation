namespace Models
{
    /// <summary>
    /// Result model.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Convert to the result json string.
        /// </summary>
        string ToJson();

        /// <summary>
        /// Gets the result error object. If Error message is empty,
        /// result is succeeded.
        /// </summary>
        IError Error { get; }

        /// <summary>
        /// Gets a value indicating whether this this result has error.
        /// </summary>
        /// <value><c>true</c> if has error; otherwise, <c>false</c>.</value>
        bool HasError { get; }

        /// <summary>
        /// Gets a value indicating whether this result is succeeded.
        /// </summary>
        /// <value><c>true</c> if is succeeded; otherwise, <c>false</c>.</value>
        bool IsSucceeded { get; }

        /// <summary>
        /// Gets a value indicating whether this result is failed.
        /// </summary>
        /// <value><c>true</c> if is failed; otherwise, <c>false</c>.</value>
        bool IsFailed { get; }
    }
}
