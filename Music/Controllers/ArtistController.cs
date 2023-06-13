using Microsoft.AspNetCore.Mvc;
using Music.Data.DataModels;
using Music.Services;
using Music.Services.ViewModels;

namespace Music.Controllers
{
	public class ArtistController : Controller
	{
		public ArtistService artistService { get; set; }

		public ArtistController(ArtistService service)
		{
			artistService = service;
		}
		public IActionResult Index()
		{

			List<Artist> artists = artistService.GetAll();

			return this.View(artists);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ArtistViewModel artistVM)
		{

				await artistService.CreateAsync(artistVM);
				TempData["done"] = "An Artist Has Been Created Successfully!";
				return RedirectToAction("Index");

		}

		public IActionResult Edit(string? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			if (!artistService.CheckId(id))
			{
				return NotFound();
			}
			return View(artistService.GetById(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(ArtistViewModel artistVM)
		{
				await artistService.UpdateAsync(artistVM);
				TempData["done"] = "An Artist Has Been Edited Successfully!";
				return RedirectToAction("Index");
		}
		public IActionResult Delete(string? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			if (!artistService.CheckId(id))
			{
				return NotFound();
			}
			return View(artistService.GetById(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePOST(string? id)
		{
			var obj = artistService.GetById(id);
			if (obj == null)
			{
				return NotFound();
			}
			await artistService.DeleteArtist(id);
			TempData["done"] = "An Artist Has Been Deleted Successfully!";
			return RedirectToAction("Index");

		}
	}
}
