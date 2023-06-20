using Music.Data.DataModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Music.Services.ViewModels
{
	public class AlbumViewModel
	{
		public string Id { get; set; }
		[Required(ErrorMessage = "This field Is Required")]
		[DisplayName("Title")]
		public string Title { get; set; }
        [Required(ErrorMessage = "This field Is Required")]
        [DisplayName("Number Of Tracks")]
        public int TotalTracks { get; set; }
        [Required(ErrorMessage = "This field Is Required")]
        [DisplayName("Release Year")]
        public string RelaseYear { get; set; }
        [Required(ErrorMessage = "This field Is Required")]
        public string Label { get; set; }
        [Required(ErrorMessage = "This field Is Required")]
		[DisplayName("Artist")]
		public string ArtistName { get; set; }
		public string? CreatorID { get; set; }
	}
}
