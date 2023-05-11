using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using URLShortner.Data.Models;

namespace URLShortner.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
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
            if (ModelState.IsValid)
            {
                //TODO: Retrieve any existing URL.

                ShortURL existingEntry = null;

                if (existingEntry != null)
                {
                    ModelState.AddModelError("", "The short URL you have entered is not available - please try again.");
                }

                ShortURL.GeneratedURL = Request.GetDisplayUrl() + ShortURL.Key;

                //TODO: Store the ShortURL

            }
            else
            {
                // The values entered failed validation so redisplay hte page with the validation summary.
                ModelState.AddModelError("","The values you have entered are not valid - please try again.");
            }

            return Page();
        }



    }
}