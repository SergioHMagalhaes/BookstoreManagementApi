using BookstoreManagementApi.Communication.Requests;
using BookstoreManagementApi.Communication.Responses;
using BookstoreManagementApi.Models;
using BookstoreManagementApi.Services.Author;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookstoreManagementApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorInterface _authorInterface;
    public AuthorController(IAuthorInterface authorInterface)
    {
        _authorInterface = authorInterface;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<AuthorModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllAuthors()
    {
        var response = await _authorInterface.GetAllAuthors();
        if (response.Count == 0)
            return NoContent();
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(List<AuthorModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAuthorById([FromRoute] int id)
    {
        var response = await _authorInterface.GetAuthorById(id);
        if (response == null)
            return NoContent();
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredAuthorJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAuthor([FromBody] RequestRegisterAuthorJson author)
    {
        if (author == null)
            return BadRequest("Dados inválidos");

        var response = await _authorInterface.AddAuthor(author);
        return Created(string.Empty, response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(AuthorModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAuthor([FromBody] AuthorModel author)
    {
        if (author == null)
            return BadRequest("Dados inválidos");
        var response = await _authorInterface.UpdateAuthor(author);
        if (response == null)
            return NotFound("Autor não encontrado");
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
    {
        var response = await _authorInterface.DeleteAuthor(id);
        if (!response)
            return NotFound("Autor não encontrado");
        return NoContent();
    }
}
