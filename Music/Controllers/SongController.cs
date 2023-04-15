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
    }
}
