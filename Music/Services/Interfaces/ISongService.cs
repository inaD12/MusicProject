using Music.Services.ViewModels;

namespace Music.Services.Interfaces
{
	public interface ISongService
	{
		Task DeleteSong(string id);
		Task CreateAsync(SongViewModel model);
		SongViewModel UpdateById(string id);
		Task UpdateAsync(SongViewModel model);
	}
}

