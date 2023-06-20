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
				Length = song.Length,
				Artist = song.Artist,
				Album = song.Album,
				Explicit = song.Explicit,
				CreatorID = song.CreatorID,
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
		public bool CheckId(string Id)
		{
			var song = _db.Songs.Find(Id);
			if (song == null)
				return false;
			return true;
		}
		


		public async Task CreateAsync(SongViewModel model)
		{
			
				Song song = new Song
			{
				Id = Guid.NewGuid().ToString(),
				Title = model.Title,
				Artist = _db.Artists.FirstOrDefault(artist => artist.StageName == model.ArtistName),
				Album = _db.Albums.FirstOrDefault(album => album.Title == model.AlbumName),
				Length = model.Length,
				Explicit = model.Explicit,
				CreatorID = model.CreatorID,
			};


			await _db.Songs.AddAsync(song);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteSong(string id)
		{
			var Db = _db.Songs.FirstOrDefault(x => x.Id == id);
			_db.Songs.Remove(Db);
			await _db.SaveChangesAsync();
		}

		public async Task UpdateAsync(SongViewModel model)
		{
			Song song = _db.Songs.Find(model.Id);

			bool isSongNull = song == null;
			if (isSongNull)
			{
				return;
			}

			song.Title = model.Title;
			song.Artist = _db.Artists.FirstOrDefault(artist => artist.StageName == model.ArtistName);
			song.Album = _db.Albums.FirstOrDefault(album => album.Title == model.AlbumName);
			song.Length = model.Length;
			song.CreatorID = model.CreatorID;
			song.Explicit = model.Explicit;

			_db.Songs.Update(song);
			await _db.SaveChangesAsync();
		}

		public SongViewModel GetById(string id)
		{
			SongViewModel song = _db.Songs
				.Select(song => new SongViewModel
				{
					Id = song.Id,
					Title = song.Title,
					ArtistName = song.Artist.StageName,
					AlbumName = song.Album.Title,
					Length = song.Length,
					Explicit = song.Explicit,
					CreatorID = song.CreatorID,
				}).SingleOrDefault(song => song.Id == id);

			return song;
		}
	}
}
