namespace MeretTune.Models; 

public class UserInfo {
	
	public Guid UserInfoId { get; set; }
	public ApplicationUser ApplicationUser { get; set; }
    public Guid ApplicationUserId { get; set; }
    public string? UserProfileImage { get; set; }  
    
    public DateOnly BirthDate { get; set; }
    public string Gender { get; set; } = null!;
    public string Country { get; set; } = null!;
    public List<Playlist> Playlist { get; set; }
    public List<Album> BoughtAlbums { get; set; }
    public List<Artist> FollowedArtists { get; set; }}