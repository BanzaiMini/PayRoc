using URLShortner.Data.Models;

namespace URLShortner.Services
{
    /// <summary>
    /// Async Interface for a service that manages the ShortURLs
    /// </summary>
    public interface IShortURLService
    {
        /// <summary>
        /// Check if a specific short URL key already exists
        /// </summary>
        /// <param name="key">Short URL key to check for</param>
        /// <returns>Whether the Short URL key is already in use</returns>
        Task<bool> KeyExists(string key);
        
        /// <summary>
        /// Retrieve the long URL for a specified short URL key
        /// </summary>
        /// <param name="key">Short URL key to retrieve the long URL for</param>
        /// <returns>The long URL for the specified key, null if the short URL key does not exist</returns>
        Task<string> GetURL(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Short URL key to add the long URL key for</param>
        /// <param name="URL">Long URL to add.</param>
        /// <returns>true if the key was added successfully, false if it already exists</returns>
        Task<bool> AddURL(ShortURL URL);

    }
}
