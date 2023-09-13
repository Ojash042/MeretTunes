namespace MeretTune.Models; 

public class AlbumUsers {
	public Guid AlbumId { get; set; }
	public Album Album { get; set; }
	public Guid ApplicationUserId { get; set; }
	public ApplicationUser Buyers { get; set; }
}