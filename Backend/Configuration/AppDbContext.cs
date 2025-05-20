using Backend.Models;
using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<UrlEntity> Urls => Set<UrlEntity>();
}