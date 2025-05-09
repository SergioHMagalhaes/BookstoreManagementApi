using BookstoreManagementApi.Communication.Requests;
using BookstoreManagementApi.Communication.Responses;
using BookstoreManagementApi.Models;
using BookstoreManagementApi.Services.Gender;
using BookstoreManagementApi.Services.Gender;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreManagementApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GenderController : ControllerBase
{
    private readonly IGenderInterface _genderInterface;
    public GenderController(IGenderInterface genderInterface)
    {
        _genderInterface = genderInterface;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<GenderModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllGenders()
    {
        var response = await _genderInterface.GetAllGenders();
        if (response.Count == 0)
            return NoContent();
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(List<GenderModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetGenderById([FromRoute] int id)
    {
        var response = await _genderInterface.GetGenderById(id);
        if (response == null)
            return NoContent();
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredGenderJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddGender([FromBody] RequestRegisterGenderJson author)
    {
        if (author == null)
            return BadRequest("Dados inválidos");

        var response = await _genderInterface.AddGender(author);
        return Created(string.Empty, response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(GenderModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateGender([FromBody] GenderModel author)
    {
        if (author == null)
            return BadRequest("Dados inválidos");
        var response = await _genderInterface.UpdateGender(author);
        if (response == null)
            return NotFound("Genero não encontrado");
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGender([FromRoute] int id)
    {
        var response = await _genderInterface.DeleteGender(id);
        if (!response)
            return NotFound("Genero não encontrado");
        return NoContent();
    }
}
