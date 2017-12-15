namespace Ottoman.CrossCutting.Additional
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.ExceptionHandling;
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
            // Create your own custom result here...
            // In dev, you might want to null out the result
            // to display the YSOD.
            // context.Result = null;
            context.Result = new InternalServerErrorResult(context.Request);
        }
    }
}
