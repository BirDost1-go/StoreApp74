
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FCB.Models;
using FCB.Services;
using Microsoft.EntityFrameworkCore;


namespace FCB.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IPersonService _personService;
        private readonly IFileStorageService _fileStorageService;

        public ProductsController(IProductService productService,
            IPersonService personService,
            IFileStorageService fileStorageService)
        {
            _productService = productService;
            _personService = personService;
            _fileStorageService = fileStorageService;
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
            ViewData["OwnerId"] = new SelectList(owners, "Id", "PhoneNumber");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,Stock,OwnerId")]
        Product product, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                if(formFile != null && formFile.Length > 0)
                {
                    product.ImageUrl = await _fileStorageService.UploadFileAsync(formFile);
                }
                else
                {
                    product.ImageUrl = "/images/default-product.jpg"; // Default image if no file is uploaded
                }
                product.CreatedAt = DateTime.UtcNow; // Set the creation date
                await _productService.AddAsync(product);
                return RedirectToAction(nameof(Index));
            } 
            var owners = await _personService.GetAllAsync();
            ViewData["OwnerId"] = new SelectList(owners, "Id", "PhoneNumber", product.OwnerId);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Stock,OwnerId")]
        Product product, IFormFile? formFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var productToUpdate= await _productService.GetByIdAsync(id);
                if (productToUpdate == null) 
                    return NotFound();

                productToUpdate.Name = product.Name; 
                productToUpdate.Description = product.Description;
                productToUpdate.Price = product.Price;
                productToUpdate.Stock = product.Stock;
                productToUpdate.OwnerId = product.OwnerId;

                if (formFile != null && formFile.Length > 0)
                {
                    _fileStorageService.RemoveFile(productToUpdate.ImageUrl);
                    productToUpdate.ImageUrl = await _fileStorageService.UploadFileAsync(formFile);
                }

                await _productService.UpdateAsync(productToUpdate);
                return RedirectToAction(nameof(Index));
            }
            var owners = await _personService.GetAllAsync();    
            ViewData["OwnerId"] = new SelectList(owners, "Id", "PhoneNumber", product.OwnerId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            //var owners = await _personService.GetAllAsync();
            //ViewData["OwnerId"] = new SelectList(owners, "Id", "PhoneNumber", product.OwnerId);
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productToDelete = await _productService.GetByIdAsync(id);
            if (productToDelete != null)
            {
                _fileStorageService.RemoveFile(productToDelete.ImageUrl);
                await _productService.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
