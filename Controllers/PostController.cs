using Blog.Database.Repositories;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Authorize]
public class PostController : Controller
{
    private readonly IPostRepository _postRepository;
    private readonly UserManager<User> _userManager;

    public PostController(IPostRepository postRepository, UserManager<User> userManager)
    {
        _postRepository = postRepository;
        _userManager = userManager;
    }

    [AllowAnonymous]
    public IActionResult View(int id)
    {
        return View(_postRepository.GetById(id));
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PostViewModel postViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(postViewModel);
        }

        var post = new Post
        {
            Title = postViewModel.Title,
            Body = postViewModel.Body,
            Author = await _userManager.GetUserAsync(User),
            Enabled = true,
            CreationDate = DateTime.Now
        };

        _postRepository.Add(post);
        _postRepository.Save();

        return RedirectToAction("Index", "Home");
    }
}