namespace RecruitBackend
{
    public static class CardConstants
    {
        public const string CardErrorOnlyNumbers = "CardNumber should only contain numbers.";

        public const string CardErrorNotInStorage = "No card in storage by the specified card number.";

        public const string CardErrorInvalidName =
            "Name should not be blank, and contain at most 50 alphanumeric characters.";

        public const string CardErrorInvalidExpiry =
            "ExpiryMonth and ExpiryYear should constitute a valid date on or " +
            "after the current date.";
    }
}