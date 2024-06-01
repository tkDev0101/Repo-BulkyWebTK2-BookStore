using BulkyWebTK22.DataAccess.Data;
using BulkyWebTK22.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWebTK22.Controllers
{
    public class ProductController : Controller
    {
        //CTOR
        private readonly ApplicationDbContext _db; //add
        public ProductController(ApplicationDbContext db)
        {
            _db = db;

        }

        //INDEX

        //action Method that will be invoked
        public IActionResult Index()
        {
            List<Product> objProductList = _db.Products.ToList();
            return View(objProductList);   //pass object into view and dispaly
        }

        //CREATE

        //Goes to view to create Product
        public IActionResult Create()
        {
            return View();
        }

        //when something is being posted
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            //}
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj); //add category object to categpry table
                _db.SaveChanges();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }


        //EDIT

        //user wants to edit Product
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }


            //diffirent ways to find category

            Product? categoryFormDb = _db.Products.Find(id);
            //Product? categoryFormDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Product? categoryFormDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (categoryFormDb == null)
            {
                return NotFound();
            }
            return View(categoryFormDb);

        }

        //when something is being posted
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj); //Update category object to categpry table
                _db.SaveChanges();
                TempData["success"] = "Product Updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }



        //DELETE

        //user wants to delete Product
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? categoryFormDb = _db.Products.Find(id);

            if (categoryFormDb == null)
            {
                return NotFound();
            }
            return View(categoryFormDb);

        }

        //when something is being posted
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _db.Products.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");


        }




    }
}
