using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Courier.API.Models;
using CourierMobile.Core.Core.API.Models;
using Refit;

namespace Courier.API
{       
    /// <summary>
    /// API Сферы Курьер.
    /// </summary>
    [Headers("Content-type: application/json")]
    public interface ICourier
    {
        /// <summary>
        /// Авторизация на сервере.
        /// </summary>
        /// <param name="credentials">Логин и пароль пользователя.</param>
        /// <returns> Результат авторизации.</returns>
        [Post("/api/auth/logon")]
        Task<LogonResponse> Logon([Body] Credentials credentials);

		/// <summary>
		/// Получить список документов согласно фильтру.
		/// </summary>
		/// <param name="token">Ключ авторизации.</param>
		/// <param name="filter"> Фильтр.</param>
		/// <param name="cancellationToken">Токен отмены.</param>
		/// <returns> Список документов.</returns>
		[Post("/api/document/list")]
        Task<List<Document>> DocumentList([Header("Auth-token")] string token, [Body] ApiDocumentFilter filter, CancellationToken cancellationToken);

        /// <summary>
        /// Получить информацию о своей компании.
        /// </summary>
        /// <param name="token">Ключ авторизации.</param>
        /// <returns> Информация об организации.</returns>
        [Get("/v2.0/client/self")]
        Task<Client> GetInfoBySelf([Header("Auth-token")] string token);

	    /// <summary>
	    /// Получить pfd версию файла.
	    /// </summary>
	    /// <param name="token">Ключ авторизации.</param>
	    /// <param name="documentId"> Id документа.</param>
	    /// <param name="cancellationToken">Токен отмены.</param>
	    /// <returns> Pdf файл.</returns>
	    [Get("/api/document/pdf/{documentId}")]
		[Headers("Accept:application/json")]
		Task<FileContent> GetPdf([Header("Auth-token")] string token, long documentId, CancellationToken cancellationToken);

		/// <summary>
		/// Получить карточку и содержимое документа.
		/// </summary>
		/// <param name="token">Ключ авторизации.</param>
		/// <param name="documentId">Идентификатор документа.</param>
		/// <returns>Карточка и содержимое документа.</returns>
		[Get("/api/document/details/{documentId}")]
		[Headers("Accept:application/json")]
		Task<Document> Document([Header("Auth-token")] string token, long documentId);
	}
}