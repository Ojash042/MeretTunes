namespace MeretTune.Models; 

public class ArtistFollowers {
	public Guid ArtistId { get; set; }
	public Artist Artist { get; set; }
	public Guid ApplicationUserId { get; set; }
	public ApplicationUser Followers { get; set; }
}