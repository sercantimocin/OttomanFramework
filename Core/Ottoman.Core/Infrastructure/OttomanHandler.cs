﻿namespace Ottoman.Core.Infrastructure
{
    using System;
    using System.Linq;
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

        private readonly bool _useModelStateError;

        /// <summary>
        /// Initializes a new instance of the <see cref="OttomanHandler"/> class.
        /// </summary>
        /// <param name="versionNumber">
        /// The version number.
        /// </param>
        public OttomanHandler(string versionNumber, bool useModelStateError = false)
        {
            version = versionNumber;
            _useModelStateError = useModelStateError;
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
            HttpResponseMessage newResponse = this.GetResponse(request, response);

            foreach (var header in response.Headers)
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }

            return newResponse;
        }

        private HttpResponseMessage GetResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            HttpResponseMessage responseMessage;
            object content;

            // TODO Refactor this logic
            if (response.TryGetContentValue<object>(out content))
            {
                HttpError error = content as HttpError;
                if (error == null)
                {
                    responseMessage = this.CreateNewResponseObject(request, response, content, null);
                }
                else
                {
                    if (_useModelStateError && error.ModelState != null)
                    {
                        responseMessage = this.CreateNewResponseObject(request, response, error.ModelState.Select(x => (x.Value as string[])[0]).ToArray());
                    }
                    else
                    {
                        string message = error.Message;
                        //string message = string.Concat(error.Message, " ", error.ExceptionType, " ", error.ExceptionMessage, " ", error.StackTrace);
                        responseMessage = this.CreateNewResponseObject(request, response, message);
                        //TODO Logging
                    }
                }
            }
            else
            {
                responseMessage = this.CreateNewResponseObject(request, response, null);
            }

            return responseMessage;
        }

        private HttpResponseMessage CreateNewResponseObject(HttpRequestMessage request, HttpResponseMessage response, params string[] message)
        {
            Type responseType = typeof(OttomanResponse);
            OttomanResponse ottomanResponse = Activator.CreateInstance(responseType, version, response.StatusCode, message) as OttomanResponse;
            return request.CreateResponse(response.StatusCode, ottomanResponse);
        }

        private HttpResponseMessage CreateNewResponseObject(HttpRequestMessage request, HttpResponseMessage response, object content, params string[] message)
        {
            Type responseType = typeof(OttomanResponse<>);
            responseType = responseType.MakeGenericType(content.GetType());
            var ottomanResponse = Activator.CreateInstance(responseType, version, response.StatusCode, content, message);

            return request.CreateResponse(response.StatusCode, ottomanResponse);
        }
    }
}
