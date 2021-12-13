using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery{
        //sadece constructor içerisinden set edilsin.
        private readonly BookStoreDbContext  _dbContext;
        public int BookId{get;set;}
        public GetBookDetailQuery(BookStoreDbContext  dbContext) {
            //DBCONTEXT İHTİYACIM VAR
             _dbContext = dbContext;
        }
        public BookDetailViewModel Handle(){
         var book = _dbContext.Books.Where(book=>book.Id == BookId).SingleOrDefault();
         if(book is null)
            throw new InvalidOperationException("Kitap bulunamadı.");
            
        //book view modele maplemem lazım
        BookDetailViewModel vm = new BookDetailViewModel();
        vm.Title = book.Title;
        vm.PageCount = book.PageCount;
        vm.PublishDate = book.PublishDate.Date.ToString("dd/mm/yyyy");
        vm.Genre = ((GenreEnum)book.GenreId).ToString();
        return vm;
        }   
    }
    public class BookDetailViewModel{
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }

}