using System;
using CourierMobile.Core.Core.API.Enums;

namespace CourierMobile.Core.Core.API.Models
{
    [Serializable]
    public class DocumentRelation
    {
        /// <summary>
        /// Идентификатор документа.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор родительского документа.
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// Тип взаимосвязи документов.
        /// </summary>
        public RelationType? Relation { get; set; }

        /// <summary>
        /// Описание формы документа.
        /// </summary>
        public string Description { get; set; }
    }

}