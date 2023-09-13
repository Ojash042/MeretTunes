using MeretTune.Models;

namespace MeretTune.ViewModels; 

public class AlbumArtistSong {
	public Album Album { get; set; }
    public Artist Artist { get; set; }
    public List<Song> Songs { get; set; }
}