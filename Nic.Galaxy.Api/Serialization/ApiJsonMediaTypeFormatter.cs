using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace Nic.Galaxy.Api.Serialization
{
    /// <summary>
    /// ApiJsonMediaTypeFormatter
    /// </summary>
    /// <seealso cref="System.Net.Http.Formatting.JsonMediaTypeFormatter" />
    public class ApiJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiJsonMediaTypeFormatter" /> class.
        /// </summary>
        public ApiJsonMediaTypeFormatter()
        {
            SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
            SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }

        private static MediaTypeFormatter _default;

        /// <summary>
        /// Default API MediaTypeFormatter
        /// </summary>
        /// <value>
        /// The default.
        /// </value>
        public static MediaTypeFormatter Default => _default ?? (_default = new ApiJsonMediaTypeFormatter());
    }
}