using Microsoft.AspNetCore.Mvc;
using Music.Data.DataModels;
using Music.Services.Interfaces;
using Music.Services.ViewModels;
using System.Security.Claims;

namespace Music.Controllers
{
    public class SongController : Controller
    {
        public ISongService songService { get; set; }

        public SongController(ISongService service)
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
            if (!songService.CheckAlbum(songVM.AlbumName))
            {
                ModelState.AddModelError("AlbumName", "An album with this name doesn't exist");
            }
            if (!songService.CheckArtist(songVM.ArtistName))
            {
                ModelState.AddModelError("ArtistName", "An artist with this name doesn't exist");
            }
            if (songService.CheckAlbum(songVM.AlbumName) && songService.CheckArtist(songVM.ArtistName))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                songVM.CreatorID = userId;
                await songService.CreateAsync(songVM);
                TempData["done"] = "A Song Has Been Created Successfully!";
                return RedirectToAction("Index");
            }
            return View(songVM);
        }

        public IActionResult Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!songService.CheckId(id))
            {
                return NotFound();
            }
            return View(songService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SongViewModel songVM)
        {
            if (!songService.CheckAlbum(songVM.AlbumName))
            {
                ModelState.AddModelError("AlbumName", "An album with this name doesn't exist");
            }
            if (!songService.CheckArtist(songVM.ArtistName))
            {
                ModelState.AddModelError("ArtistName", "An artist with this name doesn't exist");
            }
            if (songService.CheckAlbum(songVM.AlbumName) && songService.CheckArtist(songVM.ArtistName))
            {
                await songService.UpdateAsync(songVM);
                TempData["done"] = "A Song Has Been Edited Successfully!";
                return RedirectToAction("Index");
            }
            return View(songVM);
        }
        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!songService.CheckId(id))
            {
                return NotFound();
            }
            return View(songService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(string? id)
        {
            var obj = songService.GetById(id);
            if (obj == null)
            {
                return NotFound();
            }
            await songService.DeleteSong(id);
            TempData["done"] = "A Song Has Been Deleted Successfully!";
            return RedirectToAction("Index");

        }
    }
}
