using Music.Data.DataModels;
using Music.Services.ViewModels;

namespace Music.Services.Interfaces
{
    public interface IAlbumService
    {

        List<Album> GetAll();
        bool CheckArtist(string name);
        bool CheckId(string Id);
        Task DeleteAlbum(string id);
        Task CreateAsync(AlbumViewModel model);
        AlbumViewModel GetById(string id);
        Task UpdateAsync(AlbumViewModel model);
    }
}

