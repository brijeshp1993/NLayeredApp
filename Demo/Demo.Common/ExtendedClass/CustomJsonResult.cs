using System;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Demo.Common.ExtendedClass
{
    /// <summary>
    /// A Newtonsoft.Json based JsonResult for ASP.NET MVC
    /// </summary>
    public class CustomJsonResult : ActionResult
    {
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Initializes a new instance of the <see>
        ///         <cref>JsonNetResult</cref>
        ///     </see>
        ///     class.
        /// </summary>
        public CustomJsonResult()
        {
            SerializerSettings = new JsonSerializerSettings();
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        /// <summary>
        /// Gets or sets the json request behaviour
        /// </summary>
        public JsonRequestBehavior JsonRequestBehavior { get; set; }

        /// <summary>
        /// Gets or sets the content encoding.
        /// </summary>
        /// <value>The content encoding.</value>
        public Encoding ContentEncoding { get; set; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>The type of the content.</value>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data object.</value>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the serializer settings.
        /// </summary>
        /// <value>The serializer settings.</value>
        public JsonSerializerSettings SerializerSettings { get; set; }

        /// <summary>
        /// Gets or sets the formatting.
        /// </summary>
        /// <value>The formatting.</value>
        public Formatting Formatting { get; set; }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult"/> class.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet
                    && String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }
            var response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrWhiteSpace(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data == null)
            {
                return;
            }
            var isoConvert = new IsoDateTimeConverter
            {
                DateTimeFormat = DateFormat
            };

            response.Write(JsonConvert.SerializeObject(Data, isoConvert));

            var writer = new JsonTextWriter(response.Output) { Formatting = Formatting };

            var serializer = JsonSerializer.Create(SerializerSettings);

            serializer.Serialize(writer, Data);

            writer.Flush();
        }
    }
}