using Microsoft.EntityFrameworkCore;
using DataLibrary.Models;
using BerrasBio.Data;

namespace BerrasBioApp.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using BerrasBioContext? context = new(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BerrasBioContext>>());

            // Kollar om det finns filmer i DB redan.
            if (context.ActiveMovieModels.Any())
            {
                return;   
            }
            #region AddSaloons
            context.SaloonModels.Add(new SaloonModel("Saloon 1", 100)); // 1 Dune
            context.SaloonModels.Add(new SaloonModel("Saloon 1", 100)); // 2 The Batman
            context.SaloonModels.Add(new SaloonModel("Saloon 1", 100)); // 3 Avatar2
            context.SaloonModels.Add(new SaloonModel("Saloon 2", 50)); // 4 
            context.SaloonModels.Add(new SaloonModel("Saloon 1", 100)); // 5
            context.SaloonModels.Add(new SaloonModel("Saloon 2", 50));  // 6

            context.SaveChanges();
            #endregion
            #region AddSeats
            for (int i = 1; i <= 100; i++) // 1
            {
                context.SeatModels.Add(new SeatModel(i, 1)); 

            }
            for (int i = 1; i <= 100; i++) // 2
            {
                context.SeatModels.Add(new SeatModel(i, 2)); 
            }
            for (int i = 1; i <= 100; i++) // 3
            {
                context.SeatModels.Add(new SeatModel(i, 3)); 
            }
            for (int i = 1; i <= 50; i++) // 4
            {
                context.SeatModels.Add(new SeatModel(i, 4)); 
            }
            for (int i = 1; i <= 100; i++) // 5
            {
                context.SeatModels.Add(new SeatModel(i, 5)); 
            }
            for (int i = 1; i <= 50; i++) // 6
            {
                context.SeatModels.Add(new SeatModel(i, 6)); 
            }
            context.SaveChanges();
            #endregion
            #region AddMovies
            context.MovieModels.AddRange(
                new MovieModel
                {
                    Title = "Dune",
                    ReleaseDate = DateTime.Parse("2021 - 10 - 12"),
                    Genre = "Science Fiction",
                    IsActive = true,
                    ImgSrc = "/Content/Images/Dune.jpg"
                },

                new MovieModel
                {
                    Title = "The Batman",
                    ReleaseDate = DateTime.Parse("2022 - 3 - 2"),
                    Genre = "Action",
                    IsActive = true,
                    ImgSrc ="/Content/Images/thebatman.jpg"
                },

                new MovieModel
                {
                    Title = "Avatar 2",
                    ReleaseDate = DateTime.Parse("2022 - 12 - 14"),
                    Genre = "Science Fiction",
                    IsActive = false,
                    ImgSrc = "/Content/Images/Avatar2.jpg"

                }); ;
            context.SaveChanges();
            #endregion
            #region AddTimes
            string todayTime = DateTime.Now.ToString("yyyy/MM/dd");
            string nextDay = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd");
            
            context.TimeModels.AddRange(
                new TimeModel 
                {
                    ShowTime = DateTime.Parse($"{todayTime} 15:00") // 1
                },
                new TimeModel 
                {
                    ShowTime = DateTime.Parse($"{todayTime} 18:00") // 2
                },
                new TimeModel 
                {
                    ShowTime = DateTime.Parse($"{todayTime} 20:30") // 3
                },
                new TimeModel 
                {
                    ShowTime = DateTime.Parse($"{nextDay} 18:00") // 4
                },
                new TimeModel 
                {
                    ShowTime = DateTime.Parse($"{nextDay} 20:30") // 5
                },
                new TimeModel 
                {
                    ShowTime = DateTime.Parse("2022-12-14 20:30") // 6
                });
            context.SaveChanges();
            #endregion
            #region AddActiveMovieModels
            context.ActiveMovieModels.AddRange(
                new ActiveMovieModel
                {
                    SaloonModelId = 4, // 50
                    MovieModelId = 1,  // Dune
                    TimeModelId = 1,   //15:00
                    Price = 85
                },
                new ActiveMovieModel
                {
                    SaloonModelId = 2,  // 100
                    MovieModelId = 2,   // The Batman
                    TimeModelId = 2,    // 18:00
                    Price = 100
                },
                new ActiveMovieModel
                {
                    SaloonModelId = 1,  // 100
                    MovieModelId = 1,   // Dune
                    TimeModelId = 4,    // Next18:00
                    Price = 100
                },
                new ActiveMovieModel
                {
                    SaloonModelId = 6,  // 50
                    MovieModelId = 1,   // Dune
                    TimeModelId = 5,    // Next20:30
                    Price = 105
                },
                new ActiveMovieModel
                {
                    SaloonModelId = 5,  // 50
                    MovieModelId = 2,   // The Batman
                    TimeModelId = 5,    // Next20:30
                    Price = 110
                },
                new ActiveMovieModel
                {
                    SaloonModelId = 3,  // 100
                    MovieModelId = 3,   // Avatar
                    TimeModelId = 6,    // 14 december
                    Price = 150
                });

            context.SaveChanges();
            #endregion
        }
    }
}
        