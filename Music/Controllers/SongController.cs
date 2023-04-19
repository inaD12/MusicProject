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

            List<SongViewModel> songs = songService.GetAll(); 

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

            await songService.CreateAsync(songVM);
            return RedirectToAction("Index");
        }
    }
}
