namespace RecruitBackend.Models
{
    /// <summary>
    /// Represents a card registered by users of the application.
    /// </summary>
    public class Card : BaseEntity
    {
        /// <summary>
        /// The credit card number uniquely identifying this card.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// The numeric (1-based) month when this card expires.
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// The numeric (full) year when this card expires.
        /// </summary>
        public int ExpiryYear { get; set; }

        /// <summary>
        /// The (case-insensitive) name of this card's owner as printed on the card itself.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Also known as a card security code, this is a security feature to prevent credit card fraud.
        /// Look up <seealso cref="https://en.wikipedia.org/wiki/Card_security_code">CVC articles</seealso> for more.
        /// </summary>
        public int CVC { get; set; }
    }
}