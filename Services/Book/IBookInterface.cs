using BookstoreManagementApi.Communication.Requests;
using BookstoreManagementApi.Communication.Responses;
using BookstoreManagementApi.Models;

namespace BookstoreManagementApi.Services.Book;

public interface IBookInterface
{
    Task<List<BookModel>> GetAllBooks();
    Task<List<BookModel>> GetAllBooksByAuthor(int id);
    Task<List<BookModel>> GetAllBooksByGender(int id);
    Task<BookModel?> GetBookById(int id);
    Task<ResponseRegisteredBookJson?> AddBook(RequestRegisterBookJson book);
    Task<BookModel?> UpdateBook(BookModel book);
    Task<bool> DeleteBook(int id);
}
