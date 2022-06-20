using Blog.Database.Repositories;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class PostController : Controller
{
    private readonly IPostRepository _postRepository;

    public PostController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(PostViewModel postViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(postViewModel);
        }
        
        var post = new Post(postViewModel.Title, postViewModel.Body);
        _postRepository.Add(post);
        _postRepository.Save();
        
        return RedirectToAction("Index", "Home");
    }
}