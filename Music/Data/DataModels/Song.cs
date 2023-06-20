using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music.Data.DataModels
{
    public class Song
    {
        [Key]
		public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        public double Length { get; set; }
		public Artist Artist { get; set; }
		[Required]
        [DisplayName("Album Name")]
		public Album Album { get; set; }
        public bool Explicit { get; set; }
        public string? CreatorID { get; set; }

    }
}
