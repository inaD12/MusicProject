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
		[Required(ErrorMessage = "This field Is Required")]
		[DisplayName("First Name")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "This field Is Required")]
		[DisplayName("Last Name")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "This field Is Required")]
		[DisplayName("Country Of Origin")]
		public string Country { get; set; }
	}
}
