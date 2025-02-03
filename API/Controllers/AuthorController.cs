using API.Converters;
using API.Dto;
using Core.Models;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Authors")]
public class AuthorController : ControllerBase
{
    private readonly AuthorConverters _authorConverters;
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService, AuthorConverters authorConverters)
    {
        _authorService = authorService;
        _authorConverters = authorConverters;
    }

    [SwaggerOperation("Get all authors")]
    [HttpGet("api/author/list")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<AuthorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAuthors()
    {
        List<Author> authors = new();
        authors = await _authorService.GetAllAsync();

        var authorDtos = new List<AuthorDto>();
        foreach (var author in authors) authorDtos.Add(await _authorConverters.AuthorToAuthorDto(author));

        return Ok(authorDtos);
    }
}