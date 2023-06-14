using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music.Data.DataModels
{
    public class Album
    {
        [Key]
		public string Id { get; set; }
		[DisplayName("Title")]
		public string Title { get; set; }
		[DisplayName("Artist")]
		public Artist Artist { get; set; }
		public string? CreatorID { get; set; }
	}
}
