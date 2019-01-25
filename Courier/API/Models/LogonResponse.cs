using System;

namespace CourierMobile.Core.Core.API.Models
{
    /// <summary>
    /// Результат авторизации пользователя.
    /// </summary>
    [Serializable]
    public class LogonResponse
    {
        /// <summary>
        /// Токен авторизации.
        /// </summary>
        public string Token { get; set; }
    }
}