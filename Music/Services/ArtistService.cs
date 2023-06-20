using Microsoft.EntityFrameworkCore;
using Music.Data;
using Music.Data.DataModels;
using Music.Services.Interfaces;
using Music.Services.ViewModels;
using System;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;

namespace Music.Services
{
	public class ArtistService : IArtistService
	{
		private readonly ApplicationDbContext _db;

        public ArtistService(ApplicationDbContext db)
        {
			_db = db;
        }
		public List<Artist> GetAll()
		{
			return _db.Artists.Select(artist => new Artist()
			{
				Id = artist.Id,
				StageName = artist.StageName,
				CreatorID = artist.CreatorID,
			}).ToList();
		}

		public bool CheckId(string Id)
		{
			var artist = _db.Artists.Find(Id);
			if (artist == null)
				return false;
			return true;
		}
		


		public async Task CreateAsync(ArtistViewModel model)
		{
			Artist artist = new Artist
			{
				Id = Guid.NewGuid().ToString(),
				StageName = model.StageName,
				CreatorID = model.CreatorID,
			};


			await _db.Artists.AddAsync(artist);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteArtist(string id)
		{
			var artist = await _db.Artists.FindAsync(id);

			if (artist != null)
			{
				var albums = _db.Albums.Where(a => a.Artist.Id == id);

				foreach (var album in albums)
				{
					_db.Albums.Remove(album);
				}

				_db.Artists.Remove(artist);

				await _db.SaveChangesAsync();
			}
		}

		public async Task UpdateAsync(ArtistViewModel model)
		{
			Artist artist = _db.Artists.Find(model.Id);

			bool isArtistNull = artist == null;
			if (isArtistNull)
			{
				return;
			}

			artist.StageName = model.StageName;
			artist.CreatorID = model.CreatorID;

			_db.Artists.Update(artist);
			await _db.SaveChangesAsync();
		}

		public ArtistViewModel GetById(string id)
		{
			ArtistViewModel artist = _db.Artists
				.Select(artist => new ArtistViewModel
				{
					Id = artist.Id,
					StageName = artist.StageName,
					CreatorID = artist.CreatorID,
				}).SingleOrDefault(artist => artist.Id == id);

			return artist;
		}
	}
}
