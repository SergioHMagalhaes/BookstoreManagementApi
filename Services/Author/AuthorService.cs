using BookstoreManagementApi.Communication.Requests;
using BookstoreManagementApi.Communication.Responses;
using BookstoreManagementApi.Data;
using BookstoreManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreManagementApi.Services.Author;

public class AuthorService : IAuthorInterface
{
    private readonly AppDbContext _context;
    public AuthorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseRegisteredAuthorJson> AddAuthor(RequestRegisterAuthorJson author)
    {
        ResponseRegisteredAuthorJson response = new();

        var newAuthor = new AuthorModel
        {
            Name = author.Name,
            BirthYear = author.BirthYear,
            Biography = author.Biography
        };

        _context.Authors.Add(newAuthor);
        await _context.SaveChangesAsync();

        response.Id = newAuthor.Id;
        response.Name = newAuthor.Name;
        return response;

    }

    public async Task<bool> DeleteAuthor(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        if (author == null)
            return false;

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<AuthorModel>> GetAllAuthors()
    {
        return await _context.Authors.ToListAsync();
        
    }

    public async Task<AuthorModel?> GetAuthorById(int id)
    {
        return await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<AuthorModel?> UpdateAuthor(AuthorModel author)
    {
        var authorToUpdate = await _context.Authors.FirstOrDefaultAsync(x => x.Id == author.Id);
        if (authorToUpdate == null)
            return null;

        authorToUpdate.Name = author.Name;
        authorToUpdate.BirthYear = author.BirthYear;
        authorToUpdate.Biography = author.Biography;

        _context.Authors.Update(authorToUpdate);
        await _context.SaveChangesAsync();

        return authorToUpdate;
    }
}
