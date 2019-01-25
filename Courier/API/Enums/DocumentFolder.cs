namespace Courier.API.Enums
{
    /// <summary>
    /// Направление документа.
    /// </summary>

    public enum DocumentFolder
    {
        /// <summary>
        /// Неопределённая папка.
        /// </summary>
        None = 0,

        /// <summary>
        /// Входящий документ.
        /// </summary>
        In = 1,

        /// <summary>
        /// Исходящий документ.
        /// </summary>
        Out = 2,

        /// <summary>
        /// Черновик, недоисходящий дкоумент.
        /// </summary>
        Draft = 3,

        /// <summary>
        /// Необработанные документы.
        /// </summary>
        OnProcess = 4,

        /// <summary>
        /// Отмеченные документы.
        /// </summary>
        Favorites = 5,

        /// <summary>
        /// Обработанные документы.
        /// </summary>
        Processed = 7,

        /// <summary>
        /// Квитанции (не папка документов).
        /// </summary>
        Tickets = 8,

        /// <summary>
        /// Корзина (документы отмеченные для удаления).
        /// </summary>
        Trash = 9,

        /// <summary>
        /// Документы без маршрута.
        /// </summary>
        NoRoute = 10,

        /// <summary>
        /// Филиальные документы - на обработку.
        /// </summary>
        BranchOnProcess = 11,

        /// <summary>
        /// Филиальные документы - все документы.
        /// </summary>
        BranchAll = 12,

        /// <summary>
        /// Все документы.
        /// </summary>
        AllDocuments = 13
    }

}