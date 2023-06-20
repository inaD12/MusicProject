using Microsoft.AspNetCore.Mvc;
using Music.Data.DataModels;

namespace Music.Services.Interfaces
{
    public interface IApiService
    {
		Task<IActionResult> FetchAlbums();
	}
}
