using System.ComponentModel.DataAnnotations;

namespace MeretTune.Models; 

public class Artist {
	public Guid ArtistId { get; set; }
    [Required] 
    public string ArtistName { get; set; } = null!;
    public string? ArtistImage { get; set; }
    public List<Album>? Album { get; set; }
    public List<ArtistFollowers>? Followers { get; set; }
    public String Description { get; set; } = null!;
}