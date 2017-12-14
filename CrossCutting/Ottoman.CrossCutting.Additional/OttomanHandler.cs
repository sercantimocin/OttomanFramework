namespace Ottoman.CrossCutting.Additional
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    /// <summary>
    /// The ottoman handler.
    /// </summary>
    public class OttomanHandler : DelegatingHandler
    {
        /// <summary>
        /// The version.
        /// </summary>
        private static string version;

        /// <summary>
        /// Initializes a new instance of the <see cref="OttomanHandler"/> class.
        /// </summary>
        /// <param name="versionNumber">
        /// The version number.
        /// </param>
        public OttomanHandler(string versionNumber)
        {
            version = versionNumber;
        }

        /// <summary>
        /// The send async.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            return this.BuildApiResponse(request, response);
        }

        /// <summary>
        /// The build api response.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        private HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            string message = null;

            object content = this.ParseContentErrors(response, ref message);
            var newResponse = this.CreateNewResponseObject(request, response, content, message);

            foreach (var header in response.Headers)
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }

            return newResponse;
        }

        private object ParseContentErrors(HttpResponseMessage response, ref string message)
        {
            object content;

            if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode)
            {
                HttpError error = content as HttpError;

                if (error != null)
                {
                    content = null;
                    message = error.Message;
                    message = string.Concat(message, error.ExceptionMessage, error.StackTrace);

                    //TODO Logging
                }
            }

            return content;
        }

        private HttpResponseMessage CreateNewResponseObject(HttpRequestMessage request, HttpResponseMessage response, object content, string message)
        {
            Type responseType;

            if (content == null)
            {
                responseType = typeof(OttomanResponse);
            }
            else
            {
                responseType = typeof(OttomanResponse<>);
                responseType = responseType.MakeGenericType(content.GetType());
            }

            var ottomanResponse = Activator.CreateInstance(responseType, version, response.StatusCode, content, message);
            return request.CreateResponse(response.StatusCode, ottomanResponse);
        }
    }
}
