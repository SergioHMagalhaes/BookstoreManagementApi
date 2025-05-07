using BookstoreManagementApi.Communication.Requests;
using BookstoreManagementApi.Communication.Responses;
using BookstoreManagementApi.Models;

namespace BookstoreManagementApi.Services.Author;

public interface IAuthorInterface
{
    Task<ResponseRegisteredAuthorJson> AddAuthor(RequestRegisterAuthorJson author);
    Task<bool> DeleteAuthor(int id);
    Task<List<AuthorModel>> GetAllAuthors();
    Task<AuthorModel?> GetAuthorById(int id);
    Task<AuthorModel?> UpdateAuthor(AuthorModel author);
}
