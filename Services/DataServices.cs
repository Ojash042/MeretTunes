using MeretTune.Data;
using MeretTune.Models;
using MeretTune.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MeretTune.Services; 

public class DataServices {
	
private readonly ApplicationDbContext _applicationDbContext;
    private readonly IWebHostEnvironment _environment;
    public DataServices(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment) {
        _applicationDbContext = applicationDbContext;
        _environment = environment;
    }

    /*public UserAndUserInfo GetUserByUserId(Guid userId) {
        var user = _applicationDbContext.Users.Single(user => user.UserId == userId);
        var userInfo = _applicationDbContext.UserInfos.Single(userInfo => userInfo.UserId ==  userId);
        UserAndUserInfo userData = new UserAndUserInfo() {
            user = user,
            userInfo = userInfo
        };
        return userData;
    }
    public void UpdateUserData(User user, UserInfo userInfo) {
        var userRecord = _applicationDbContext.Users.FirstOrDefault(u => u.UserId == user.UserId);
        var userInfoRecord = _applicationDbContext.UserInfos.FirstOrDefault(uInfo => uInfo.UserInfoId == userInfo.UserInfoId);
        userRecord.Username = user.Username;
        userRecord.Email = user.Email;
        userInfoRecord.UserProfileImage = userInfo.UserProfileImage;
        userInfoRecord.Country = userInfo.Country;
        userInfoRecord.Gender = userInfo.Gender;
        userInfoRecord.BirthDate = userInfo.BirthDate;
        _applicationDbContext.SaveChanges();
    }

    public bool SaveUserData(User user, UserInfo userInfo) {
        if (!_applicationDbContext.Users.Any(e=>e.Email == user.Email)) {
            _applicationDbContext.Add(user);
            _applicationDbContext.Add(userInfo);
            _applicationDbContext.SaveChanges();
            return true;
        }
        return false;
    }
    /*public User CheckUserExists(string username, string password) {
        try {
            var currentUser = _applicationDbContext.Users.Single(e => e.Email == username || e.Username == username);
            var passwordValidator = new PasswordHasher<User>();
            if (passwordValidator.VerifyHashedPassword(null!, currentUser.HashedPassword, password)==PasswordVerificationResult.Success) {
                return currentUser;
            }
            return new User(){Username = null!};
        }
        catch (Exception) {
            return new User(){Username = null!};
        }
    } *//*
    
    public async Task<ApplicationUser> GetProfileInfo(Guid guid) {
        string profileImageLocation = String.Empty;
        string profileImage = String.Empty;
        
        UserAndUserInfo? userProfile = new UserAndUserInfo(); 
        userProfile = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_applicationDbContext.Users.Join(
            _applicationDbContext.UserInfos,
            user => user.UserId,
            userInfo => userInfo.UserId,
            ((user, userInfo) => new UserAndUserInfo(){ user = user, userInfo = userInfo })
        ), e=>e.user.UserId == guid);
        
        return userProfile;
    }
    
     public bool CheckUserByUserId(Guid guid) {
        return  _applicationDbContext.Users.Any(e => e.UserId == guid);
    }
*/
    public bool CheckUserBoughtAlbum(Guid albumId, Guid userId) {
        return _applicationDbContext.AlbumUsers.Any(au => au.ApplicationUserId == userId && au.AlbumId == albumId);
    }
     public  Artist GetArtist(Guid artistId) {
         return  _applicationDbContext.Artists.Single(u => u.ArtistId == artistId);
     }

     public Album GetAlbum(Guid albumId) {
         return _applicationDbContext.Albums.Single(album => album.AlbumId == albumId);
     }
     
     public List<ArtistAlbum> GetAllAlbums() {
         return  _applicationDbContext.Albums.Join(_applicationDbContext.Artists,
             album => album.ArtistId,
             artist => artist.ArtistId,
             ((album, artist) => new ArtistAlbum(){Album = album, Artist = artist} )
             ).ToList();
     }

     public List<ArtistAlbum> GetUserBoughtAlbums(Guid userId) {
         return _applicationDbContext.AlbumUsers.Where(au => au.ApplicationUserId == userId)
             .Select(au => au.Album)
             .Join(_applicationDbContext.Artists,
                 album => album.ArtistId,
                 artist => artist.ArtistId,
                 (((album, artist) => new ArtistAlbum() { Album = album, Artist = artist }))
             ).ToList();
     }

     public AlbumArtistSong? GetAlbumSongs(Guid albumId) {
         return _applicationDbContext.Albums
             .Join(_applicationDbContext.Artists,
                 album => album.ArtistId,
                 artist => artist.ArtistId,
                 (album, artist) => new { album, artist })
             .GroupJoin(_applicationDbContext.Song,
                 ab => ab.album.AlbumId,
                 song => song.AlbumId,
                 (ab, songs) => new AlbumArtistSong() {
                     Album = ab.album,
                     Artist = ab.artist,
                     Songs = songs.ToList()
                 }).SingleOrDefault(composite => composite.Album.AlbumId == albumId);
     }

     public async Task CreateNewAlbum(Album album) {
         _applicationDbContext.Albums.Add(album);
         await _applicationDbContext.SaveChangesAsync();
     }

     public async Task CreateNewSong(Song song) {
         _applicationDbContext.Song.Add(song);
         await _applicationDbContext.SaveChangesAsync();
     }

     public async Task CreateNewArtist(Artist artist) {
         _applicationDbContext.Artists.Add(artist);
         await _applicationDbContext.SaveChangesAsync();
     }

     public async Task TransferCart(List<Album> albums, Guid UserId) {
         foreach (var album in albums) {
             AlbumUsers albumUsers = new AlbumUsers() {
                 AlbumId = album.AlbumId,
                 ApplicationUserId = UserId,
             };
             _applicationDbContext.AlbumUsers.Add(albumUsers);
              await _applicationDbContext.SaveChangesAsync();
         }
     }

     public SiteMetrics GetSiteMetrics() {
         SiteMetrics metrics = new SiteMetrics() {
             AlbumCount = _applicationDbContext.Albums.Count(),
             ArtistCount = _applicationDbContext.Artists.Count(),
             NoOfAlbumsBought = _applicationDbContext.AlbumUsers.Count(),
             SongCount = _applicationDbContext.Song.Count(),
             UserCount = _applicationDbContext.Users.Count()
         };
         return metrics;
     }
}
