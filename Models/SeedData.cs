using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCBookStore.Areas.Identity.Data;
using MVCBookStore.Data;
using MVCBookStore.Models;

namespace BookStore.Models
{
    public class SeedData
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<MVCBookStoreUser>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;
            //Add Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }
            MVCBookStoreUser user = await UserManager.FindByEmailAsync("admin@mvcbookstore.com");
            if (user == null)
            {
                var User = new MVCBookStoreUser();
                User.Email = "admin@mvcbookstore.com";
                User.UserName = "admin@mvcbookstore.com";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { await UserManager.AddToRoleAsync(User, "Admin"); }

            }

            var roleCheck1 = await RoleManager.RoleExistsAsync("User");
            if (!roleCheck1) { roleResult = await RoleManager.CreateAsync(new IdentityRole("User")); }
            MVCBookStoreUser user1 = await UserManager.FindByEmailAsync("bobi.j@mvcbookstore.com");
            if (user1 == null)
            {
                var User = new MVCBookStoreUser();
                User.Email = "bobi.j@mvcbookstore.com";
                User.UserName = "bobi.j@mvcbookstore.com";
                string userPWD = "BobiJ123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { await UserManager.AddToRoleAsync(User, "User"); }

            }


        }


        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MVCBookStoreContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<MVCBookStoreContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();

                // Look for any books
                if (context.Books.Any() || context.Author.Any())
                {
                    return; // DB has been seeded
                }

                context.SaveChanges();


                context.Author.AddRange(
                new Author
                {
                    //Id = 1,
                    FirstName = "Joanne ",
                    LastName = "Rowling",
                    BirthDate = DateTime.Parse("1965-7-31"),
                    Nationality = "British",
                    Gender = "female",
                    Books = new List<Books>
                    {
                        new Books { 
                
                            //Id = 1,
                            Title = "Harry Potter and the Philosopher's Stone",
                            YearPublished = 1997,
                            NumPages = 223,
                            Description = "It is a story about Harry Potter, an orphan brought up by his aunt and uncle because his parents were killed when he was a baby. Harry is unloved by his uncle and aunt but everything changes when he is invited to join Hogwarts School of Witchcraft and Wizardry and he finds out he's a wizard.",
                            Publisher = "Bloomsbury",
                            FrontPage = "",
                            DownloadUrl = "",
                            AuthorId = 1,
                            Reviews = new List<Review>
                            {
                                new Review { BooksId = 1, AppUser = "bobi123@gmail.com", Comment = "Super", Rating = 9 },
                                new Review { BooksId = 1, AppUser = "deni456@gmail.com", Comment = "Amazing book", Rating = 10 }
                            },

                            UserBooks = new List<UserBooks>
                            {
                                new UserBooks { BooksId = 1, AppUser = "bobi123@gmail.com" },
                                new UserBooks { BooksId = 1, AppUser = "deni456@gmail.com" }
                            }
                        },

                        new Books {
                            //Id = 2,
                            Title = "Harry Potter and the Order of the Phoenix",
                            YearPublished = 2003,
                            NumPages = 766,
                            Description = "It follows Harry Potter's struggles through his fifth year at Hogwarts School of Witchcraft and Wizardry, including the surreptitious return of the antagonist Lord Voldemort, O.W.L. exams, and an obstructive Ministry of Magic.",
                            Publisher = "Bloomsbury",
                            FrontPage = "",
                            DownloadUrl = "",
                            AuthorId = 1,
                            Reviews = new List<Review>
                            {
                                new Review { BooksId = 2, AppUser = "bobi123@gmail.com", Comment = "Didn't like this book very much", Rating = 5 }
                            },
                            UserBooks = new List<UserBooks>
                            {
                                new UserBooks { BooksId = 2, AppUser = "bobi123@gmail.com" }
                            }
                        }

                    }   
                },

                new Author
                {   //Id = 2, 
                    FirstName = "Leo",
                    LastName = "Tolstoy",
                    BirthDate = DateTime.Parse("1828-9-9"),
                    Nationality = "Russian",
                    Gender = "male",
                    Books = new List<Books>
                    {
                        new Books
                        {
                            //Id = 3,
                            Title = "Anna Karenina",
                            YearPublished = 1878,
                            NumPages = 864,
                            Description = "The story centers on an extramarital affair between Anna and dashing cavalry officer Count Alexei Kirillovich Vronsky that scandalizes the social circles of Saint Petersburg and forces the young lovers to flee to Italy in a search for happiness, but after they return to Russia, their lives further unravel.",
                            Publisher = "The Russian Messenger",
                            FrontPage = "",
                            DownloadUrl = "",
                            AuthorId = 2,

                            Reviews = new List<Review>
                            {
                                new Review { BooksId = 3, AppUser = "deni456@gmail.com", Comment = "Amazing book", Rating = 10 }
                            },

                            UserBooks = new List<UserBooks>
                            {
                                new UserBooks { BooksId = 3, AppUser = "deni456@gmail.com" }
                            }
                        }
                    }
                });
                context.SaveChanges();
                context.Genre.AddRange(
                new Genre { /*Id = 1, */GenreName = "fantasy", },
                new Genre { /*Id = 2, */GenreName = "adventure", },
                new Genre { /*Id = 3, */GenreName = "drama", },
                new Genre { /*Id = 4, */GenreName = "novel", }
                );
                context.SaveChanges();
                context.BookGenre.AddRange(
                new BookGenre { /*Id = 1, */ GenreId = 1, BooksId = 1 },
                new BookGenre { /*Id = 2, */ GenreId = 2, BooksId = 1 },
                new BookGenre { /*Id = 3, */ GenreId = 1, BooksId = 2 },
                new BookGenre { /*Id = 4, */ GenreId = 2, BooksId = 2 },
                new BookGenre { /*Id = 5, */ GenreId = 3, BooksId = 3 },
                new BookGenre { /*Id = 6, */ GenreId = 4, BooksId = 3 }
                );
                context.SaveChanges();
            }
        }
    }
}