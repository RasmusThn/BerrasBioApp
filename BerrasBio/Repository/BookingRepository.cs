#nullable disable
using BerrasBio.Data;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BerrasBio.Repository

{
    public class BookingRepository 
    {
        private readonly BerrasBioContext _db;
        public BookingRepository(BerrasBioContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Returns the current ActiveMovieModel by passing in int
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActiveMovieModel</returns>
        public ActiveMovieModel GetActiveModel(int? id)
        {
            ActiveMovieModel activeId = _db.ActiveMovieModels.FirstOrDefault(x => x.Id == id);
            return activeId;
        }
        /// <summary>
        /// Returns the current MovieModel by passing in int
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MovieModel</returns>
        public MovieModel GetMovieModel(int? id)
        {
            MovieModel activeId = _db.MovieModels.FirstOrDefault(x => x.Id == id);
            return activeId;
        }
        /// <summary>
        /// Returns the current TimeModel by passing in int
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TimeModel</returns>
        public TimeModel GetTimeModel(int? id)
        {
            TimeModel activeId = _db.TimeModels.FirstOrDefault(x => x.Id == id);
            return activeId;
        }
        /// <summary>
        /// Returns the current SaloonModel by passing in int
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>SaloonModel</returns>
        public SaloonModel GetSaloonModel(int? id)
        {
            SaloonModel activeId = _db.SaloonModels.FirstOrDefault(x => x.Id == id);
            return activeId;
        }
        /// <summary>
        /// Returns the current SaloonModel by passing in ActiveMovieModel
        /// </summary>
        /// <param name="ActiveMovieModel"></param>
        /// <returns>SaloonModel</returns>
        public SaloonModel GetActiveSaloon(ActiveMovieModel ActiveMovieModel)
        {
            int saloonId = ActiveMovieModel.SaloonModelId;
            var saloon = _db.SaloonModels.FirstOrDefault(x => x.Id == saloonId);

            return saloon;
        }
        /// <summary>
        /// Finds the first SeatModel with: isBooked == false
        /// </summary>
        /// <returns>SeatModel</returns>
        public SeatModel FindUnBookedSeat()
        {
            var seatToUpdate = _db.SeatModels.First(x => x.IsBooked == false);

            return seatToUpdate;
        }
        /// <summary>
        /// Finds the SeatModel's Id with the matching passed in INT
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of SeatModel</returns>
        public List<SeatModel> FindSeatModel(int id)
        {
            var seatModel = _db.SeatModels
                .Where(x => x.BookingModelId == id)
                .ToList();

            return seatModel;
        }
        /// <summary>
        /// Returns the View for a 1 BookingModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Task of BookingModel</returns>
        public Task<BookingModel> GetSingleBookingModel(int? id)
        {
            var bookingModel = _db.BookingModels
                .Include(b => b.ActiveMovieModel)
                .ThenInclude(a => a.MovieModel)
                .Include(s => s.ActiveMovieModel)
                .ThenInclude(b => b.SaloonModel)
                .FirstOrDefaultAsync(m => m.Id == id);

            return bookingModel;
        }
        /// <summary>
        /// Returns the View for all BookingModels
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A List of BookingModel's</returns>
        public List<BookingModel> GetAllBookingViewModel()
        {
            var checkBookings = _db.BookingModels
                .Include(m => m.ActiveMovieModel)
                .ThenInclude(a => a.MovieModel)
                .Include(s => s.ActiveMovieModel)
                .ThenInclude(b => b.SaloonModel)
                .ToList();
            return checkBookings;
        }
        /// <summary>
        /// Returns True if the passed in INT is NULL
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true or false</returns>
        public bool CheckIfNull(int? id)
        {
            if (id == null)
            {
                return true;
            }
            return false;
        }
        
    }
}
