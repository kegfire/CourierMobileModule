using CourierMobile.Core.Core.API.Enums;

namespace CourierMobile.Core.Core.API.Models
{
    /// <summary>
    /// Основные данные клиента.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Идентификатор организации.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Тип контрагента (фл/юл).
        /// </summary>
        public CompanyType CompanyType { get; set; }

        /// <summary>
        /// Наименование клиента.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Полное наименование клиента.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Фамилия руководителя или ИП.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя руководителя или ИП.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество руководителя или ИП.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// ИНН клиента.
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// КПП клиента.
        /// </summary>
        public string Kpp { get; set; }

        /// <summary>
        /// ОГРН клиента.
        /// </summary>
        public string RegistryNumber { get; set; }

        /// <summary>
        /// Реквизиты свидетельства о государственной регистрации ИП.
        /// </summary>
        public string RegistrationRequisites { get; set; }
    }

}