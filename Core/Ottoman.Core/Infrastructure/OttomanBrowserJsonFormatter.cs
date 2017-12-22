namespace Ottoman.Core.Infrastructure
{
    using System;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;

    using Newtonsoft.Json;

    public class OttomanBrowserJsonFormatter : JsonMediaTypeFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OttomanBrowserJsonFormatter"/> class.
        /// </summary>
        public OttomanBrowserJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            this.SerializerSettings.Formatting = Formatting.Indented;
        }

        /// <summary>
        /// The set default content headers.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="headers">
        /// The headers.
        /// </param>
        /// <param name="mediaType">
        /// The media type.
        /// </param>
        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}
