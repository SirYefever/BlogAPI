using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Addresses")]
public class AddressController : ControllerBase
{
    private readonly IGarService _garService;

    public AddressController(IGarService garService)
    {
        _garService = garService;
    }

    [HttpGet("api/address/search")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchAddress(long parentId, string? query)
    {
        var result = await _garService.Search(parentId, query);
        return Ok(result);
    }

    [HttpGet("api/address/chain")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAddressChain(Guid objectGuid)
    {
        var result = await _garService.GetAddressChainAsync(objectGuid);
        return Ok(result);
    }
}