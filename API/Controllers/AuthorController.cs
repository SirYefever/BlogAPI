using API.Converters;
using API.Dto;
using Core.Models;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Authors")]
public class AuthorController: ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly AuthorConverters _authorConverters;

    public AuthorController(IAuthorService authorService, AuthorConverters authorConverters)
    {
        _authorService = authorService;
        _authorConverters = authorConverters;
    }

    [SwaggerOperation("Get all authors")]
    [HttpGet("api/author/list")]
    [ProducesResponseType(typeof(List<AuthorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAuthors()
    {
        List<Author> authors = new List<Author>();
        try
        {
            authors = await _authorService.GetAllAsync();
        }
        catch
        {
            return StatusCode(500, new Response("An internal server error occurred", "Failed to get authors list."));
        }

        var authorDtos = new List<AuthorDto>();
        foreach (var author in authors)
        {
            try
            {
                authorDtos.Add(await _authorConverters.AuthorToAuthorDto(author));
            }
            catch
            {
                return StatusCode(500, new Response("An internal server error occurred", "Failed to convert entity into dto."));
            }
        }

        return Ok(authorDtos);
    }
}