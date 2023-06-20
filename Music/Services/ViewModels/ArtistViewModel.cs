using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Music.Services.ViewModels
{
	public class ArtistViewModel
	{
		public string? Id { get; set; }
		[Required(ErrorMessage = "This field Is Required")]
		[DisplayName("Stage Name")]
		public string StageName { get; set; }
		public string? CreatorID { get; set; }
	}
}
