using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using URLShortner.Data;
using URLShortner.Data.Models;
using URLShortner.Services;

namespace URLShortnerUnitTests
{
    /// <summary>
    /// Tests for the functionality of the ShortURLService
    /// </summary>
    public class ShortURLServiceUnitTests
    {
        //TODO: Moq out these dependencies
        private static string connectionstring = "Server = (localdb)\\mssqllocaldb; Database = URLShortner; Trusted_Connection = True; MultipleActiveResultSets = true;";

        private static DbContextOptionsBuilder<URLShortnerDbContext> optionsBuilder = new DbContextOptionsBuilder<URLShortnerDbContext>();

        private static ILogger<ShortURLService> logger = LoggerFactory.Create(logging => logging.AddConsole()).CreateLogger<ShortURLService>();


        static ShortURLServiceUnitTests()
        {
            optionsBuilder.UseSqlServer(connectionstring);
        }

        IShortURLService service = new ShortURLService(new URLShortnerDbContext(optionsBuilder.Options), logger);

        /// <summary>
        /// Clear down the persistent store between tests
        /// </summary>
        public ShortURLServiceUnitTests()
        {
            service.RemoveURL("qwerty");
        }

        [Fact]
        public async Task AddANewURL_ShouldSucceed()
        {
            ShortURL shortURL = new ShortURL
            {
                Key = "qwerty",
                URL="https://www.microsoft.com/"
            };

            Assert.True(await service.AddURL(shortURL));

        }

        [Fact]
        public async Task AddAnExistingURL_ShouldFail()
        {
            ShortURL shortURL = new ShortURL
            {
                Key = "qwerty",
                URL = "https://www.microsoft.com/"
            };

            await service.AddURL(shortURL);
            Assert.False(await service.AddURL(shortURL));

        }
        [Fact]
        public async Task RetrieveANonExistingURL_ShouldFail()
        {

            Assert.Null(await service.GetURL("Unknown"));

        }
        [Fact]
        public async Task RetrieveAnExistingURL_ShouldSucceed()
        {
            ShortURL shortURL = new ShortURL
            {
                Key = "qwerty",
                URL = "https://www.microsoft.com/"
            };

            await service.AddURL(shortURL);

            ShortURL result = await service.GetURL(shortURL.Key);

            Assert.Equal(result.Key, shortURL.Key);
            Assert.Equal(result.URL, shortURL.URL);

        }
    }
}