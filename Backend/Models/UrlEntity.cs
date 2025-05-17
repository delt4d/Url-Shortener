using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class UrlEntity
{
    public Guid Id { get; set; } = Guid.Empty;
    
    [StringLength(int.MaxValue)]
    public string OriginalUrl { get; set; } = string.Empty;
    
    [StringLength(int.MaxValue)]
    public string ShortUrl { get; set; } = Guid.NewGuid().ToString();
}