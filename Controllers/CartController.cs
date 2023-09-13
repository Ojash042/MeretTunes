using System.Text.Json;
using MeretTune.Models;
using MeretTune.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace MeretTune.Controllers; 

[Authorize]
public class CartController : Controller {
	private readonly DataServices _dataServices;
	private readonly UserManager<ApplicationUser> _userManager;

	public CartController(DataServices dataServices, UserManager<ApplicationUser> userManager) {
		_dataServices = dataServices;
		_userManager = userManager;
	}

	public IActionResult Index() {
		List<Album> albums = new List<Album>();
		if (!String.IsNullOrEmpty(HttpContext.Session.GetString("CartValues"))) {
			string json = HttpContext.Session.GetString("CartValues");
			albums = JsonSerializer.Deserialize<List<Album>>(json);
		}
		return View(albums);
	}
	
	[Route("/Cart/AddToCart")]
	[HttpPost]
	public IActionResult AddToCart(string albumGuid) {
		Album album = _dataServices.GetAlbum(Guid.Parse(albumGuid));
		List<Album> albums; 
		string json = HttpContext.Session.GetString("CartValues");
		
		albums = (!String.IsNullOrEmpty(HttpContext.Session.GetString("CartValues"))) ? JsonSerializer.Deserialize<List<Album>>(json) : new List<Album>();
		 	
		if (!(albums.Any(item=>item.AlbumId == album.AlbumId))) {
			albums.Add(album);
			HttpContext.Session.SetString("CartValues",JsonSerializer.Serialize(albums)); 
		}
		
		return RedirectToAction("Index","Album",new{albumId=albumGuid});
	}
	
	[HttpPost]
	public IActionResult RemoveFromCart(string albumGuid) {
		Console.WriteLine(albumGuid);
		Album album = _dataServices.GetAlbum(Guid.Parse(albumGuid));
		List<Album> albums;
		string json = HttpContext.Session.GetString("CartValues");
		albums = JsonSerializer.Deserialize<List<Album>>(json);
		albums.RemoveAll(item=> item.AlbumId == album.AlbumId);
		if (albums.Count > 0) {
			HttpContext.Session.SetString("CartValues", JsonSerializer.Serialize(albums));
		}
		else {
			HttpContext.Session.Remove("CartValues");
		}
		return RedirectToAction("Index", "Cart");
	}

	[HttpGet]
	public async Task<IActionResult> Transfer() {
		if (!String.IsNullOrEmpty(HttpContext.Session.GetString("CartValues"))) {
			string json = HttpContext.Session.GetString("CartValues");
			List<Album> albums = JsonSerializer.Deserialize<List<Album>>(json);
			ApplicationUser user = _userManager.GetUserAsync(User).Result;
			Guid userId = user.Id;
			await _dataServices.TransferCart(albums, userId);
			HttpContext.Session.Remove("CartValues");
		}
		return RedirectToAction("Index", "Cart");
	}
}