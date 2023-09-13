using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MeretTune.Models;
using MeretTune.Services;
using MeretTune.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace MeretTune.Controllers;

public class HomeController : Controller {
	private readonly ILogger<HomeController> _logger;
	private readonly DataServices _dataServices;
	private readonly UserManager<ApplicationUser> _userManager;

	public HomeController(ILogger<HomeController> logger, DataServices dataServices, UserManager<ApplicationUser> userManager) {
		_dataServices = dataServices;
		_logger = logger;
		_userManager = userManager;
	}

	public IActionResult Index() {
		var model = _dataServices.GetAllAlbums();
		return View(model);
	}

	public IActionResult Privacy() {
		return View();
	}
	[Route("/Library")]
	public async Task<IActionResult> Library() {
		var user = await _userManager.GetUserAsync(User);
		Guid userId = User.Identity.IsAuthenticated ? user.Id : Guid.Empty;
		List<ArtistAlbum> model  = _dataServices.GetUserBoughtAlbums(userId);
		return View(model);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error() {
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}