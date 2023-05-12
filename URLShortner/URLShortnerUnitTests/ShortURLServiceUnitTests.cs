using URLShortner.Data.Models;
using URLShortner.Services;

namespace URLShortnerUnitTests
{
    /// <summary>
    /// Tests for the functionality of the ShortURLService
    /// </summary>
    public class ShortURLServiceUnitTests
    {
 
        IShortURLService service = new ShortURLService();

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