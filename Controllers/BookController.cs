using BookstoreManagementApi.Communication.Requests;
using BookstoreManagementApi.Communication.Responses;
using BookstoreManagementApi.Models;
using BookstoreManagementApi.Services.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreManagementApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookInterface _bookInterface;
    public BookController(IBookInterface bookInterface)
    {
        _bookInterface = bookInterface;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<BookModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllBooks()
    {
        var response = await _bookInterface.GetAllBooks();
        if (response.Count == 0)
            return NoContent();
        return Ok(response);
    }

    [HttpGet("author")]
    [ProducesResponseType(typeof(List<BookModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllBooksByAuthor([FromQuery] int id)
    {
        var response = await _bookInterface.GetAllBooksByAuthor(id);
        if (response.Count == 0)
            return NoContent();
        return Ok(response);
    }

    [HttpGet("gender")]
    [ProducesResponseType(typeof(List<BookModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllBooksByGender([FromQuery] int id)
    {
        var response = await _bookInterface.GetAllBooksByGender(id);
        if (response.Count == 0)
            return NoContent();
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(List<BookModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetBookById([FromRoute] int id)
    {
        var response = await _bookInterface.GetBookById(id);
        if (response == null)
            return NoContent();
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredBookJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddBook([FromBody] RequestRegisterBookJson book)
    {
        if (book == null)
            return BadRequest("Dados inválidos");

        var response = await _bookInterface.AddBook(book);
        return Created(string.Empty, response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(BookModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBook([FromBody] BookModel book)
    {
        if (book == null)
            return BadRequest("Dados inválidos");
        var response = await _bookInterface.UpdateBook(book);
        if (response == null)
            return NotFound("Livro não encontrado");
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook([FromRoute] int id)
    {
        var response = await _bookInterface.DeleteBook(id);
        if (!response)
            return NotFound("Livro não encontrado");
        return NoContent();
    }
}
