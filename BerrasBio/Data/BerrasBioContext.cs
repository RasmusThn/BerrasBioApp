#nullable disable
using Microsoft.EntityFrameworkCore;
using DataLibrary.Models;

namespace BerrasBio.Data
{
    public class BerrasBioContext : DbContext
    {      
        public BerrasBioContext (DbContextOptions<BerrasBioContext> options)
            : base(options)
        {
        }

        public DbSet<BookingModel> BookingModels { get; set; }
        public DbSet<ActiveMovieModel> ActiveMovieModels { get; set; }
        public DbSet<MovieModel> MovieModels { get; set; }
        public DbSet<SaloonModel> SaloonModels { get; set; }
        public DbSet<SeatModel> SeatModels { get; set; }
        public DbSet<TimeModel> TimeModels { get; set; }


    }
}
