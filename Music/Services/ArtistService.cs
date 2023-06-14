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
				FirstName = artist.FirstName,
				LastName = artist.LastName,
				Country = artist.Country,
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
				FirstName = model.FirstName,
				LastName = model.LastName,
				Country = model.Country,
				CreatorID = model.CreatorID,
			};


			await _db.Artists.AddAsync(artist);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteArtist(string id)
		{
			var Db = _db.Artists.FirstOrDefault(x => x.Id == id);
			_db.Artists.Remove(Db);
			await _db.SaveChangesAsync();
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
			artist.FirstName = model.FirstName;
			artist.LastName = model.LastName;
			artist.Country = model.Country;
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
					FirstName = artist.FirstName,
					LastName = artist.LastName,
					Country = artist.Country,
					CreatorID = artist.CreatorID,
				}).SingleOrDefault(artist => artist.Id == id);

			return artist;
		}
	}
}
