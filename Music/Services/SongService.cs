using Music.Data;
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
				Artists = song.Artists,
				ReleaseYear = song.ReleaseYear,
				SongLanguage = song.SongLanguage,
				Album = song.Album,
			}).ToList();
		}
		public Task CreateAsync(SongViewModel model)
		{
			throw new NotImplementedException();
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
