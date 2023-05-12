using System;
using URLShortner.Data;
using URLShortner.Data.Models;

namespace URLShortner.Services
{
    /// <summary>
    /// Class to manage the short URLs
    /// </summary>
    public class ShortURLService : IShortURLService
    {
        /// <summary>
        /// DB Context for access ot the persisten store
        /// </summary>
        private readonly URLShortnerDbContext _dbContext;

        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<ShortURLService> _logger;

        /// <summary>
        /// Construct initialising the new db context
        /// </summary>
        /// <param name="dbContext">DB Context initiaise via the framework</param>
        public ShortURLService(URLShortnerDbContext     dbContext,
                               ILogger<ShortURLService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;   
        }

        public async Task<bool> KeyExists(string key)
        {
            bool result = false;
            try
            {
                result = _dbContext.ShortURLs.Any(s => s.Key == key);
            }
            catch(Exception e)
            {
                _logger.LogError("Failed Checking URL", e);
            }

            return result;
        }
        public async Task<bool> AddURL(ShortURL URL)
        {
            bool Added = false;

            try
            { 
                if(!(await KeyExists(URL.Key)))
                {
                    var addedEntity = _dbContext.ShortURLs.Add(URL);
                    _dbContext.SaveChanges();
                    Added = true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Failed Adding URL", e);
            }


            return Added;
        }

        public async Task<ShortURL> GetURL(string key)
        {
            ShortURL result = null;

            try
            {
                result = _dbContext.ShortURLs.FirstOrDefault(s => s.Key == key);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed Getting URL", e);
            }

            return result;
        }

        public async Task<bool> RemoveURL(string key)
        {
            bool result = false;
            try
            {
                var existingShortURL = _dbContext.ShortURLs.FirstOrDefault(s => s.Key == key);
                if (existingShortURL != null)
                {

                    _dbContext.ShortURLs.Remove(existingShortURL);
                    _dbContext.SaveChanges();
                }

                result = true;
            }
            catch(Exception e)
            {
                _logger.LogError("Failed Removing URL", e);
            }


            return result;
        }
    }
}
