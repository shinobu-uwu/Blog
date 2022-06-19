using Blog.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class HomeController : Controller
{
    private readonly IPostRepository _postRepository;

    public HomeController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public IActionResult Index()
    {
        return View(_postRepository.GetAllEnabledOrderedByDate());
    }
}