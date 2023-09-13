using System.Security.Claims;
using MeretTune.Models;
using MeretTune.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeretTune.Controllers; 

public class AlbumController : Controller {
	
    private readonly DataServices _dataServices;
    private readonly UserManager<ApplicationUser> _userManager;

    public AlbumController(DataServices dataServices, UserManager<ApplicationUser> userManager) {
        _dataServices = dataServices;
        _userManager = userManager;
    }

    [Route("album/{albumId}")]
    public async Task<IActionResult> Index(string albumId) {
        Guid albumGuid = Guid.Parse(albumId);
        
        bool flag = false; 
        var model =_dataServices.GetAlbumSongs(albumGuid);
        var user = await _userManager.GetUserAsync(User);
        Guid userId = User.Identity.IsAuthenticated ? user.Id : Guid.Empty; 
        flag = User.Identity.IsAuthenticated && _dataServices.CheckUserBoughtAlbum(Guid.Parse(albumId), userId);
        ViewBag.flag = flag;
        ViewBag.Identity = userId;
        return View(model);
    }}