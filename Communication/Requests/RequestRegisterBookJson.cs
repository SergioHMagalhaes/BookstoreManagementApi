using BookstoreManagementApi.Models;

namespace BookstoreManagementApi.Communication.Requests;

public class RequestRegisterBookJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AuthorModel Author { get; set; }
    public GenderModel Gender { get; set; }
    public int Price { get; set; }
    public int Amount { get; set; }
}