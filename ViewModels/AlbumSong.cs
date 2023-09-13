using MeretTune.Models;

namespace MeretTune.ViewModels; 

public class AlbumSong {
	
 public Guid AlbumId { get; set; }
    public Album Album { get; set; }
    
    public Guid SongId { get; set; }
    public Song Song { get; set; }
    
}