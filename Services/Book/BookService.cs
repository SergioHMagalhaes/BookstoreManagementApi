using BookstoreManagementApi.Communication.Requests;
using BookstoreManagementApi.Communication.Responses;
using BookstoreManagementApi.Data;
using BookstoreManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookstoreManagementApi.Services.Book;

public class BookService : IBookInterface
{
    private readonly AppDbContext _context;
    public BookService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseRegisteredBookJson> AddBook(RequestRegisterBookJson book)
    {
        ResponseRegisteredBookJson response = new();
        var newBook = new BookModel
        {
            Title = book.Title,
            Author = book.Author,
            Gender = book.Gender,
            Description = book.Description,
            Price = book.Price,
            Amount = book.Amount
        };

        _context.Books.Add(newBook);
        await _context.SaveChangesAsync();

        response.Id = newBook.Id;
        response.Title = newBook.Title;
        return response;
    }

    public async Task<bool> DeleteBook(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (book == null)
            return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<BookModel>> GetAllBooks()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<List<BookModel>> GetAllBooksByAuthor(int id)
    {
        return await _context.Books.Include(x => x.Author)
            .Where(x => x.Author.Id == id)
            .ToListAsync();
    }

    public async Task<List<BookModel>> GetAllBooksByGender(int id)
    {
        return await _context.Books.Include(x => x.Ge)
            .Where(x => x.Author.Id == id)
            .ToListAsync();
    }

    public async Task<BookModel?> GetBookById(int id)
    {
        return await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BookModel?> UpdateBook(BookModel book)
    {
        var bookToUpdate = await _context.Books.FirstOrDefaultAsync(x => x.Id == book.Id);
        if (bookToUpdate == null)
            return null;

        bookToUpdate.Title = book.Title;
        bookToUpdate.Description = book.Description;
        bookToUpdate.Author = book.Author;
        bookToUpdate.Gender = book.Gender;
        bookToUpdate.Price = book.Price;
        bookToUpdate.Amount = book.Amount;

        _context.Books.Update(bookToUpdate);
        await _context.SaveChangesAsync();

        return bookToUpdate;
    }
}
