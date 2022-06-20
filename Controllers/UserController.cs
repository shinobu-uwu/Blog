using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class UserController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserViewModel userViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userViewModel);
        }

        var user = new IdentityUser { Email = userViewModel.Email, UserName = userViewModel.UserName };
        var result = await _userManager.CreateAsync(user, userViewModel.Password);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        ViewData["Show alert"] = true;
        return View(userViewModel);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginViewModel userViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userViewModel);
        }

        var result =
            await _signInManager.PasswordSignInAsync(userViewModel.UserName, userViewModel.Password, true, false);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Invalid login attempt");
        return View(userViewModel);
    }
}