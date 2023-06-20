using Music.Data.DataModels;
using Music.Services.ViewModels;

namespace Music.Services.Interfaces
{
    public interface ISongService
    {
        List<Song> GetAll();
        bool CheckAlbum(string name);
        bool CheckArtist(string name);
        bool CheckId(string Id);
        Task DeleteSong(string id);
        Task CreateAsync(SongViewModel model);
        SongViewModel GetById(string id);
        Task UpdateAsync(SongViewModel model);
    }
}

