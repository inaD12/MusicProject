using Microsoft.EntityFrameworkCore;
using Music.Data;
using Music.Data.DataModels;
using Music.Services.Interfaces;
using Music.Services.ViewModels;
using System;

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
			song.ReleaseYear = model.ReleaseYear;
			song.Album = _db.Albums.FirstOrDefault(album => album.Title == model.AlbumName);
			song.SongLanguage = model.SongLanguage;
			song.Length = model.Length;
			song.Ganre = model.Ganre;

			_db.Songs.Update(song);
			await _db.SaveChangesAsync();
		}

		public SongViewModel UpdateById(string id)
		{
			SongViewModel song = _db.Songs
				.Select(song => new SongViewModel
				{
					Id = song.Id,
					Title = song.Title,
					ArtistName = song.Artist.StageName,
					ReleaseYear = song.ReleaseYear,
					AlbumName = song.Album.Title,
					SongLanguage = song.SongLanguage,
					Length = song.Length,
					Ganre = song.Ganre
				}).SingleOrDefault(movie => movie.Id == id);

			return song;
		}
	}
}
