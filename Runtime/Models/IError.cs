namespace Models
{
    /// <summary>
    /// Error model.
    /// </summary>
    public interface IError
    {
        /// <summary>
        /// Error code.
        /// </summary>
        int Code { get; }

        /// <summary>
        /// Error description.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Formatted message that combines <see cref="Code"/> and <see cref="Message"/>.
        /// </summary>
        string FullMessage { get; }
    }
}
