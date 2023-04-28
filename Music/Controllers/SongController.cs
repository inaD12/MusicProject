using Microsoft.AspNetCore.Mvc;
using Music.Data;
using Music.Data.DataModels;
using Music.Services;
using Music.Services.ViewModels;

namespace Music.Controllers
{
    public class SongController : Controller
    {
		public SongService songService { get; set; }

		public SongController(SongService service)
        {
            songService = service;
        }
        public IActionResult Index()
        {

            List<Song> songs = songService.GetAll(); 

            return this.View(songs);
        }

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(SongViewModel songVM)
        {
            if(!songService.CheckAlbum(songVM.AlbumName))
            {
                ModelState.AddModelError("AlbumName", "An album with this name doesn't exist");
            }
			if (!songService.CheckArtist(songVM.ArtistName))
			{
				ModelState.AddModelError("ArtistName", "An artist with this name doesn't exist");
			}
            if(songService.CheckAlbum(songVM.AlbumName) && songService.CheckArtist(songVM.ArtistName))
			{
                await songService.CreateAsync(songVM);
                return RedirectToAction("Index");
            }
            return View(songVM);  
        }
    }
}
