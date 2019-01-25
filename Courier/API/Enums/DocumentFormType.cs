namespace CourierMobile.Core.Core.API.Enums
{
    /// <summary>
    /// Типы документов.
    /// </summary>
    public enum DocumentFormType
    {
        /// <summary>
        /// Прочий документ.
        /// </summary>
        Other = 0,

        /// <summary>
        /// Тип документа: Счет-фактура.
        /// </summary>
        Invoice = 1,

        /// <summary>
        /// Тип документа: Корректировочная счет-фактура.
        /// </summary>
        CorrectionInvoice = 2,

        /// <summary>
        /// Тип документа: Счёт.
        /// </summary>
        Bill = 3,

        /// <summary>
        /// Товарная накладная (ТОРГ-12).
        /// </summary>
        Torg12 = 4,

        /// <summary>
        /// Акт выполненных работ (структурированный).
        /// </summary>
        AcceptanceCertificate = 5,

        /// <summary>
        /// Акт выполненных работ.
        /// </summary>
        Act = 6,

        /// <summary>
        /// Акт сверки.
        /// </summary>
        ReconciliationReport = 7,

        /// <summary>
        /// Тип документа: Договор.
        /// </summary>
        Agreement = 8,

        /// <summary>
        /// Тип документа: Документ.
        /// </summary>
        Document = 9,

        /// <summary>
        /// Тип документа: Прайс-лист.
        /// </summary>
        PriceList = 10,

        /// <summary>
        /// Прайс-лист согласование.
        /// </summary>
        PriceListReconciliation = 11,

        /// <summary>
        /// Приложение к акту о выполнении работ (оказании услуг).
        /// </summary>
        ActAdd = 12,

        /// <summary>
        /// Счет (неструктурированный).
        /// </summary>
        PlainBill = 13,

        /// <summary>
        /// Уведомление о расчёте скидки.
        /// </summary>
        NoticeDiscounts = 14,

        /// <summary>
        /// Заявление об участнике ЭДО.
        /// </summary>
        Infsoob = 15,

        /// <summary>
        /// Договор автоматической оплаты счетов-фактур.
        /// </summary>
        DirectDebitContract = 16,

        /// <summary>
        /// Тип документа: Поставка.
        /// </summary>
        Delivery = 17,

        /// <summary>
        /// Тип документа: Реестр переданных денежных требований.
        /// </summary>
        RegistryUdt = 18,

        /// <summary>
        /// Тип документа: Реестр подтвержденных денежных требований.
        /// </summary>
        RegistryPdt = 19,

        /// <summary>
        /// Тип документа: Уведомление об отгрузке.
        /// </summary>
        Desadv = 20,

        /// <summary>
        /// Тип документа: Уведомление о приемке.
        /// </summary>
        Recadv = 21,

        /// <summary>
        /// Тип документа: Запрос на получение кредита/гарантии.
        /// </summary>
        CreditRequest = 22,

        /// <summary>
        /// Тип документа: Заявка/изменение на кредит, гарантию.
        /// </summary>
        
        CreditApplication = 23,

        /// <summary>
        /// Тип документа: Ответ на требование.
        /// </summary>
        
        CreditRequestReply = 24,

        /// <summary>
        /// Тип документа: Кредитно-обеспечительная документация.
        /// </summary>
        
        CreditDocumentation = 25,

        /// <summary>
        /// Тип документа: Кредитно-обеспечительная документация - подтверждение.
        /// </summary>
        
        CreditDocumentationConf = 26,

        /// <summary>
        /// УПД. Счёт-фактура.
        /// </summary>
        UpdInvoice = 27,

        /// <summary>
        /// Тип документа: Реестр платежей (инвест-проекты).
        /// </summary>
        FeesRegisterInvestment = 28,

        /// <summary>
        /// УПД. Счёт-фактура доп.
        /// </summary>
        UpdInvoiceInfo = 29,

        /// <summary>
        /// УПД. Акт-торг.
        /// </summary>
        UpdInfo = 30,

        /// <summary>
        /// Корректировочный счет-фактура, применяемый при расчетах по налогу на добавленную стоимость.
        /// Документ с функцией «КССЧФ».
        /// </summary>
        UpdCorrectionInvoice = 31,

        /// <summary>
        /// Корректировочный счет-фактура, применяемый при расчетах по налогу на добавленную стоимость и первичный
        /// учетный документ о передаче товаров (работ, услуг, имущественных прав).
        /// Документ с функцией «КСЧФДИС».
        /// </summary>
        UpdCorrectionInvoiceInfo = 32,

        /// <summary>
        /// Корректировочный первичный учетный документ для оформления факта передачи товаров (работ, услуг, имущественных прав).
        /// Документ с функций «ДИС».
        /// </summary>
        UpdCorrectionInfo = 33,

        /// <summary>
        /// Форма документа: запрос на оценку.
        /// </summary>
        ServiceRequest = 34,

        /// <summary>
        /// Заявка на дофинансирование.
        /// </summary>
        AdditionalFinancingRequest = 35,

        /// <summary>
        /// Заявка на отмену финансирования.
        /// </summary>
        CancelFinancingRequest = 36,

        /// <summary>
        /// Документ о передаче товаров.
        /// </summary>
        GoodsDeliveryReport = 37,

        /// <summary>
        /// Документ о передаче результатов работ.
        /// </summary>
        WorksDeliveryReport = 38,

        /// <summary>
        /// Коммерческое предложение.
        /// </summary>
        ServiceKP = 39,

        /// <summary>
        /// Задание на проведение оценки в целях залога.
        /// </summary>
        ServiceTask = 40,

        /// <summary>
        /// Договор оценки в целях залога.
        /// </summary>
        ServiceContract = 41,

        /// <summary>
        /// Запрос дополнительных документов.
        /// </summary>
        ServiceDocRequest = 42,

        /// <summary>
        /// Платежное поручение.
        /// </summary>
        PaymentOrder = 43,

        /// <summary>
        /// Дополнительный документ.
        /// </summary>
        ServiceDocResponse = 44,

        /// <summary>
        /// Проект отчета об оценке.
        /// </summary>
        ServiceReportProject = 45,

        /// <summary>
        /// Отчет об оценке.
        /// </summary>
        ServiceReport = 46,

        /// <summary>
        /// Договор гранта.
        /// </summary>
        AgreementFpg = 47,

        /// <summary>
        /// Запрос на предоставление документов финмониторинга.
        /// </summary>
        FinancialMonitoringRequest = 48,

        /// <summary>
        /// Документы по запросу финмониторинга.
        /// </summary>
        FinancialMonitoringRequestReply = 49,
    }

}