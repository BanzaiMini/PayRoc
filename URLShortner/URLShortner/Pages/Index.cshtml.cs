using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using URLShortner.Data.Models;
using URLShortner.Services;

namespace URLShortner.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IShortURLService    _service;


        public IndexModel(ILogger<IndexModel> logger,
                          IShortURLService    service)
        {
            _logger = logger;
            _service = service;
        }
        
        /// <summary>
        /// The values related to the Short URL
        /// </summary>
        [BindProperty]
        public ShortURL ShortURL { get; set; }


        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            // Check for any existing URL with the requested short url.
            if (await _service.KeyExists(ShortURL.Key))
            {
                ModelState.AddModelError("", "The Key you have entered is not available - please try again.");
            }

            if (ModelState.IsValid)
            {
                // Build the full Short URL
                UriBuilder builder = new UriBuilder(Request.Scheme,
                                                    Request.Host.Host);
                
                // Add the port number if using a non standard port
                if (Request.Host.Port.HasValue)
                {
                    builder.Port = Request.Host.Port.Value;
                }

                // Use the key as the path
                builder.Path = ShortURL.Key;
                
                ShortURL.GeneratedURL = builder.Uri.ToString();
 

                await _service.AddURL(ShortURL);
            }


            return Page();
        }



    }
}