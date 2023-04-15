using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music.Data.DataModels
{
    public class Album
    {
        [Key]
		public string Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
