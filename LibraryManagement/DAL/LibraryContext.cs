using LibraryManagement.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LibraryManagement.DAL
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("name=LibraryContext")
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<LoanType> LoanTypes { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanLog> LoanLogs { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<Shelf> Shelves { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        }
}