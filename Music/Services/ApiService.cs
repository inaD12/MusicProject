using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using Music.Data;
using Music.Data.DataModels;
using Music.Services.Interfaces;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Music.Services
{
	public class ApiService : IApiService
	{
		private readonly ApplicationDbContext _dbContext;
		private SpotifyClient _spotifyClient;

		public ApiService(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		static async Task<AccessToken> GetToken()
		{
			Console.WriteLine("Getting Token");
			string clientId = "22378a8c10fd4b0aa5d8ec467c3408db";
			string clientSecret = "93cff7beffe1430e9e4a3d42fae2547a";
			string credentials = String.Format("{0}:{1}", clientId, clientSecret);

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

				List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
				requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

				FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

				var request = await client.PostAsync("https://accounts.spotify.com/api/token", requestBody);
				var response = await request.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<AccessToken>(response);
			}
		}

		public async Task<IActionResult> FetchAlbums()
		{
			const int targetAlbumCount = 50;
			var albums = new List<Album>();
			var fetchedAlbumIds = new HashSet<string>();

			var accessToken = await GetToken();

			var config = SpotifyClientConfig.CreateDefault()
				.WithToken(accessToken.access_token);
			_spotifyClient = new SpotifyClient(config);

			while (albums.Count < targetAlbumCount)
			{
				var albumItems = await FetchRandomAlbums(targetAlbumCount - albums.Count);

				foreach (var albumItem in albumItems)
				{
					if (fetchedAlbumIds.Contains(albumItem.Id))
						continue;

					var artistName = albumItem.Artists[0]?.Name;

					if (string.IsNullOrEmpty(artistName))
						continue;

					var artist = new Artist
					{
						Id = Guid.NewGuid().ToString(),
						StageName = artistName
					};
					await _dbContext.Artists.AddAsync(artist);
					await _dbContext.SaveChangesAsync();

					
					var album = new Album
					{
						Id = Guid.NewGuid().ToString(),
						Title = albumItem.Name,
						TotalTracks = albumItem.TotalTracks,
						RelaseYear = albumItem.ReleaseDate,
						Label = albumItem.Label,
						Artist = _dbContext.Artists.FirstOrDefault(a => a.StageName == artistName),
					};

					
					if (album.Title == null || album.RelaseYear == null || album.Label == null || album.Artist == null)
						continue;

					await _dbContext.Albums.AddAsync(album);
					await _dbContext.SaveChangesAsync();

					foreach (var trackItem in albumItem.Tracks.Items)
					{
						var trackArtistName = trackItem.Artists[0]?.Name;

						if (string.IsNullOrEmpty(trackArtistName))
							continue;

						var song = new Song
						{
							Id = Guid.NewGuid().ToString(),
							Title = trackItem.Name,
							Length = Math.Round((double)trackItem.DurationMs / 60000, 2),
							Artist = _dbContext.Artists.FirstOrDefault(a => a.StageName == trackArtistName),
							Album = _dbContext.Albums.FirstOrDefault(a => a.Title == album.Title),
							Explicit = trackItem.Explicit,
						};

						
						if (song.Title == null || song.Length == null || song.Artist == null || song.Album == null)
							continue;

						await _dbContext.Songs.AddAsync(song);
						await _dbContext.SaveChangesAsync();
					}

					albums.Add(album);
					fetchedAlbumIds.Add(albumItem.Id);

					if (albums.Count >= targetAlbumCount)
						break;
				}
			}

			return null;
		}

		private async Task<List<FullAlbum>> FetchRandomAlbums(int count)
		{
			var newReleases = await _spotifyClient.Browse.GetNewReleases();
			var albumItems = newReleases.Albums.Items
				.Take(count)
				.ToList();

			var albumRequest = new AlbumsRequest(albumItems.Select(album => album.Id).ToList());
			var albumDetails = await _spotifyClient.Albums.GetSeveral(albumRequest);

			return albumDetails.Albums;
		}
	}
}