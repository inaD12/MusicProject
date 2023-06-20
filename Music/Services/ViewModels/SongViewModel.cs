using Music.Data.DataModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Music.Services.ViewModels
{
	public class SongViewModel
	{
		public string? Id { get; set; }
		[Required(ErrorMessage = "This field Is Required")]
		public string Title { get; set; }
		[Required(ErrorMessage = "This field Is Required")]
		public double Length { get; set; }
        [Required(ErrorMessage = "This field Is Required")]
        public bool Explicit { get; set; }
		[Required(ErrorMessage = "This field Is Required")]
        [DisplayName("Artist Name")]
        public string ArtistName { get; set; }
		[Required(ErrorMessage = "This field Is Required")]
		[DisplayName("Album Name")]
		public string AlbumName { get; set; }
		public string? CreatorID { get; set; }
	}
}
