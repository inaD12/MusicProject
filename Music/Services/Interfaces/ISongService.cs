using Music.Data.DataModels;
using Music.Services.ViewModels;

namespace Music.Services.Interfaces
{
	public interface ISongService
	{
		Task DeleteSong(string id);
		Task CreateAsync(SongViewModel model);
		SongViewModel GetById(string id);
		Task UpdateAsync(SongViewModel model);
	}
}

