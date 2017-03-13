namespace Ottoman.CrossCutting.Additional
{
    using System.Net;

    /// <summary>
    /// The ottoman response.
    /// </summary>
    public class OttomanResponse<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OttomanResponse{T}"/> class.
        /// </summary>
        /// <param name="statusCode">
        /// The status code.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public OttomanResponse(HttpStatusCode statusCode, T result, params string[] message)
        {
            if (result == null)
            {
                this.Result = default(T);
            }
            else
            {
                this.Result = result;
            }

            this.StatusCode = (int)statusCode;
            this.Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OttomanResponse{T}"/> class.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <param name="statusCode">
        /// The status code.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public OttomanResponse(string version, HttpStatusCode statusCode, T result, params string[] message) : this(statusCode, result, message)
        {
            this.Version = version;
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string[] Message { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public T Result { get; set; }
    }
}
