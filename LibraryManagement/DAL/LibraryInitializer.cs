using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LibraryManagement.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace LibraryManagement.DAL
{
    public class LibraryInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<LibraryContext>
    {
        protected override void Seed(LibraryContext context)
        {


            var loanTypes = new List<LoanType>
            {
                new LoanType { loan_type_name = "1 Day", duration = 1 },
                new LoanType { loan_type_name = "3 Days", duration = 3 },
                new LoanType { loan_type_name = "A Week", duration = 7 },
                new LoanType { loan_type_name = "Fortnightly", duration = 15 },
                new LoanType { loan_type_name = "A Month", duration = 30 },
                new LoanType { loan_type_name = "2 Months", duration = 60 },
                new LoanType { loan_type_name = "3 Months", duration = 90 },
                new LoanType { loan_type_name = "6 Months", duration = 180 }
            };
            loanTypes.ForEach(t => context.LoanTypes.Add(t));
            context.SaveChanges();

            var membershipTypes = new List<MembershipType>
            {
                new MembershipType { membership_type_name = "Donor", books_allowed = 20},
                new MembershipType { membership_type_name = "Full Member", books_allowed = 10},
                new MembershipType { membership_type_name = "Premium Member", books_allowed = 7},
                new MembershipType { membership_type_name = "Member", books_allowed = 5},
                new MembershipType { membership_type_name = "New Member", books_allowed = 3},
                new MembershipType { membership_type_name = "Guest", books_allowed = 1}
            };
            membershipTypes.ForEach(t => context.MembershipTypes.Add(t));
            context.SaveChanges();

            var members = new List<Member>
            {
                new Member { first_name = "Bill", last_name = "Gates", gender = "male", date_of_birth = DateTime.Parse("1984-11-01"), email = "gates@microsoft.com", contact_number = "9842000000", address = "Mountain View, California", membershipTypeID = 1},
                new Member { first_name = "Larry", last_name = "Page", gender = "male", date_of_birth = DateTime.Parse("1970-04-02"), email = "page@gmail.com", contact_number = "9842156324", address = "Paulo Alto, California", membershipTypeID = 2},
                new Member { first_name = "Mark", last_name = "Zukerburg", gender = "male", date_of_birth = DateTime.Parse("1980-06-01"), email = "zuck@fb.com", contact_number = "9845621496", address = "Menlo Park, California", membershipTypeID = 3},
                new Member { first_name = "Wareen", last_name = "Buffet", gender = "male", date_of_birth = DateTime.Parse("1980-07-08"), email = "buffet@hathayway.com", contact_number = "9846249762", address = "Wall Street, New York", membershipTypeID = 4},
                new Member { first_name = "Sheryl", last_name = "Sandberg", gender = "female", date_of_birth = DateTime.Parse("1975-04-12"), email = "sheryl@fb.com", contact_number = "9865041769", address = "Menlo Park, California", membershipTypeID = 3},
                new Member { first_name = "Marrisa", last_name = "Mayer", gender = "female", date_of_birth = DateTime.Parse("1979-08-01"), email = "ceo@yahoo.com", contact_number = "9860456387", address = "Dusk Town, California", membershipTypeID = 4 },
                new Member { first_name = "Ada", last_name = "Lovelace", gender = "female", date_of_birth = DateTime.Parse("1965-12-10"), email = "love@example.com", contact_number = "9851056894", address = "Tennessace, Oklahoma", membershipTypeID = 6},
            };
            members.ForEach(t => context.Members.Add(t));
            context.SaveChanges();

            var shelves = new List<Shelf>
            {
                new Shelf { shelf_name = "Yellow A", room_name = "Buckingham Palace" },
                new Shelf { shelf_name = "Yellow B", room_name = "Buckingham Palace" },
                new Shelf { shelf_name = "Red A", room_name = "Westminister Palace" },
                new Shelf { shelf_name = "Red B", room_name = "Tower Bridge" },
                new Shelf { shelf_name = "Green A", room_name = "Tower Bridge" },
                new Shelf { shelf_name = "Green B", room_name = "Picadillay Circus" },
                new Shelf { shelf_name = "Blue A", room_name = "Picadillay Circus" },
                new Shelf { shelf_name = "Blue B", room_name = "Picadillay Circus" }
            };
            shelves.ForEach(t => context.Shelves.Add(t));
            context.SaveChanges();

            var publisher1 = new Publisher { name = "McGraw Hill", email = "support@mcgraw.com", contact_number = "9842115456", address = "Mountain View, California", website = "mcgraw.com", press = "Washinton Press" };
            var publisher2 = new Publisher { name = "Oreily Publication", email = "support@puv.com", contact_number = "9840000000", address = "32th Street, Washinton", website = "capitol.com", press = "Capitol Press" };
            var publisher3 = new Publisher { name = "Dummies Publication", email = "support@dummies.com", contact_number = "9842665456", address = "Warner Street, Menlo Park", website = "warnerbros.com", press = "Warner Brothers Press" };
            var publisher4 = new Publisher { name = "WestFord Publication House", email = "support@westford.com", contact_number = "9842333225", address = "Central Park, New York", website = "paramount.com", press = "Paramount Press Pvt. Ltd." };
            var publisher5 = new Publisher { name = "House of Winterfell Publication", email = "support@winterfell.com", contact_number = "9848466121", address = "Los County, California", website = "universal.com", press = "Universal Press" };
            var publisher6 = new Publisher { name = "House of Elves Publication", email = "support@elves.com", contact_number = "9842105698", address = "Big Mac, Las Vegas", website = "fox.com", press = "20th Century Fox Pvt. Ltd." };
            context.Publishers.Add(publisher1);
            context.Publishers.Add(publisher2);
            context.Publishers.Add(publisher3);
            context.Publishers.Add(publisher4);
            context.Publishers.Add(publisher5);
            context.Publishers.Add(publisher6);
            context.SaveChanges();

            var book1 = new Book { title = "48 Laws of Power", published_date = DateTime.Parse("2012-04-01"), standard_charge = 10.5m, penalty_charge = 30.5m, publisherID = 1, Authors = new List<Author>(), BookCategories = new List<BookCategory>(), BookCopies = new List<BookCopy>() };
            var book2 = new Book { title = "The Lean Startup", published_date = DateTime.Parse("2014-04-01"), standard_charge = 5.5m, penalty_charge = 25.5m, publisherID = 2, Authors = new List<Author>(), BookCategories = new List<BookCategory>(), BookCopies = new List<BookCopy>() };
            var book3 = new Book { title = "War and Peace", published_date = DateTime.Parse("2010-04-01"), standard_charge = 15.5m, penalty_charge = 30.5m, publisherID = 3, Authors = new List<Author>(), BookCategories = new List<BookCategory>(), BookCopies = new List<BookCopy>() };
            var book4 = new Book { title = "Algorithums and Data Structures", published_date = DateTime.Parse("2009-04-01"), standard_charge = 30.5m, penalty_charge = 60.5m, publisherID = 4, Authors = new List<Author>(), BookCategories = new List<BookCategory>(), BookCopies = new List<BookCopy>() };
            var book5 = new Book { title = "Javascript the definitive guide", published_date = DateTime.Parse("2011-04-01"), standard_charge = 10.5m, penalty_charge = 40.5m, publisherID = 1, Authors = new List<Author>(), BookCategories = new List<BookCategory>(), BookCopies = new List<BookCopy>() };
            var book6 = new Book { title = "Unit Testing in NutShell", published_date = DateTime.Parse("2005-04-01"), standard_charge = 30.5m, penalty_charge = 50.5m, publisherID = 2, Authors = new List<Author>(), BookCategories = new List<BookCategory>(), BookCopies = new List<BookCopy>() };
           
            var bookCategory1 = new BookCategory { category_name = "Fiction", age_restriction = false };
            var bookCategory2 = new BookCategory { category_name = "Fantasy", age_restriction = false };
            var bookCategory3 = new BookCategory { category_name = "Adult", age_restriction = true };
            var bookCategory4 = new BookCategory { category_name = "Sex Psycology", age_restriction = true };
            var bookCategory5 = new BookCategory { category_name = "Health", age_restriction = false };
            var bookCategory6 = new BookCategory { category_name = "Science Fiction", age_restriction = false };

            var author1 = new Author { first_name = "Jack", last_name = "Sparrow", gender = "male", email = "jac@sparrow.com", website = "jacksparrow.com" };
            var author2 = new Author { first_name = "Will", last_name = "Smith", gender = "male", email = "will@smith.com", website = "willsmith.com" };
            var author3 = new Author { first_name = "Princess", last_name = "Aura", gender = "female", email = "aura@princess.com", website = "auraprin.com" };
            var author4 = new Author { first_name = "Jason", last_name = "Statham", gender = "male", email = "jason@gmail.com", website = "jason.com" };
            var author5 = new Author { first_name = "George", last_name = "Clooney", gender = "male", email = "clooney@george.com", website = "george.com" };
            var author6 = new Author { first_name = "Matt", last_name = "Damon", gender = "male", email = "daemon@gmail.com", website = "mattdaemon.com" };
            var author7 = new Author { first_name = "Gal", last_name = "Gadot", gender = "female", email = "gadot@gmail.com", website = "gadot.com" };
            var author8 = new Author { first_name = "Ellen", last_name = "Degeneres", gender = "other", email = "ellen@gmail.com", website = "ellendegeneres.com" };

            book1.Authors.Add(author1);
            book1.Authors.Add(author2);
            book2.Authors.Add(author5);
            book3.Authors.Add(author4);
            book4.Authors.Add(author1);
            book4.Authors.Add(author7);
            book6.Authors.Add(author8);
            book6.Authors.Add(author7);
            book1.BookCategories.Add(bookCategory1);
            book2.BookCategories.Add(bookCategory4);
            book2.BookCategories.Add(bookCategory5);
            book3.BookCategories.Add(bookCategory3);
            book5.BookCategories.Add(bookCategory6);
            book5.BookCategories.Add(bookCategory2);
            book5.BookCategories.Add(bookCategory1);
            book6.BookCategories.Add(bookCategory4);
            context.Books.Add(book1);
            context.Books.Add(book2);
            context.Books.Add(book3);
            context.Books.Add(book4);
            context.Books.Add(book5);
            context.Books.Add(book6);
            context.SaveChanges();

            var rack1 = new Rack { rack_name = "YAB-1", shelfID = 1 };
            var rack2 = new Rack { rack_name = "BAN-10", shelfID = 2 };
            var rack3 = new Rack { rack_name = "DEA-11", shelfID = 3 };
            var rack4 = new Rack { rack_name = "UOW-51", shelfID = 1 };
            var rack5 = new Rack { rack_name = "PAC-45", shelfID = 2 };
            var rack6 = new Rack { rack_name = "DEA-15", shelfID = 4 };
            var rack7 = new Rack { rack_name = "FAM-110", shelfID = 4 };
            var rack8 = new Rack { rack_name = "DEA-64", shelfID = 5 };
            var rack9 = new Rack { rack_name = "NAI-84", shelfID = 6 };
            var rack10 = new Rack { rack_name = "NRI-74", shelfID = 7 };
            var rack11 = new Rack { rack_name = "TES-50", shelfID = 4 };
            var rack12 = new Rack { rack_name = "LOL-20", shelfID = 8 };
            context.Racks.Add(rack1);
            context.Racks.Add(rack2);
            context.Racks.Add(rack3);
            context.Racks.Add(rack4);
            context.Racks.Add(rack5);
            context.Racks.Add(rack6);
            context.Racks.Add(rack7);
            context.Racks.Add(rack8);
            context.Racks.Add(rack9);
            context.Racks.Add(rack10);
            context.Racks.Add(rack11);
            context.Racks.Add(rack12);
            context.SaveChanges();

            var bookCopy1 = new BookCopy { bookCopyNo = 1, bookID = 1, rackID = 1, added_date = DateTime.Parse("2014-04-01") };
            var bookCopy2 = new BookCopy { bookCopyNo = 2, bookID = 1, rackID = 2, added_date = DateTime.Today };
            var bookCopy3 = new BookCopy { bookCopyNo = 3, bookID = 1, rackID = 3, added_date = DateTime.Today };
            var bookCopy4 = new BookCopy { bookCopyNo = 4, bookID = 1, rackID = 4, added_date = DateTime.Today };
            var bookCopy5 = new BookCopy { bookCopyNo = 1, bookID = 2, rackID = 1, added_date = DateTime.Parse("2016-03-01") };
            var bookCopy6 = new BookCopy { bookCopyNo = 2, bookID = 2, rackID = 2, added_date = DateTime.Today };
            var bookCopy7 = new BookCopy { bookCopyNo = 3, bookID = 2, rackID = 5, added_date = DateTime.Today };
            var bookCopy8 = new BookCopy { bookCopyNo = 4, bookID = 2, rackID = 6, added_date = DateTime.Today };
            var bookCopy9 = new BookCopy { bookCopyNo = 1, bookID = 3, rackID = 5, added_date = DateTime.Parse("2017-01-01") };
            var bookCopy10 = new BookCopy { bookCopyNo = 2, bookID = 3, rackID = 6, added_date = DateTime.Today };
            var bookCopy11 = new BookCopy { bookCopyNo = 3, bookID = 3, rackID = 7, added_date = DateTime.Today };
            var bookCopy12 = new BookCopy { bookCopyNo = 4, bookID = 3, rackID = 8, added_date = DateTime.Parse("2014-04-12") };
            var bookCopy13 = new BookCopy { bookCopyNo = 5, bookID = 3, rackID = 10, added_date = DateTime.Today };
            var bookCopy14 = new BookCopy { bookCopyNo = 6, bookID = 3, rackID = 11, added_date = DateTime.Today };
            var bookCopy15 = new BookCopy { bookCopyNo = 1, bookID = 4, rackID = 11, added_date = DateTime.Parse("2015-08-11") };
            var bookCopy16 = new BookCopy { bookCopyNo = 2, bookID = 4, rackID = 8, added_date = DateTime.Parse("2013-05-01") };
            var bookCopy17 = new BookCopy { bookCopyNo = 3, bookID = 4, rackID = 12, added_date = DateTime.Today };
            var bookCopy18 = new BookCopy { bookCopyNo = 1, bookID = 5, rackID = 7, added_date = DateTime.Today };
            var bookCopy19 = new BookCopy { bookCopyNo = 2, bookID = 5, rackID = 12, added_date = DateTime.Today };
            var bookCopy20 = new BookCopy { bookCopyNo = 1, bookID = 6, rackID = 5, added_date = DateTime.Parse("2011-04-08") };
            context.BookCopies.Add(bookCopy1);
            context.BookCopies.Add(bookCopy2);
            context.BookCopies.Add(bookCopy3);
            context.BookCopies.Add(bookCopy4);
            context.BookCopies.Add(bookCopy5);
            context.BookCopies.Add(bookCopy6);
            context.BookCopies.Add(bookCopy7);
            context.BookCopies.Add(bookCopy8);
            context.BookCopies.Add(bookCopy9);
            context.BookCopies.Add(bookCopy10);
            context.BookCopies.Add(bookCopy11);
            context.BookCopies.Add(bookCopy12);
            context.BookCopies.Add(bookCopy13);
            context.BookCopies.Add(bookCopy14);
            context.BookCopies.Add(bookCopy15);
            context.BookCopies.Add(bookCopy16);
            context.BookCopies.Add(bookCopy17);
            context.BookCopies.Add(bookCopy18);
            context.BookCopies.Add(bookCopy19);
            context.BookCopies.Add(bookCopy20);
            context.SaveChanges();
        }
    }
}