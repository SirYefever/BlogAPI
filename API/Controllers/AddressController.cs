using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Addresses")]
public class AddressController: ControllerBase
{
    private readonly IGarService _garService;

    public AddressController(IGarService garService)
    {
        _garService = garService;
    }

    [HttpGet("api/address/chain")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Test(Guid objectGuid)
    {
        var restult = await _garService.Test(objectGuid);
        return Ok(restult);
    }
}