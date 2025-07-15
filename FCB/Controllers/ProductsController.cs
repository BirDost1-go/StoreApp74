
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FCB.Models;
using FCB.Services;


namespace FCB.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IPersonService _personService;

        public ProductsController(IProductService productService, IPersonService personService)
        {
            _productService = productService;
            _personService = personService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var product = await _productService.GetProductsWithOwnerAsync();
            return View(product);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductWithOwnerByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var owners = await _personService.GetAllAsync();
            ViewData["OwnerId"] = new SelectList(owners, "Id", "FirstName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Stock,OwnerId")]
        Product product, IFormFile formFile)
        {
            //ModelState.Remove("ImageUrl"); // Remove ImageUrl from model state to avoid validation errors
            ModelState.Remove("FormFile"); // Remove ImageUrl from model state to avoid validation errors
            if (product.OwnerId > 0)
            {
                var ownerExists = await _personService.PeopleExistsByIdAsync(product.OwnerId);
                if (!ownerExists)
                {
                    ModelState.AddModelError("OwnerId", "The selected owner does not exist.");
                }
            }
            if (formFile != null && formFile.Length > 0)
            {
                try
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads");
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    product.ImageUrl = "/Uploads/" + uniqueFileName;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("FormFile", "File upload faild: " + ex.Message);
                }
            }
            else
            {
                product.ImageUrl = "/images/default-product.jpg";// Set default value if no file is uploaded
            }
            product.CreatedAt = DateTime.UtcNow; // Set CreatedAt to current time
            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.AddAsync(product);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving product: " + ex.Message);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    Console.WriteLine($"ModelState Error: {error}");
                }
                foreach (var key in ModelState.Keys)
                {
                    if(ModelState[key].Errors.Count > 0)
                    {
                        Console.WriteLine($"Field '{key}' has errors:");
                        foreach (var error in ModelState[key].Errors)
                        {
                            Console.WriteLine($"  - {error.ErrorMessage}");
                        }
                    }
                }
            }
            var owners = await _personService.GetAllAsync();
            ViewData["OwnerId"] = new SelectList(owners, "Id", "FirstName", product.OwnerId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductWithOwnerByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            var owners = await _personService.GetAllAsync();
            ViewData["OwnerId"] = new SelectList(owners, "Id", "PhoneNumber", product.OwnerId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Stock,ImageUrl,CreatedAt,OwnerId")]
        Product product, IFormFile formFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            //ModelState.Remove("ImageUrl"); // Remove ImageUrl from model state to avoid validation errors
            ModelState.Remove("FormFile"); // Remove FormFile from model state to avoid validation errors
            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile != null && formFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads");

                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }

                        product.ImageUrl = "/Uploads/" + uniqueFileName;
                    }

                    await _productService.UpdateAsync(product);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating product: {ex.Message}");
                }
            }

            var owners = await _personService.GetAllAsync();
            ViewData["OwnerId"] = new SelectList(owners, "Id", "FirstName", product.OwnerId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
