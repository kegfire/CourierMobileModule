namespace CourierMobile.Core.Core.API.Enums
{
    /// <summary>
    /// Тип баркода.
    /// </summary>
    public enum BarcodeType
    {
        /// <summary>
        /// Баркод отсутствует.
        /// </summary>
        None = 0,

        /// <summary>
        /// Тип Code 128.
        /// </summary>
        Code128 = 1,

        /// <summary>
        /// Тип EAN 8.
        /// </summary>
        Ean8 = 2,

        /// <summary>
        /// Тип EAN 13.
        /// </summary>
        Ean13 = 3
    }

}