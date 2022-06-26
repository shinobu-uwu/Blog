using Blog.Database.Repositories;
using Blog.Exceptions.Entity;
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
        var post = _postRepository.GetById(id);

        if (!post.Enabled)
        {
            return RedirectToAction("Error");
        }

        return View(post);
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

    public IActionResult Delete(int id)
    {
        var post = _postRepository.GetById(id);
        _postRepository.Disable(post);
        _postRepository.Save();

        return RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 300)]
    public IActionResult Error()
    {
        return View();
    }

    public async Task<IActionResult> Edit(int id)
    {
        var post = _postRepository.GetById(id);

        if (!await IsPostEditionValid(post))
        {
            return RedirectToAction("Error");
        }

        return View(new EditPostViewModel { PostId = post.Id, Title = post.Title, Body = post.Body });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditPostViewModel postViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(postViewModel);
        }

        try
        {
            var post = _postRepository.GetById(postViewModel.PostId);

            if (!await IsPostEditionValid(post))
            {
                return RedirectToAction("Error");
            }

            post.Title = postViewModel.Title;
            post.Body = postViewModel.Body;
            _postRepository.Save();
        }
        catch (InvalidOperationException) // In case the user edits the HTML to remove the hidden input field
        {
            return RedirectToAction("Error");
        }
        catch (EntityNotFoundException)
        {
            return RedirectToAction("Error");
        }

        return RedirectToAction("Index", "Home");
    }

    private async Task<bool> IsPostEditionValid(Post post)
    {
        var user = await _userManager.GetUserAsync(User);
        return post.Enabled && post.Author.Id == user.Id;
    }
}