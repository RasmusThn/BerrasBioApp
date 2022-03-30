#nullable disable
using System.ComponentModel.DataAnnotations;


namespace DataLibrary.Models
{
    /// <summary>
    /// Mostly used for Entity.
    /// FK to SaloonModel, MovieModel and TimeModel
    /// </summary>
    public class ActiveMovieModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int SaloonModelId { get; set; }
        public int MovieModelId { get; set; }
        public int TimeModelId { get; set; }
        [DataType(DataType.Currency)]
        public int Price { get; set; }
        public virtual SaloonModel SaloonModel { get; set; }
        public virtual MovieModel MovieModel { get; set; }
        public virtual TimeModel TimeModel { get; set; }
    }
}
