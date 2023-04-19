using Music.Data.DataModels;
using System.ComponentModel.DataAnnotations;

namespace Music.Services.ViewModels
{
	public class SongViewModel
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Ganre { get; set; }
		public double Length { get; set; }
		public Artist Artist { get; set; }
		public int ReleaseYear { get; set; }
		public string SongLanguage { get; set; }
		public Album Album { get; set; }
	}
}
