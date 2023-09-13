using System.ComponentModel.DataAnnotations;

namespace MeretTune.Models; 

public class Album {
	
	public Guid AlbumId { get; set; }
    [Required]
    public string AlbumName { get; set; } = null!;
    
    [Required] 
    public string ProducerName { get; set; } = null!;
    
    public Guid ArtistId { get; set; }
    
    [Required]
    public Artist Artist { get; set; } = null!;
    public string? AlbumCover { get; set; }

    public List<Song> Songs { get; set; } = null!;
    public int AlbumViews { get; set; }
    [Required]
    public int AlbumPrice { get; set; }
    
    public List<AlbumUsers>? Buyers { get; set; }
    
}