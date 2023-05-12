using URLShortner.Data.Models;

namespace URLShortner.Services
{
    /// <summary>
    /// Class to manage the short URLs
    /// </summary>
    public class ShortURLService : IShortURLService
    {
        /// <summary>
        /// Temporary in memory implementation for incremental testing
        /// </summary>
        private static Dictionary<string,string> s_ShortURLMap = new Dictionary<string,string>();
        public async Task<bool> KeyExists(string key)
        {
            return s_ShortURLMap.ContainsKey(key);
        }
        public async Task<bool> AddURL(ShortURL URL)
        {
            bool Added = false;

            if(!(await KeyExists(URL.Key)))
            {
                s_ShortURLMap[URL.Key] = URL.URL;
                Added = true;
            }

            return Added;
        }

        public async Task<string> GetURL(string key)
        {
            s_ShortURLMap.TryGetValue(key, out var url);

            return url;
        }
    }
}
