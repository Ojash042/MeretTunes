using MeretTune.Models;
using MeretTune.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeretTune.Controllers;

[Authorize(Policy = "EmailId")]
public class CMSController : Controller {
	private readonly DataServices _dataServices;
	private readonly FileServices _fileServices;

	public CMSController(DataServices dataServices, FileServices fileServices) {
		_dataServices = dataServices;
		_fileServices = fileServices;
	}

	public IActionResult Index() {
		var model = _dataServices.GetSiteMetrics();
		return View(model);
	}


	[HttpGet]
	public IActionResult CreateArtist() {
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> CreateArtistPost(IFormFile imageFile, string artistName, string description) {
		string artistImgPath = _fileServices.CreateArtistImage(artistName, imageFile).Result;
		Artist artist = new Artist() {
			ArtistImage = artistImgPath,
			ArtistName = artistName,
			Description = description
		};

		await _dataServices.CreateNewArtist(artist);

		return RedirectToAction("CreateArtist");
	}

	[Route("/CMS/CreateAlbum")]
	[HttpGet]
	public IActionResult CreateAlbumGet() {
		return View("CreateAlbum");
	}

	[HttpPost]
	public async Task<IActionResult> CreateAlbumPost(IFormFile imageFile, string AlbumName, string ProducerName,
		string Price, string ArtistGuid) {
		string albumImgPath = _fileServices.CreateAlbumImage(AlbumName, imageFile).Result;
		Guid albumGuid = Guid.NewGuid();
		Artist artist = _dataServices.GetArtist(Guid.Parse(ArtistGuid));

		Album album = new Album() {
			AlbumId = albumGuid,
			ProducerName = ProducerName,
			ArtistId = Guid.Parse(ArtistGuid),
			AlbumViews = 0,
			AlbumName = AlbumName,
			AlbumPrice = Int16.Parse(Price),
			Artist = artist,
			AlbumCover = albumImgPath
		};


		await _dataServices.CreateNewAlbum(album);

		return RedirectToAction("CreateSong");
	}

	[HttpGet]
	public IActionResult CreateSong() {
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> CreateSongPost(string songName, string albumGuid, IFormFile songFile) {
		Console.WriteLine(Guid.Parse(albumGuid));
		var album = _dataServices.GetAlbum(Guid.Parse(albumGuid));

		Console.WriteLine("HelloWorldd3332");
		var songLocation = _fileServices.CreateSongs(album.ArtistId.ToString(), albumGuid, songFile, songName).Result;
		var song = new Song() {
			SongId = new Guid(), SongName = songName, SongViews = 0,
			SongLocation = songLocation
		};
		song.AlbumId = Guid.Parse(albumGuid);
		song.Album = album;
		await _dataServices.CreateNewSong(song);
		ViewBag.Error = "";


		return RedirectToAction("CreateSong");
	}
}