using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi;
using WebApi.DbOperations;

namespace WepApi.DBOperations
{
    
    public class DataGenerator{
        //verileri insert eden method: inmemory service provider ile program.cs ye bağlanır. 
        //program.cs deki serviceprovider burayı çağıracak.uygulama ilk ayağa kalktığında hep çalışacak.
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // context instance 
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any()){
                    //hiç kitap var mı?
                    return;//çalıştırma
                } 
                context.Books.AddRange(   //Books içine 2 tane kitap eklendi.
                    new Book{
                       // Id = 1,
                        Title = "lean startup",
                        GenreId = 1,
                        PageCount =200,
                        PublishDate =new DateTime(2010,02,12)
                    },
                    new Book{
                       // Id = 2,
                        Title = "herland",
                        GenreId = 2,
                        PageCount =250,
                        PublishDate =new DateTime(2000,04,1)
                    }
                );//database de değişiklik yapınca onu savechanges diyerek kaydetmemiz gerekir.
                context.SaveChanges();        
            }
        }
    }
}