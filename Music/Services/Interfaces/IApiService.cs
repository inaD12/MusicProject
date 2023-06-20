using Microsoft.AspNetCore.Mvc;

namespace Music.Services.Interfaces
{
    public interface IApiService
    {
        Task<IActionResult> FetchAlbums();
    }
}
