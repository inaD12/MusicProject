using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music.Data.DataModels
{
    public class Artist
    {
        [Key]
		public string Id { get; set; }
		[DisplayName("Stage Name")]
		public string StageName { get; set; }
		[DisplayName("First Name")]
		public string FirstName { get; set; }
		[DisplayName("Last Name")]
		public string LastName { get; set; }
		[DisplayName("Country Of Origin")]
		public string Country { get; set; }
    }
}
