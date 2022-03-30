using BerrasBio.Data;
using Microsoft.AspNetCore.Mvc;
using BerrasBioApp.Models;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using BerrasBio.Repository;

namespace BerrasBio.Controllers
{
    public class BookingController : Controller
    {
       
        private readonly BerrasBioContext _db;
        private readonly BookingRepository _bookingRepository;

        public BookingController(BerrasBioContext db )
        {
            _db = db;
            _bookingRepository = new BookingRepository(db);
        }
        
        //GET
        [HttpGet]     
        public ActionResult Create(int? activeMovieModelId)
        {
            // Checks if Id is null
            if (_bookingRepository.CheckIfNull(activeMovieModelId)){return NotFound();}
    
            GetActiveModelAndViewBags(activeMovieModelId);
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,EmailAddress,ActiveMovieModelId,bookedTickets")] int? activeMovieModelId, BookingModel bookingModels)
        {
            // Checks if Id is null
            if (_bookingRepository.CheckIfNull(activeMovieModelId)){return NotFound();}

            if (ModelState.IsValid)
            {
            
                ActiveMovieModel activeId = _bookingRepository.GetActiveModel(activeMovieModelId);
                SaloonModel saloon = _bookingRepository.GetActiveSaloon(activeId);
               
                bool isFull = CheckAvailableSeats(saloon.AvailableSeats, bookingModels.BookedTickets);
                if (isFull){return View();}

                saloon.AvailableSeats -= bookingModels.BookedTickets;

                _db.BookingModels.Add(bookingModels);
                _db.SaveChanges();
                
                UpdateSeats(bookingModels);
                _db.SaveChanges();
                
                return RedirectToAction("ListView", "ActiveMovie");
            }

            GetActiveModelAndViewBags(activeMovieModelId);
            return View(); //Få den att returna rätt
        }

        //GET
        [HttpGet]
        public  ActionResult<List<BookingViewModel>> ListView()
        {

            List<BookingModel> checkBookings = _bookingRepository.GetAllBookingViewModel();

            return View(checkBookings);
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Delete(int? id) 
        {
            // Checks if Id is null
            if (_bookingRepository.CheckIfNull(id)) { return NotFound(); }

            Task<BookingModel> bookingModel = _bookingRepository.GetSingleBookingModel(id);
            if (bookingModel == null){return NotFound();}

            return View(await bookingModel);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingModel = await _db.BookingModels.FindAsync(id);

            AddSeatWhenDelete(bookingModel);
            RemoveSeats(id);
            _db.BookingModels.Remove(bookingModel);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(ListView));
        }

        [HttpGet]
        public ActionResult DeleteAll()
        {
            var bookingList = _db.BookingModels.ToList();
            if (bookingList.Count == 0)
            {
                return RedirectToAction(nameof(ListView));
            }
        
            foreach (var booking in bookingList)
            {
                _db.Remove(booking);
            }

            var seatReset = _db.SeatModels.ToList();

            foreach (var seat in seatReset)
            {
                seat.IsBooked = false;
                seat.BookingModelId = -1;
            }

            var saloons = _db.SaloonModels.ToList();

            foreach (var saloon in saloons)
            {
                saloon.AvailableSeats = saloon.NumberOfSeats;
            }

            _db.SaveChanges();

            return RedirectToAction(nameof(ListView));
        }

        #region Methods
        /// <summary>
        /// Updates the passed in BookingModels Seats.IsBooked to true and 
        /// Seats.BookingModelId to the same id as BookingModel.Id
        /// </summary>
        /// <param name="bookingModels"></param>
        private void UpdateSeats(BookingModel bookingModels)
        {
            for (int i = 1; i <= bookingModels.BookedTickets; i++)
            {
                SeatModel seatToUpdate = _bookingRepository.FindUnBookedSeat();
                seatToUpdate.IsBooked = true;
                seatToUpdate.BookingModelId = bookingModels.Id;

            }
        }
        /// <summary>
        /// If AvailibleSeats is bigger than bookedTickes, Returns true with Viewbag.Full Message
        /// </summary>
        /// <param name="availableSeats"></param>
        /// <param name="bookedTickets"></param>
        /// <returns>true or false</returns>
        private bool CheckAvailableSeats(int availableSeats, int bookedTickets)
        {
            //Checks if Saloon is full
            if (availableSeats < bookedTickets)
            {
                ViewBag.Full = $"Full, there is only {availableSeats} left ";
                return true;
            }
            return false;
        }
        /// <summary>
        /// Adds the number of bookedTickes back to saloon.AvailableSeats
        /// </summary>
        /// <param name="bookingModel"></param>
        public void AddSeatWhenDelete(BookingModel bookingModel)
        {
            var saloonId = bookingModel.ActiveMovieModelId;

            SaloonModel saloon = _bookingRepository.GetSaloonModel(saloonId);

            saloon.AvailableSeats += bookingModel.BookedTickets;
        }
        /// <summary>
        /// Changes SeatModel.isBooked back to false for all SeatModels.Id = id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveSeats(int id)
        {
            List<SeatModel> seatModelList = _bookingRepository.FindSeatModel(id);

            foreach (var item in seatModelList)
            {
                item.IsBooked = false;
                item.BookingModelId = -1;
            }
        }
        /// <summary>
        /// Creates TempData with Price info,
        /// Viewbag for MovieModel,
        /// Viewbag for TimeModel and
        /// Viewbag for SaloonModel
        /// </summary>
        /// <param name="activeMovieModelId"></param>
        private void GetActiveModelAndViewBags(int? activeMovieModelId)
        {
            ActiveMovieModel activeModel = _bookingRepository.GetActiveModel(activeMovieModelId);
            TempData["price"] = activeModel.Price;
            ViewBag.Movie = _bookingRepository.GetMovieModel(activeModel.MovieModelId);
            ViewBag.Time = _bookingRepository.GetTimeModel(activeModel.TimeModelId);
            ViewBag.Saloon = _bookingRepository.GetSaloonModel(activeModel.SaloonModelId);
        }
        #endregion
    }
}
