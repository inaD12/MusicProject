using Microsoft.EntityFrameworkCore;
using Music.Data;
using Music.Data.DataModels;
using Music.Services.Interfaces;
using Music.Services.ViewModels;

namespace Music.Services
{
	public class SongService : ISongService
	{
		private readonly ApplicationDbContext _db;

        public SongService(ApplicationDbContext db)
        {
			_db = db;
        }
		public List<SongViewModel> GetAll()
		{
			return _db.Songs.Select(song => new SongViewModel()
			{
				Id = song.Id,
				Title = song.Title,
				Ganre = song.Ganre,
				Length = song.Length,
				Artist = song.Artist,
				ReleaseYear = song.ReleaseYear,
				SongLanguage = song.SongLanguage,
				Album = song.Album,
			}).ToList();
		}


		public async Task CreateAsync(SongViewModel model)
		{
			Song song = new Song();

			song.Id = Guid.NewGuid().ToString();
			song.Title = model.Title;
			song.Artist = _db.Artists.FirstOrDefault(artist => artist.StageName == model.Artist.StageName);
			song.ReleaseYear = model.ReleaseYear;
			song.Album = _db.Albums.FirstOrDefault(album => album.Title == model.Album.Title);
			song.SongLanguage = model.SongLanguage;
			song.Length = model.Length;
			song.Ganre = model.Ganre;
			

			await _db.Songs.AddAsync(song);
			await _db.SaveChangesAsync();
		}

		public Task DeleteSong(string id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(SongViewModel model)
		{
			throw new NotImplementedException();
		}

		public SongViewModel UpdateById(string id)
		{
			throw new NotImplementedException();
		}
	}
}
