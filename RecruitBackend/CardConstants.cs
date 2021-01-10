namespace RecruitBackend
{
    public static class CardConstants
    {
        public const string CardErrorOnlyNumbers = "CardNumber should only contain numbers.";

        public const string CardErrorInvalidExpiry =
            "ExpiryMonth and ExpiryYear should constitute a valid date on or " +
            "after the current date.";
    }
}