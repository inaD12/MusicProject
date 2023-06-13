using Music.Data.DataModels;
using Music.Services.ViewModels;

namespace Music.Services.Interfaces
{
	public interface IArtistService
	{
		Task DeleteArtist(string id);
		Task CreateAsync(ArtistViewModel model);
		ArtistViewModel GetById(string id);
		Task UpdateAsync(ArtistViewModel model);
	}
}

