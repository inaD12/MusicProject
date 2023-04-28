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
		public List<Song> GetAll()
		{
			return _db.Songs.Select(song => new Song()
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

		public bool CheckAlbum(string name)
		{
			if (_db.Albums.Any(album => album.Title == name))
				return true;
			return false;
		}
		public bool CheckArtist(string name)
		{
			if (_db.Artists.Any(artist => artist.StageName == name))
				return true;
			return false;
		}


		public async Task CreateAsync(SongViewModel model)
		{
			Song song = new Song
			{
				Id = Guid.NewGuid().ToString(),
				Title = model.Title,
				Artist = _db.Artists.FirstOrDefault(artist => artist.StageName == model.ArtistName),
				ReleaseYear = model.ReleaseYear,
				Album = _db.Albums.FirstOrDefault(album => album.Title == model.AlbumName),
				SongLanguage = model.SongLanguage,
				Length = model.Length,
				Ganre = model.Ganre
			};


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
