using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Text;
using WebApplication21.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication21.Controllers
{
    
public class UserController : Controller
    {
        private readonly BillingdbContext context;
        public UserController(BillingdbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            // Check cookie and redirect to login if not set
            string tokenValue = Request.Cookies["token"];
            if (string.IsNullOrEmpty(tokenValue))
            { 
                return RedirectToAction("Login");
                TempData["isLogged"]="no";
            }

            TempData["isLogged"] = "yes";
            return View();
        }
        public IActionResult Login()
        {
            string tokenValue = Request.Cookies["token"];
            if (!string.IsNullOrEmpty(tokenValue))
            {
                Console.WriteLine($"Token: {tokenValue}");
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                User? user = context.Users.FirstOrDefault(item => item.Email == username);
                if (user != null && user.Password == password)
                {
                    TempData["Message"] = "Login success";

            
                    long currentTimeTicks = DateTime.UtcNow.Ticks;
                    string combinedString = $"{currentTimeTicks}:{user}";
                    byte[] bytes = Encoding.UTF8.GetBytes(combinedString);
                    string token = Convert.ToBase64String(bytes);
                    user.Token = token;
                    context.Users.Update(user);
                    context.SaveChanges(true);

                    // Fix: Use Response.Cookies.Append instead of HttpCookie
                    Response.Cookies.Append("token", $"{token}", new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7) // Optional: set expiry
                    });

                    return RedirectToAction("Index");
                }
            }

            TempData["Message"] = "Login failed";
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(string username, string name, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    User user = new User();
                    user.Email = username;
                    user.Password = password;
                    user.Name = name;
                    context.Users.Add(user);
                    context.SaveChanges();
                    TempData["alert"] = "success";
                    TempData["message"] = "Account created";
                } catch(Exception ex)
                {
                    TempData["alert"] = "danger";
                    TempData["message"] = ex?.InnerException?.Message;
                }
                return RedirectToAction("Signup");
            }
            TempData["alert"] = "danger";
            TempData["message"] = "Fill all the fields";
            return RedirectToAction("Signup");
        }

        public IActionResult AddProducts()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProducts(string name, string description, int price, int stock)
        {
            Product product = new Product();
            product.Name = name;
            product.Description = description;
            product.Price = price;
            product.Stock = stock;

            context.Products.Add(product);
            context.SaveChanges();
            TempData["message"] = "Data Saved";
            return View();
        }

        public IActionResult ListProducts()
        {
            var products = context.Products.ToList();
            return View(products);
        }
        public IActionResult DeleteProduct(int id)
        {
            Product product = new Product();
            product.Id = id;
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("ListProducts");
        }

        public IActionResult UpdateProduct(int id)
        {
            var product = context.Products.FirstOrDefault((item)=>item.Id == id);
            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateProduct(int id, string name, string description, int price, int stock)
        {
            var product = context.Products.FirstOrDefault((item) => item.Id == id);
            product.Name = name;
            product.Description = description;
            product.Price = price;
            product.Stock = stock;
            context.Products.Update(product);
            context.SaveChanges();
            TempData["message"] = "Updated";
            return RedirectToAction("UpdateProduct", new {id = id});
        }
    }
}
