using AutoMapper;
using BlogProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    public class AccountController1 : Controller { 
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;


        public AccountController1(IMapper mapper,UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
    {
        _userManager = userMgr;
        _signInManager = signinMgr;
       _mapper = mapper;
    }

    [AllowAnonymous]
    public IActionResult Login(string returnUrl)
    {
        Account login = new Account();
        login.ReturnUrl = returnUrl;
        return View(login);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(Account login)
    {
        if (ModelState.IsValid)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(login.Email);
            if (appUser != null)
            {
                await _signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, login.Password, false, false);
                if (result.Succeeded)
                    return Redirect(login.ReturnUrl ?? "/");
            }
            ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
        }
        return View(login);
    }
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


        //Registeration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Account account)
        {
            if (!ModelState.IsValid)
            {
                return View(account);
            }

            var user = _mapper.Map<AppUser>(account);

            var result = await _userManager.CreateAsync(user, account.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View(account);
            }

            await _userManager.AddToRoleAsync(user, "Visitor");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
