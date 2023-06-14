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
		[DisplayName("Artist")]
		public string ArtistName { get; set; }
		public string? CreatorID { get; set; }
	}
}
