using Microsoft.AspNetCore.Mvc;
using Music.Services.Interfaces;
using SpotifyAPI.Web;
using System.Diagnostics;

namespace Music.Controllers
{
    [ApiController]
    [Route("api/albums")]
    public class AlbumsController : ControllerBase
    {
        private readonly IApiService _apiService;

        public AlbumsController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpPost]
        public async Task<IActionResult> FetchAlbums()
        {
            try
            {
                var result = await _apiService.FetchAlbums();
                if (result != null)
                {
                    return Ok("Albums fetched and stored in the database.");
                }
                else
                {
                    return StatusCode(500, "An error occurred while fetching albums. Please try again later.");
                }
            }
            catch (APIException ex)
            {
                Debug.WriteLine("APIException occurred while fetching albums from Spotify API:");
                Debug.WriteLine(ex.ToString());

                return StatusCode(500, "An error occurred while fetching albums. Please try again later.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unexpected error occurred while fetching albums:");
                Debug.WriteLine(ex.ToString());

                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
