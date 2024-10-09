using AvtoHubProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.IO;

namespace AvtoHubProject.Controllers
{
    public class AvtoHub : Controller
    {
        private readonly AvtoHubDbContext context;
        private readonly UserManager<AvtoHubUser> userManager;
        private readonly SignInManager<AvtoHubUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment environment;

        public AvtoHub(AvtoHubDbContext context, UserManager<AvtoHubUser> userManager, SignInManager<AvtoHubUser> signInManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment environment)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.environment = environment;
        }
        public async Task<IActionResult> HomePage()
        {
            var listOfAllProducts = await context.AvtoHubProducts.ToListAsync();
            return View(listOfAllProducts);
        }
        public IActionResult About() => View();

        [Authorize]
        [Route("AvtoHub/profile")]
        public async Task<IActionResult> MyAccount()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Email = user.Email;

            var list  = await context.AvtoHubProducts.Where(a=>a.UserId==user.Id).ToListAsync();
            if (list == null || !list.Any())
            {
                // Check if the query is returning any products for the user
                return Content("No products found for this user.");
            }
            ViewBag.ProductCounts = list.Count();
            return View(list);
        }
        [Authorize]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        [Route("AvtoHub/add")]
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest();
            }
            ViewBag.Email = user.Email;
            ViewBag.Id = user.Id;
            return View();
        }

        //[RequestSizeLimit(5 * 1024 * 2014)]
        [ValidateAntiForgeryToken]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return BadRequest();
                }

                ViewBag.Id = user.Id;
                ViewBag.Email = user.Email;

                var fileName = Path.GetFileName(productModel.FormFile.FileName);
                string uploadFolder = Path.Combine(environment.WebRootPath, "AvtoHubImages");
                string fullPath = Path.Combine(uploadFolder, fileName);

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                using (var stream = System.IO.File.Create(fullPath))
                {
                    await productModel.FormFile.CopyToAsync(stream);
                }

                Product newProduct = new Product()
                {
                    BanType = productModel.BanType,
                    City = productModel.City,
                    Marka = productModel.Marka,
                    Model = productModel.Model,
                    Color = productModel.Color,
                    DetailedInformation = productModel.DetailedInformation,
                    MileAge = productModel.MileAge,
                    ProductionYear = productModel.ProductionYear,
                    EnginePower = productModel.EnginePower,
                    GearBoxType = productModel.GearBoxType,
                    Price = productModel.Price,
                    OilType = productModel.OilType,
                    UserId = user.Id,
                    ImagePath = fileName
                };

                await context.AvtoHubProducts.AddAsync(newProduct);
                await context.SaveChangesAsync();

                return RedirectToAction("MyAccount", "AvtoHub");
            }
            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    // You can log or print these errors
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(productModel);
        }

        [Authorize]
        public  IActionResult Cars(int id)
        {
            var choosenProduct = context.AvtoHubProducts.Include(a=>a.AvtoHubUser).FirstOrDefault(a=>a.Id == id);

            if (choosenProduct == null)
            {
                return BadRequest();
            }
            ViewBag.Marka = choosenProduct.Marka;
            ViewBag.Model = choosenProduct.Model;
            ViewBag.Color = choosenProduct.Color;
            ViewBag.BanType = choosenProduct.BanType;
            ViewBag.DetailedInformation = choosenProduct.DetailedInformation;
            ViewBag.MileAge = choosenProduct.MileAge;
            ViewBag.ProductionYear = choosenProduct.ProductionYear;
            ViewBag.EnginePower = choosenProduct.EnginePower;
            ViewBag.Price = choosenProduct.Price;
            ViewBag.ImagePath = choosenProduct.ImagePath    ;
            ViewBag.Contact = choosenProduct.AvtoHubUser.Email;
            ViewBag.GearBoxType = choosenProduct.GearBoxType;
            ViewBag.OilType = choosenProduct.OilType;

            ViewBag.Title= choosenProduct.Marka + " " + choosenProduct.Model + " , " + choosenProduct.EnginePower+"L" + " " + choosenProduct.ProductionYear+"il" + " , "  + " " + choosenProduct.MileAge+"km" + " , " + choosenProduct.City+"da almaq";
            var RelatedCars =  context.AvtoHubProducts.Where(a => a.Model == choosenProduct.Model).ToList();

            return View(RelatedCars);
        }
    }
}
