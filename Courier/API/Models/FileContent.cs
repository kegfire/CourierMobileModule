using System;
using System.Runtime.Serialization;

namespace CourierMobile.Core.Core.API.Models
{
    /// <summary>
    /// Тело документа.
    /// </summary>
    [DataContract]
	[Serializable]
    public class FileContent
    {
        /// <summary>
        /// Имя документа.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Filename { get; set; }

        /// <summary>
        /// Mime тип содержимого файла.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string MimeType { get; set; }

        /// <summary>
        /// Содержимое документа.
        /// </summary>
        [DataMember(IsRequired = true)]
        public byte[] Content { get; set; }
    }

}