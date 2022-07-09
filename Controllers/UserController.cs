using Blog.Database.Repositories;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[AllowAnonymous]
public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUserRepository _userRepository;

    public UserController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IWebHostEnvironment webHostEnvironment,
        IUserRepository userRepository
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _webHostEnvironment = webHostEnvironment;
        _userRepository = userRepository;
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

        Avatar avatar = null;

        if (userViewModel.Avatar is not null)
        {
            avatar = new Avatar(userViewModel.Avatar);
        }

        var user = new User
        {
            Email = userViewModel.Email,
            UserName = userViewModel.UserName,
            CreationDate = DateTime.Now,
            Enabled = true,
            Avatar = avatar
        };

        var result = await _userManager.CreateAsync(user, userViewModel.Password);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

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

        var result = await _signInManager.PasswordSignInAsync(
            userViewModel.UserName,
            userViewModel.Password,
            true,
            false
        );

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Invalid login attempt");
        return View(userViewModel);
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> UserAvatar()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return GetDefaultAvatar();
        }

        var avatar = await GetUserAvatar();

        if (avatar is null || avatar.Data.Length == 0)
        {
            return GetDefaultAvatar();
        }

        return File(avatar.Data, avatar.ContentType);
    }

    private async Task<Avatar?> GetUserAvatar()
    {
        var user = await _userManager.GetUserAsync(User);

        return _userRepository.GetById(user.Id).Avatar;
    }

    private IActionResult GetDefaultAvatar()
    {
        var fileName = Path.Combine(_webHostEnvironment.WebRootPath, "avatar.webp");

        var fileInfo = new FileInfo(fileName);
        var binaryReader = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
        var imageData = binaryReader.ReadBytes((int)fileInfo.Length);

        return File(imageData, "image/webp");
    }
}