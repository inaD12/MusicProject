using Microsoft.AspNetCore.Mvc;
using Music.Data.DataModels;
using Music.Services;
using Music.Services.ViewModels;
using System.Security.Claims;

namespace Music.Controllers
{
	public class AlbumController : Controller
	{
		public AlbumService albumService { get; set; }

		public AlbumController(AlbumService service)
		{
			albumService = service;
		}
		public IActionResult Index()
		{

			List<Album> albums = albumService.GetAll();

			return this.View(albums);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(AlbumViewModel albumVM)
		{
			if (!albumService.CheckArtist(albumVM.ArtistName))
			{
				ModelState.AddModelError("ArtistName", "An artist with this name doesn't exist");
			}
			if (albumService.CheckArtist(albumVM.ArtistName))
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				albumVM.CreatorID = userId;
				await albumService.CreateAsync(albumVM);
				TempData["done"] = "An Album Has Been Created Successfully!";
				return RedirectToAction("Index");
			}
			return View(albumVM);
		}

		public IActionResult Edit(string? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			if (!albumService.CheckId(id))
			{
				return NotFound();
			}
			return View(albumService.GetById(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(AlbumViewModel albumVM)
		{
			if (!albumService.CheckArtist(albumVM.ArtistName))
			{
				ModelState.AddModelError("ArtistName", "An artist with this name doesn't exist");

			}
			if (albumService.CheckArtist(albumVM.ArtistName))
			{
				await albumService.UpdateAsync(albumVM);
				TempData["done"] = "An Album Has Been Edited Successfully!";
				return RedirectToAction("Index");
			}
			return View(albumVM);
		}
		public IActionResult Delete(string? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			if (!albumService.CheckId(id))
			{
				return NotFound();
			}
			return View(albumService.GetById(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePOST(string? id)
		{
			var obj = albumService.GetById(id);
			if (obj == null)
			{
				return NotFound();
			}
			await albumService.DeleteAlbum(id);
			TempData["done"] = "An Album Has Been Deleted Successfully!";
			return RedirectToAction("Index");

		}
	}
}
