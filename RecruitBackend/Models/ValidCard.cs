namespace RecruitBackend.Models
{
    /// <summary>
    /// Represents card information restricting which cards may be newly registered.
    /// </summary>
    public class ValidCard : BaseEntity
    {
        /// <summary>
        /// The card number representing what cards may be registered via the CardService. 
        /// </summary>
        public string CardNumber { get; set; }
    }
}