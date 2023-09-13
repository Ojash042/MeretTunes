namespace MeretTune.Models; 

public class Playlist {
	public Guid PlaylistId { get; set; }
    public string PlaylistName { get; set; } = null!;
    public DateOnly DateCreated { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public List<Song> Songs { get; set; } = null!;
}