namespace MeretTune.Services; 

public class FileServices {
		private readonly IWebHostEnvironment _environment;
    
    	public FileServices(IWebHostEnvironment environment) {
    		_environment = environment;
    	}
    
    	public async Task<string> CreateArtistImage(string artistName, IFormFile artistImage) {
    		Random rnd = new Random();
    		int num = rnd.Next();
    		string fileName = String.Concat(artistName, num, ".jpeg");
    		if (artistImage != null && artistImage.Length > 0) {
    			string filePath = Path.Combine(_environment.WebRootPath, "artistImages", fileName);
    			try {
    				await using var stream = new FileStream(filePath, FileMode.Create);
    				await artistImage.CopyToAsync(stream);
    			}
    			catch (Exception) {}
    		}
    		else {
    			throw new Exception("File Cannot be null");
    		}
    
    		return fileName;
    	}
    	
    	public async Task<string> CreateAlbumImage(string albumName, IFormFile albumImage) {
    		Random rnd = new Random();
    		int num = rnd.Next();
    		string fileName = String.Concat(albumName, num,".jpeg");
    		if (albumImage != null && albumImage.Length > 0) {
    			string filePath = Path.Combine(_environment.WebRootPath, "albumImages", fileName);
    			try {
    				await using var stream = new FileStream(filePath, FileMode.Create);
    				await albumImage.CopyToAsync(stream);
    			}
    			catch (Exception) {
    				Console.WriteLine("Oh no!");
    			}
    		}
    		return fileName;
    	}
    
    	public async Task<string> CreateSongs(string artistGuid, string AlbumGuid, IFormFile songFile, string songName) {
    			Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, "songs", artistGuid, AlbumGuid));
    			string songLocation = Path.Combine(_environment.WebRootPath, "songs", artistGuid, AlbumGuid, songFile.FileName);
    			try {
    				Console.WriteLine($"SongPath= ${Directory.Exists(songLocation)}");
    				await using var stream = new FileStream(songLocation, FileMode.Create);
    				await songFile.CopyToAsync(stream);
    			}
    			catch (Exception e) {
    				Console.WriteLine(e);
    			}
    			return Path.Combine(artistGuid,AlbumGuid,songFile.FileName);
    	}
}