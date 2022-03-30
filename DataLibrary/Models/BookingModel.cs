using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLibrary.Models
{
    /// <summary>
    /// Mostly used for Entity and seedData.
    /// FK to ActiveMovieModel
    /// </summary>
    public class BookingModel
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }
        public string EmailAddress { get; set; }
        public int ActiveMovieModelId { get; set; }       
        [Range(1, 12, ErrorMessage = "Minimum: 1 ticket. Maximum 12 tickets")]
        [Display(Name = "Tickets")]
        public int BookedTickets { get; set; }
        public virtual ActiveMovieModel? ActiveMovieModel { get; set; }

    }
}