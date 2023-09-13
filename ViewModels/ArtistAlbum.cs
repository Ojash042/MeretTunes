using MeretTune.Models;

namespace MeretTune.ViewModels; 

public class ArtistAlbum {
	
public Guid ArtistId { get; set; }
    public Artist Artist { get; set; }
    
    public Guid AlbumId { get; set; }
    public Album Album { get; set; }}