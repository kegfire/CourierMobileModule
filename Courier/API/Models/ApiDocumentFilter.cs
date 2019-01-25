using System;
using Courier.API.Enums;

namespace Courier.API.Models
{
    /// <summary>
    /// Параметры фильтрации через API папки с документами.
    /// </summary>
    [Serializable]
    public class ApiDocumentFilter
    {
        /// <summary>
        /// Папка документов.
        /// </summary>
        public DocumentFolder Folder { get; set; }

        /// <summary>
        /// Идентификатор отправителя/получателя.
        /// </summary>
        public long? ParticipantId { get; set; }

        /// <summary>
        /// Идентификатор статуса.
        /// </summary>
        public int? StatusId { get; set; }

        /// <summary>
        /// Дата документа с.
        /// </summary>
        public DateTime? From { get; set; }

        /// <summary>
        /// Дата документа по.
        /// </summary>
        public DateTime? To { get; set; }

        /// <summary>
        /// Дата создания документа с.
        /// </summary>
        public DateTime? CreatedFrom { get; set; }

        /// <summary>
        /// Дата создания документа по.
        /// </summary>
        public DateTime? CreatedTo { get; set; }

        /// <summary>
        /// Номер договора.
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// Дата договора.
        /// </summary>
        public DateTime? ContractDateFrom { get; set; }

        /// <summary>
        /// Дата окончания договора.
        /// </summary>
        public DateTime? ContractDateTo { get; set; }

        /// <summary>
        /// Начало периода даты отправки(выставления).
        /// </summary>
        public DateTime? SendDateFrom { get; set; }

        /// <summary>
        /// Окончание периода даты отправки(выставления).
        /// </summary>
        public DateTime? SendDateTo { get; set; }

        /// <summary>
        /// Начало периода даты получения.
        /// </summary>
        public DateTime? ReceiveDateFrom { get; set; }

        /// <summary>
        /// Окончание периода даты получения.
        /// </summary>
        public DateTime? ReceiveDateTo { get; set; }

        /// <summary>
        /// Документооборот завершен.
        /// </summary>
        public bool? IsDocflowCompleted { get; set; }

        /// <summary>
        /// Признак того, что документ назначен текущему пользователю.
        /// </summary>
        public bool? IsMine { get; set; }

        /// <summary>
        /// Идентификатор участника маршрута.
        /// </summary>
        public int[] UserId { get; set; }

        /// <summary>
        /// Идентификатор группы участников маршрута.
        /// </summary>
        /// <remarks>На самом деле GroupId не меняем потому что АПИ.</remarks>
        public int[] RoleId { get; set; }

        /// <summary>
        /// Код типа документа.
        /// </summary>
        public string DocumentTypeCode { get; set; }

        #region 1C compatiblity area

        /// <summary>
        /// Задает значение для свойства <see cref="ParticipantId"/>.
        /// </summary>
        /// <param name="participantId">Новое значение.</param>
        public void SetParticipantId(long participantId)
        {
            ParticipantId = participantId;
        }

        #endregion
    }
}