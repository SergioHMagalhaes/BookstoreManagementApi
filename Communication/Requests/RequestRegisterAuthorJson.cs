namespace BookstoreManagementApi.Communication.Requests;

public class RequestRegisterAuthorJson
{
    public string Name { get; set; } = string.Empty;
    public int BirthYear { get; set; }
    public string Biography { get; set; } = string.Empty;
}