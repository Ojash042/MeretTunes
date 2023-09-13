using Microsoft.AspNetCore.Identity;

namespace MeretTune.Models; 

public class ApplicationUser : IdentityUser<Guid> {
	public string? UserProfileImage { get; set; }
	public DateOnly BirthDate { get; set; }
	public string? Gender { get; set; }
	public string? Country { get; set; }
	//public List<Playlist>? Playlist { get; set; }
	public List<AlbumUsers>? BoughtAlbums { get; set; }
	public List<ArtistFollowers>? FollowedArtists { get; set; }	
	
}