using URLShortner.Data.Models;
using URLShortner.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IShortURLService, ShortURLService>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// Add the manual route and handler for the short URLs
// Only handler URLs that match the key regular expression enforced on entry to ensure only valid
// keys are handled
app.Map("/{key:regex(^[a-zA-Z0-9]{{3,10}}$)}", async (string key,IShortURLService service) =>  {
    
    // Retrieve the actual URL based on the short key
    ShortURL shortURL = await service.GetURL(key);

    if (null == shortURL)
    {
        // If the short key does not exist return 404 Not Found
        return Results.NotFound();
    }
    else
    {
        // If the short key is found redirect to it. 
        return Results.Redirect(shortURL.URL);
    }
});

app.Run();
