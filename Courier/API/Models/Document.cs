using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Xml.Serialization;
using CourierMobile.Core.Core.API.Enums;
using CourierMobile.Core.Core.API.Models;

namespace Courier.API.Models
{
    [Serializable]
    public class Document
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class.
        /// </summary>
        public Document()
        {
            Relations = new List<DocumentRelation>();
        }

        /// <summary>
        /// Идентификатор документа.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор типа документа.
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Наименование типа документа.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Код типа документа.
        /// </summary>
        public string DocumentTypeCode { get; set; }


        /// <summary>
        /// Описание документа.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Тип баркода.
        /// </summary>
        public BarcodeType BarcodeType { get; set; }

        /// <summary>
        /// Значение баркода.
        /// </summary>
        public string BarcodeValue { get; set; }

        /// <summary>
        /// Идентификатор отправителя.
        /// </summary>
        public long SenderId { get; set; }

        /// <summary>
        /// Идентификатор получателя.
        /// </summary>
        public long ReceiverId { get; set; }

        /// <summary>
        /// Идентификатор контрагента (отправителя или получателя).
        /// </summary>
        public long ParticipantId { get; set; }

        /// <summary>
        /// Наименование контрагента (отправителя/получателя) документа.
        /// </summary>
        public string ParticipantName { get; set; }

        /// <summary>
        /// Статус документа.
        /// </summary>
        public DocumentStatus Status { get; set; }

        /// <summary>
        /// Дата документа.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Признак необходимости запроса подписи у получателя.
        /// </summary>
        public bool RequestSign { get; set; }

        /// <summary>
        /// Номер документа.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Сумма без НДС.
        /// </summary>
        public decimal? NetSum { get; set; }

        /// <summary>
        /// Сумма НДС.
        /// </summary>
        public decimal? VatSum { get; set; }

        /// <summary>
        /// Сумма с НДС.
        /// </summary>
        public decimal? TotalSum { get; set; }

        /// <summary>
        /// Маска доступных действий с документом.
        /// </summary>
        public Actions Actions { get; set; }

        /// <summary>
        /// Признак того что документ добавлен в избранное.
        /// </summary>
        public bool IsMarked { get; set; }

        /// <summary>
        /// Дата создания документа.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Дата отправки (выставления) документа.
        /// </summary>
        public DateTime? SendDate { get; set; }

        /// <summary>
        /// Дата получения документа.
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// Признак завершенности документооборота.
        /// </summary>
        public bool IsDocflowCompleted { get; set; }

        /// <summary>
        /// Комментарий к документу.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Номер договора.
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// Дата договора.
        /// </summary>
        [XmlElement(DataType = "date")]
        public DateTime? ContractDate { get; set; }

        /// <summary>
        /// Тип транспортного документа.
        /// </summary>
        public TransportDocumentType? Type { get; set; }

        /// <summary>
        /// Тип документа.
        /// </summary>
        public DocumentFormType FormType { get; set; }

        /// <summary>
        /// Идентификатор участника ЭДО отправителя.
        /// </summary>
        public string SellerCode { get; set; }

        /// <summary>
        /// Идентификатор участника ЭДО получателя.
        /// </summary>
        public string BuyerCode { get; set; }

        /// <summary>
        /// Версия формата документа.
        /// </summary>
        public string FormatVersion { get; set; }

        /// <summary>
        /// Имя файла документа.
        /// </summary>
        public string Filename { get; set; }

        #region Устаревшие поля

        /// <summary>
        /// Причина отзыва.
        /// </summary>
        public string RevokeReason { get; set; }

        /// <summary>
        /// Описание договора.
        /// </summary>
        public string ContractDescription { get; set; }

        /// <summary>
        /// Признак, указывающий, что документ, передавался (должен передаться) в роуминге.
        /// </summary>
        public bool IsRoaming { get; set; }

        /// <summary>
        /// Содержимое документа.
        /// </summary>
        public FileContent Content { get; set; }

        /// <summary>
        /// Признак того что документ можно распечатать.
        /// </summary>
        public bool IsPrintable { get; set; }

        /// <summary>
        /// Связанные документы.
        /// </summary>
        public List<DocumentRelation> Relations { get; set; }

        /// <summary>
        /// Наименование статуса документа (для текущего пользователя).
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Причина отклонения.
        /// </summary>
        public string RejectReason { get; set; }

        /// <summary>
        /// Наименование отправителя документа.
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Наименование получателя документа.
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// Признак, указывающий отсутствие в маршруте подписанта на стороне получателя при need_rcpt_sign=1.
        /// </summary>
        public bool ExistRouteSigner { get; set; }

        /// <summary>
        /// Статус/тип связи документа (тестовый/промышленный/...).
        /// </summary>
        public DocumentRelationType DocumentRelationType { get; set; }

        /// <summary>
        /// Приложения к документу.
        /// </summary>
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// Дата смены статуса.
        /// </summary>
        public DateTime StatusChanged { get; set; }

        /// <summary>
        /// Идентификатор пакета.
        /// </summary>
        public long? PackageId { get; set; }
        #endregion

        /// <summary>
        /// Возвращает идентификатор документа строкой.
        /// </summary>
        /// <returns>Идентификатор документа.</returns>
        public string GetId()
        {
            return Id.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Возвращает массив связанных документов.
        /// </summary>
        /// <returns>Массив связанных документов.</returns>
        public DocumentRelation[] GetRelations()
        {
            return Relations?.ToArray();
        }
    }

}