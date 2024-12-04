using API.Converters;
using API.Dto;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAuthors()
    {
        var authors = await _authorService.GetAllAsync();
        var authorDtos = new List<AuthorDto>();
        foreach (var author in authors)
            authorDtos.Add(await _authorConverters.AuthorToAuthorDto(author));
        
        return Ok(authorDtos);
    }
}