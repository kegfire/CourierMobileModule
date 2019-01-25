using System;
using System.Collections.Generic;
using System.Net.Http;
using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Refit;

namespace Courier.API
{
    /// <summary>
    /// Http клиент для работы с сервисами.
    /// </summary>
    public class CustomHttpClient : ICustomHttpClient
    {
        /// <summary>
        /// API Сфера Курьер.
        /// </summary>
        public ICourier CourierApiService { get; }

        /// <summary>
        /// Конструктор класса <see cref="CustomHttpClient"/>
        /// </summary>
        public CustomHttpClient()
        {
            var client = new HttpClient(new NativeMessageHandler())
            {
                BaseAddress = new Uri(App.ServerAddress),
                Timeout = TimeSpan.FromMilliseconds(20000)
            };
            CourierApiService = RestService.For<ICourier>(client, new RefitSettings
            {
                JsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    Converters = new List<JsonConverter> { new StringEnumConverter() }    
                }
            });
        }

        /// <inheritdoc />
        public string AuthToken { get; set; }
    }
}