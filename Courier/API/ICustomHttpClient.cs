namespace Courier.API
{
    /// <summary>
    /// Http клиент для работы с сервисами.
    /// </summary>
    public interface ICustomHttpClient
    {
        /// <summary>
        /// Токен авторизации.
        /// </summary>
        string AuthToken { get; set; }

        /// <summary>
        /// API Сферы Курьер.
        /// </summary>
        ICourier CourierApiService { get; }
    }
}