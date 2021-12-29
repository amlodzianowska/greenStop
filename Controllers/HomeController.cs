using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using greenStop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace greenStop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(_context.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                _context.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("loggedInUser", newUser.UserId);
                return RedirectToAction("Dashboard");
            }else{
                return View("Index");
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LogUser logUser)
        {
            if(ModelState.IsValid)
            {
                User userinDb = _context.Users.FirstOrDefault(u => u.Email == logUser.LogEmail);
                if(userinDb == null)
                {
                    ModelState.AddModelError("LogEmail", "Invalid login attempt");
                    return View("Index");
                }
                PasswordHasher<LogUser> Hasher = new PasswordHasher<LogUser>();
                PasswordVerificationResult result = Hasher.VerifyHashedPassword(logUser, userinDb.Password, logUser.LogPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("LogEmail", "Invalid login attempt");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("loggedInUser", userinDb.UserId);
                return RedirectToAction("Dashboard");
            }else {
                return View("Index");
            }
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("loggedInUser") == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.LoggedInUser = _context.Users.FirstOrDefault(d => d.UserId == (int)HttpContext.Session.GetInt32("loggedInUser"));
            return View();
        }

        [HttpGet("login/registration")]
        public IActionResult LogReg()
        {
            if(HttpContext.Session.GetInt32("loggedInUser") != null)
            {
                return RedirectToAction("Logout");
            }
            
            return View();
        }

        [HttpGet("createListing")]
        public IActionResult PlantForm()
        {
            // if(HttpContext.Session.GetInt32("loggedInUser") != null)
            // {
            //     return RedirectToAction("Logout");
            // }
            string[] types = new string[] { "Succulent", "Cactus", "Palm", "Ivy", "Tropical", "Carnivorous", "Blossoming"};
            ViewBag.plantTypes =  types;
            
            return View();
        }

        [HttpGet("dummyuser")]
        public IActionResult DummyUserProfile()
        {
            
            
            return View();
        }

        [HttpGet("user")]
        public IActionResult UserProfile()
        {
            if(HttpContext.Session.GetInt32("loggedInUser") == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.LoggedInUser = _context.Users.Include(p => p.PlantsPosted).FirstOrDefault(d => d.UserId == (int)HttpContext.Session.GetInt32("loggedInUser"));
            ViewBag.usersPlants = _context.Plants.Include(o => o.Owner).Include(f => f.Likes).ThenInclude(f => f.Liker).Include(c => c.Comments).ThenInclude(d=>d.Commenter).Where(u=> u.UserId == (int)HttpContext.Session.GetInt32("loggedInUser")).ToList();
            return View();
        }

        [HttpGet("onePlant/{plantId}")]
        public IActionResult OnePlant(int plantId)
        {
            if(HttpContext.Session.GetInt32("loggedInUser") == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.LoggedInUser = _context.Users.FirstOrDefault(d => d.UserId == (int)HttpContext.Session.GetInt32("loggedInUser"));
            Plant onePlant = _context.Plants.Include(o => o.Owner).Include(f => f.Likes).ThenInclude(f => f.Liker).Include(c => c.Comments).ThenInclude(d=>d.Commenter).FirstOrDefault(p => p.PlantId == plantId);
            
            return View();
        }

        [HttpPost("addPlant")]
        public IActionResult AddPlant(Plant newPlant)
        {
            if(HttpContext.Session.GetInt32("loggedInUser") == null)
            {
                return RedirectToAction("Index");
            }
            if(ModelState.IsValid)
            {
                newPlant.UserId = (int)HttpContext.Session.GetInt32("loggedInUser");
                _context.Add(newPlant);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }else {
                return View("NewPlant");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
