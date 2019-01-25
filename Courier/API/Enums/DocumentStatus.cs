using System.ComponentModel;

namespace CourierMobile.Core.Core.API.Enums
{
    /// <summary>
    /// Статусы документов.
    /// </summary>
    public enum DocumentStatus
    {
        /// <summary>
        /// Неопределенный статус.
        /// </summary>
        None = 0,

        /// <summary>
        /// Черновик документа.
        /// </summary>
        Draft = 1,

        /// <summary>
        /// Документ ожидает подписания.
        /// </summary>
        ToSign = 2,

        /// <summary>
        /// Документ подготовлен (к чему?).
        /// </summary>
        Prepared = 3,

        /// <summary>
        /// Документ прочитан (получателем?).
        /// </summary>
        IsRead = 4,

        /// <summary>
        /// Документ на согласовании.
        /// </summary>
        OnAgreement = 5,

        /// <summary>
        /// Статус подписан.
        /// </summary>
        Signed = 6,

        /// <summary>
        /// Статус отклонен.
        /// </summary>
        Rejected = 7,

        /// <summary>
        /// Документ принят.
        /// </summary>
        Accepted = 9,

        /// <summary>
        /// На отзыве.
        /// </summary>
        OnRevocation = 10,

        /// <summary>
        /// Документ отозван.
        /// </summary>
        Revoked = 11,

        /// <summary>
        /// Документ отправлен.
        /// </summary>
        Sended = 12,

        /// <summary>
        /// Статус доставлен.
        /// </summary>
        Delivered = 13,

        /// <summary>
        /// Документ получен.
        /// </summary>
        Received = 14,

        /// <summary>
        /// Документ без маршрута.
        /// </summary>
        NoRoute = 15,

        /// <summary>
        /// Предложено аннулирование.
        /// </summary>
        [Description("Предложено аннулирование")]
        RequestedAvoidance = 16,

        /// <summary>
        /// Отклонено аннулирование документа.
        /// </summary>
        [Description("Отказано в аннулировании")]
        RejectedAvoidance = 17,

        /// <summary>
        /// Документ аннулирован.
        /// </summary>
        [Description("Документ аннулирован")]
        AcceptedAvoidance = 18,

        /// <summary>
        /// С уточнением (используется для документов СФ/КСФ при формировании УОУ вместо статутса Отклонен).
        /// </summary>
        ClarificationRequested = 19,

        /// <summary>
        /// Документ ожидает отправки (в текущей версии не используется).
        /// </summary>
        WaitingForSend = 20,

        /// <summary>
        /// Перенесен в архив.
        /// </summary>
        Archive = 21,

        /// <summary>
        /// Документ предназначенный только для интеграции.
        /// </summary>
        ForIntegrationOnly = 22
    }

}