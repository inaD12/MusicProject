using Microsoft.EntityFrameworkCore;
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
        public string Ganre { get; set; }
        public double Length { get; set; }
		public Artist Artist { get; set; }
		public int ReleaseYear { get; set; }
        public string SongLanguage { get; set; }
        public Album Album { get; set; }

    }
}
