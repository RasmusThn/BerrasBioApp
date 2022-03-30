using BerrasBio.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerrasBio.Controllers
{
    public class ActiveMovieController : Controller
    {
        private readonly BerrasBioContext _db;


        public ActiveMovieController(BerrasBioContext db)
        {
            _db = db;
        }

        
        public async Task<ActionResult<List<ActiveMovieModel>>> ListView()
        {

            var checkBookings = _db.ActiveMovieModels
                .Include(s => s.SaloonModel)             
                .Include(m => m.MovieModel)
                .Include(t => t.TimeModel);

            return View(await checkBookings.ToListAsync());
        }


    }
}
