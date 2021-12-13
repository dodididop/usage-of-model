using Microsoft.EntityFrameworkCore;

namespace WebApi.DbOperations
{
    public class BookStoreDbContext : DbContext
    {
        //default constructor
        //kalıtımdan alınan options verildi.
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {

        }

        //Books nesnesini kullanıldı. db de kullancak isim çoğul olur.
        //Book bir replica databaseden:
        public DbSet<Book> Books {get;set;}

    }
}