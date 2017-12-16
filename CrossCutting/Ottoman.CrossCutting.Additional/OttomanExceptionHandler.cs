namespace Ottoman.CrossCutting.Additional
{
    using System;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.Results;

    public class OttomanExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// The _inner handler.
        /// </summary>
        private readonly IExceptionHandler _innerHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="OttomanExceptionHandler"/> class.
        /// </summary>
        /// <param name="innerHandler">
        /// The inner handler.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public OttomanExceptionHandler(IExceptionHandler innerHandler)
        {
            if (innerHandler == null)
            {
                throw new ArgumentNullException(nameof(innerHandler));
            }

            _innerHandler = innerHandler;
        }

        /// <summary>
        /// The ınner handler.
        /// </summary>
        public IExceptionHandler InnerHandler => this._innerHandler;

        /// <summary>
        /// The handle async.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            this.Handle(context);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is DbEntityValidationException)
            {
                DbEntityValidationException validationException = context.Exception as DbEntityValidationException;

                string message = string.Join("-", validationException.EntityValidationErrors.SelectMany(x => x.ValidationErrors.Select(a => a.ErrorMessage)));
                HttpError httpError = new HttpError(message);
                HttpResponseMessage response = context.Request.CreateResponse(HttpStatusCode.OK, httpError);
                context.Result = new OttomanExceptionResult(response);
            }
            else
            {
                context.Result = new OttomanExceptionResult(context.ExceptionContext.Request, HttpStatusCode.InternalServerError, "Oops! Sorry! Something went wrong.");
            }
        }


        private class OttomanExceptionResult : IHttpActionResult
        {
            private HttpResponseMessage _response;

            private HttpRequestMessage _request;
            private HttpStatusCode _statusCode;
            private string _content;

            public OttomanExceptionResult(HttpRequestMessage request, HttpStatusCode statusCode, string content)
            {
                _request = request;
                _statusCode = statusCode;
                _content = content;
            }

            public OttomanExceptionResult(HttpResponseMessage response)
            {
                _response = response;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                if (_response == null)
                {
                    _response = new HttpResponseMessage(_statusCode);
                    _response.Content = new StringContent(_content);
                    _response.RequestMessage = _request;
                }

                return Task.FromResult(_response);
            }
        }
    }
}
