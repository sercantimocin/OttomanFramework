namespace Ottoman.CrossCutting.Additional
{
    using System.Net;

    /// <summary>
    /// The ottoman response.
    /// </summary>
    public class OttomanResponse 
    {
        public OttomanResponse(HttpStatusCode statusCode, params string[] message)
        {
            this.StatusCode = (int)statusCode;
            this.Message = message;
        }

        public OttomanResponse(string version, HttpStatusCode statusCode, params string[] message) : this(statusCode, message)
        {
            this.Version = version;
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public string Version { get; protected set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string[] Message { get; set; }
    }

    /// <summary>
    /// The ottoman response.
    /// </summary>
    public class OttomanResponse<T> : OttomanResponse
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
        public OttomanResponse(HttpStatusCode statusCode, T result, params string[] message) : base(statusCode, message)
        {
            this.Result = result;
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
        public OttomanResponse(string version, HttpStatusCode statusCode, T result, params string[] message) : base(version, statusCode, message)
        {
            this.Result = result;
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public T Result { get; set; }
    }
}
