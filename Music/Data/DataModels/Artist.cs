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
		public string? CreatorID { get; set; }
	}
}
