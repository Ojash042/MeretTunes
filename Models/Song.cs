using System.ComponentModel.DataAnnotations;

namespace MeretTune.Models; 

public class Song {
    public Guid SongId { get; set; }
    
    [Required]
    public string SongName { get; set; } = null!;
    
    [Required]
    public Album Album { get; set; }
    public Guid AlbumId { get; set; }
    public int SongViews { get; set; }
    public string SongLocation { get; set; }
}