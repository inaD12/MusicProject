using Music.Data;
using Music.Data.DataModels;
using Music.Services.Interfaces;
using Music.Services.ViewModels;

namespace Music.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly ApplicationDbContext _db;

        public AlbumService(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Album> GetAll()
        {
            return _db.Albums.Select(album => new Album()
            {
                Id = album.Id,
                Title = album.Title,
                Artist = album.Artist,
                TotalTracks = album.TotalTracks,
                RelaseYear = album.RelaseYear,
                Label = album.Label,
                CreatorID = album.CreatorID,
            }).ToList();
        }

        public bool CheckArtist(string name)
        {
            if (_db.Artists.Any(artist => artist.StageName == name))
                return true;
            return false;
        }
        public bool CheckId(string Id)
        {
            var album = _db.Albums.Find(Id);
            if (album == null)
                return false;
            return true;
        }



        public async Task CreateAsync(AlbumViewModel model)
        {
            Album album = new Album
            {
                Id = Guid.NewGuid().ToString(),
                Title = model.Title,
                Artist = _db.Artists.FirstOrDefault(artist => artist.StageName == model.ArtistName),
                CreatorID = model.CreatorID,
                TotalTracks = model.TotalTracks,
                RelaseYear = model.RelaseYear,
                Label = model.Label,
            };

            await _db.Albums.AddAsync(album);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAlbum(string id)
        {
            var Db = _db.Albums.FirstOrDefault(x => x.Id == id);
            _db.Albums.Remove(Db);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(AlbumViewModel model)
        {
            Album album = _db.Albums.Find(model.Id);

            bool isAlbumNull = album == null;
            if (isAlbumNull)
            {
                return;
            }

            album.Title = model.Title;
            album.Artist = _db.Artists.FirstOrDefault(artist => artist.StageName == model.ArtistName);
            album.CreatorID = model.CreatorID;
            album.TotalTracks = model.TotalTracks;
            album.RelaseYear = model.RelaseYear;
            album.Label = model.Label;

            _db.Albums.Update(album);
            await _db.SaveChangesAsync();
        }

        public AlbumViewModel GetById(string id)
        {
            AlbumViewModel album = _db.Albums
                .Select(album => new AlbumViewModel
                {
                    Id = album.Id,
                    Title = album.Title,
                    ArtistName = album.Artist.StageName,
                    CreatorID = album.CreatorID,
                    TotalTracks = album.TotalTracks,
                    RelaseYear = album.RelaseYear,
                    Label = album.Label,
                }).SingleOrDefault(album => album.Id == id);

            return album;
        }
    }
}
