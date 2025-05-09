using BookstoreManagementApi.Communication.Requests;
using BookstoreManagementApi.Communication.Responses;
using BookstoreManagementApi.Data;
using BookstoreManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreManagementApi.Services.Gender;

public class GenderService : IGenderInterface
{
    private readonly AppDbContext _context;
    public GenderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseRegisteredGenderJson> AddGender(RequestRegisterGenderJson author)
    {
        ResponseRegisteredGenderJson response = new();
        var newGender = new GenderModel
        {
            Name = author.Name
        };

        _context.Gender.Add(newGender);
        await _context.SaveChangesAsync();

        response.Id = newGender.Id;
        response.Name = newGender.Name;
        return response;
    }

    public async Task<bool> DeleteGender(int id)
    {
        var gender = await _context.Gender.FirstOrDefaultAsync(x => x.Id == id);
        if (gender == null)
            return false;

        _context.Gender.Remove(gender);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<GenderModel>> GetAllGenders()
    {
        return await _context.Gender.ToListAsync();
    }

    public async Task<GenderModel?> GetGenderById(int id)
    {
        return await _context.Gender.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<GenderModel?> UpdateGender(GenderModel author)
    {
        var genderToUpdate = await _context.Gender.FirstOrDefaultAsync(x => x.Id == author.Id);
        if (genderToUpdate == null)
            return null;

        genderToUpdate.Name = author.Name;

        _context.Gender.Update(genderToUpdate);
        await _context.SaveChangesAsync();

        return genderToUpdate;
    }
}
