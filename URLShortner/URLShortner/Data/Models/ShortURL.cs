using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace URLShortner.Data.Models
{
    /// <summary>
    /// Data Model Object representing the mapping between a short url key and the corresponding full URL
    /// </summary>
    public class ShortURL
    {
        /// <summary>
        /// The short URL key that maps to a full URL
        /// </summary>
        [Key]
        [RegularExpression(@"^[a-zA-Z0-9]{3,10}$",ErrorMessage = "The Key can only contain between 3 and 10 characters or numbers")]
        public string Key { get; set; }

        /// <summary>
        /// The full URL to redirect to.
        /// </summary>
        [Required]
        [Url]
        public string URL { get; set; }

        /// <summary>
        /// The generated URL.
        /// This is not stored only included for display
        /// </summary>
        [NotMapped]
        public string GeneratedURL { get; set; }
    }
}
