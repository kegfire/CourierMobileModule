using System;
using System.Xml.Serialization;

namespace Courier.API.Models
{
    /// <summary>
    /// Информация о серверном исключении.
    /// </summary>
    [Serializable, XmlRoot("exception")]
    public class ExceptionDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionDetails"/> class.
        /// </summary>
        public ExceptionDetails()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionDetails"/> class.
        /// </summary>
        /// <param name="errorNumber">Номер ошибки.</param>
        /// <param name="message">Текст сообщения об ошибке.</param>
        public ExceptionDetails(string errorNumber, string message)
        {
            ErrorNumber = errorNumber;
            Message = message;
        }

        /// <summary>
        /// Номер ошибки.
        /// </summary>
        [XmlAttribute("errorNumber")]
        public string ErrorNumber { get; set; }

        /// <summary>
        /// Текст сообщения об ошибке.
        /// </summary>
        [XmlText]
        public string Message { get; set; }
    }

}