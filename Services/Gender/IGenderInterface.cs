using BookstoreManagementApi.Communication.Requests;
using BookstoreManagementApi.Communication.Responses;
using BookstoreManagementApi.Models;

namespace BookstoreManagementApi.Services.Gender;

public interface IGenderInterface
{
    Task<ResponseRegisteredGenderJson> AddGender(RequestRegisterGenderJson book);
    Task<bool> DeleteGender(int id);
    Task<List<GenderModel>> GetAllGenders();
    Task<GenderModel?> GetGenderById(int id);
    Task<GenderModel?> UpdateGender(GenderModel book);
}
