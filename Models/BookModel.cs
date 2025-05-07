namespace BookstoreManagementApi.Models;

public class BookModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public AuthorModel Author { get; set; }
    public int Price { get; set; }
    public int Amount { get; set; }
}
