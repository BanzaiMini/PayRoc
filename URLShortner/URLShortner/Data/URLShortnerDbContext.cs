using Microsoft.EntityFrameworkCore;
using System;
using URLShortner.Data.Models;

namespace URLShortner.Data
{
    public class URLShortnerDbContext:DbContext
    {
        public URLShortnerDbContext(DbContextOptions<URLShortnerDbContext> options) : base(options)
        {

        }

        public DbSet<ShortURL> ShortURLs { get; set; }

    }
}
