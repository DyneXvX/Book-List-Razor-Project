﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        } //DataBase Connection

        public IEnumerable<Book> Books { get; set; }
        //Build the IEnumerable of Books

        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        } //get the list of books.

        //Async let you run multiple tasks at a time till it is awaiting. 

        public async Task<IActionResult> OnPostDelete(int id) //OnPostDelete is a custom asp handle that I created on Index.
        {
            var book = await _db.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _db.Book.Remove(book);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }

}