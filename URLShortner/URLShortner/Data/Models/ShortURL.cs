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
        public string Key { get; set; }

        /// <summary>
        /// The full URL to redirect to.
        /// </summary>
        [Required]
        public string URL { get; set; }

        /// <summary>
        /// The generated URL.
        /// This is not stored only included for display
        /// </summary>
        [NotMapped]
        public string GeneratedURL { get; set; }
    }
}
