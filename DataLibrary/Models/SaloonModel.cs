#nullable disable
using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models
{
    /// <summary>
    /// Mostly used for Entity and seedData.
    /// FK to SeatModel.
    /// </summary>
    public class SaloonModel
    {
      
        public int Id { get; set; }
        [Display(Name = "Saloon")]
        public string Name { get; set; }
        public int NumberOfSeats { get; set; }

        [Display(Name = "Available Seats")]
        public int AvailableSeats { get; set; }
            
        
        public ICollection<SeatModel> Seats { get; set; }

        public SaloonModel()
        {

        }

        /// <summary>
        /// Sets numberOfSeats to NumberOfSeats and AvailableSeats
        /// </summary>
        /// <param name="name"></param>
        /// <param name="numberOfSeats"></param>
        public SaloonModel(string name, int numberOfSeats)
        {
            Name = name;
            NumberOfSeats = numberOfSeats;
            AvailableSeats = numberOfSeats;
        }

    }
}