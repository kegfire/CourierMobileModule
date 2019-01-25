using System;

namespace CourierMobile.Core.Core.API.Enums
{
    /// <summary>
    /// Флаги доступных действия для документа.
    /// </summary>
    [Flags]
    public enum Actions
    {
        /// <summary>
        /// Для пользователя нет доступных действий.
        /// </summary>
        None = 0x0000,

        /// <summary>
        /// Пользователь может добавить документ на основании существующего.
        /// </summary>
        Add = 0x0001,

        /// <summary>
        /// Пользователь может отклонить документ.
        /// </summary>
        Reject = 0x0002,

        /// <summary>
        /// Пользователь может отправить документа на согласование.
        /// </summary>
        [Obsolete]
        Submit = 0x0004,

        /// <summary>
        /// Пользователь может подтвердить отзыв документа.
        /// </summary>
        AcceptRevoke = 0x0008,

        /// <summary>
        /// Пользователь может принять документ.
        /// </summary>
        Accept = 0x0010,

        /// <summary>
        /// Пользователь может отозвать документ.
        /// </summary>
        Revoke = 0x0020,

        /// <summary>
        /// Пользователь может подписать документа.
        /// </summary>
        Sign = 0x0040,

        /// <summary>
        /// Пользователь может отправить документ.
        /// </summary>
        Send = 0x0080,

        /// <summary>
        /// Пользователь может поместить документ в корзину.
        /// </summary>
        Trash = 0x0100,

        /// <summary>
        /// Пользователь может сохранить документ.
        /// </summary>
        Edit = 0x0200,

        /// <summary>
        /// Пользователь может отклонить документ только подписав квитанцию.
        /// </summary>
        RejectWithTicket = 0x0400,

        /// <summary>
        /// Пользователь может удалить документ.
        /// </summary>
        Delete = 0x0800,

        /// <summary>
        /// Можно редактировать маршрут документа.
        /// </summary>
        EditableRoute = 0x1000,

        /// <summary>
        /// Принятие с квитанцией.
        /// </summary>
        AcceptWithTicket = 0x2000,

        /// <summary>
        /// Признак того,что в документе должен быть подставлен другой подписант.
        /// </summary>
        SubstituteSigner = 0x4000,

        /// <summary>
        /// Признак добавления квитанции с подписью (УОП).
        /// </summary>
        CreateSignatureTicket = 0x8000,

        /// <summary>
        /// Признак того,что можно направить предложение об аннулировании документа.
        /// </summary>
        AvoidanceRequest = 0x10000,

        /// <summary>
        /// Признак того,что можно отклонить предложение об аннулировании документа.
        /// </summary>
        RejectAvoidance = 0x20000,

        /// <summary>
        /// Признак того,что можно принять аннулирование документа.
        /// </summary>
        AcceptAvoidance = 0x40000,

        /// <summary>
        /// Признак принадлежности документа к пакету с синхронизацией маршрутов.
        /// </summary>
        PackageRouteSync = 0x80000,

        /// <summary>
        /// Пользователь может исключить документ из пакета.
        /// </summary>
        RemoveFromPackage = 0x100000,

        /// <summary>
        /// Пользователь может назначить документ в обработку самому себе.
        /// </summary>
        AssignableToSelf = 0x200000,

        /// <summary>
        /// Признак принятия с отказом в запрошенной подписи.
        /// </summary>
        AcceptWithoutSign = 0x400000,

        /// <summary>
        /// Признак необходимости подписания ответного титула, ранее сформированного.
        /// </summary>
        SignReplyTicket = 0x800000,

        /// <summary>
        /// Признак возможности подписания квитанции ИОП.
        /// </summary>
        SignReceiveNotification = 0x1000000,

        /// <summary>
        /// Признак возможности добавления платежного поручения.
        /// </summary>
        AddPaymentRequestOrder = 0x2000000
    }

}