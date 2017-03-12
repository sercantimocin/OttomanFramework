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
            this.StatusCode = (int)statusCode;
            this.Result = result;
            this.Message = message;
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public string Version
        {
            get
            {
                return "1.2.3";
            }
        }

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
