using Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PostController: ControllerBase
{
    private readonly IPostService _postService;
    
}