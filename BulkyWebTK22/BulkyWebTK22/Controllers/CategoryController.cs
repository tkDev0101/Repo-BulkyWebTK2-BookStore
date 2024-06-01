using BulkyWebTK22.DataAccess.Data;
using BulkyWebTK22.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWebTK22.Controllers
{
    public class CategoryController : Controller
    {
        //CTOR
        private readonly ApplicationDbContext _db; //add
        public CategoryController(ApplicationDbContext db)
        {
            _db= db;
                
        }

        //INDEX

        //action Method that will be invoked
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();   
            return View(objCategoryList);   //pass object into view and dispaly
        }

        //CREATE

        //Goes to view to create Category
        public IActionResult Create()
        {
            return View();  
        }

        //when something is being posted
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if ( obj.Name == obj.DisplayOrder.ToString() )
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj); //add category object to categpry table
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
         
        }


        //EDIT

        //user wants to edit Category
        public IActionResult Edit( int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }


            //diffirent ways to find category

            Category? categoryFormDb = _db.Categories.Find(id);
            //Category? categoryFormDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFormDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (categoryFormDb == null)
            {
                return NotFound();
            }
            return View(categoryFormDb);

        }

        //when something is being posted
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj); //Update category object to categpry table
                _db.SaveChanges();
                TempData["success"] = "Category Updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }



        //DELETE

        //user wants to delete Category
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFormDb = _db.Categories.Find(id);
            
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
            Category? obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");


        }




    }
}
