namespace InvoiceAndStorage.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "InvoiceAndStorage";

        public const string AdministratorRoleName = "Administrator";

        public const string ValiStreet = @"[A-Za-zA-Яа-я. 0-9-]{5,40}";

        public const string ValiCity = @"[A-Za-zA-Яа-я. 0-9-]{5,40}";

        public const string ValiCountyName = @"[A-Za-zA-Яа-я. 0-9-]{5,40}";

        public const string ValidCompanyName = @"^([A-Z]{1}[a-z0-9]{1,20}([-|\s])[A-Z]{1}[a-z0-9]{1,20}([-|\s]|[0-9|\s]){1,4}([ЕООД|ООД|ЕАД|АД|ЕТ|]){2,4}|[А-Я]{1}[а-я0-9]{1,20}([-|\s])[А-Я]{1}[а-я0-9]{1,20}([-|\s]|[0-9|\s]){1,4}([ЕООД|ООД|ЕАД|АД|ЕТ|]){2,4}|[А-Я]{1}[а-я0-9]{1,20}([-|\s])([ЕООД|ООД|ЕАД|АД|ЕТ|]){2,4})$";

        public const string ValidCompanyIdentificationNumber = @"^([0-9]{9}|[0-9]{12})$";

        public const string ValidBankAccount = @"^[BG]{2}[0-9]{2}[A-Z]{4}[0-9]{14}$";

        public const string ValidCompanyOwner = @"^[А-Я]{1}[а-я]{2,20}\s[А-Я]{1}[а-я]{2,20}$";

        public const string ValidBankName = @"[A-Za-zA-Яа-я. 0-9-]{3,40}";

        public const string ValidProductName = @"^[\w{IsCyrillic}\s]{2,30}$";
    }
}
