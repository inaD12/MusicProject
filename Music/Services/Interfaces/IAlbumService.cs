using Music.Data.DataModels;
using Music.Services.ViewModels;

namespace Music.Services.Interfaces
{
	public interface IAlbumService
	{
		Task DeleteAlbum(string id);
		Task CreateAsync(AlbumViewModel model);
		AlbumViewModel GetById(string id);
		Task UpdateAsync(AlbumViewModel model);
	}
}

