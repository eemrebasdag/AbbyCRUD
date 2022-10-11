using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories;

    [BindProperties]

    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
        Category = _db.Category.Find(id);
        //Category = _db.Category.FirstOrDefault(u=>u.Id==id);
        //Category = _db.Category.SingleOrDefault(u => u.Id == id);
        //Category = _db.Category.Where(u=>u.Id==id).FirstOrDefault();

    }

    public async Task<IActionResult> OnPost()
        {
        if(Category.Name == Category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Category.Name", "The Display Order Cannot Exactly Match The Name!");
        }
        if (ModelState.IsValid)
        {
             _db.Update(Category);
            await _db.SaveChangesAsync();
            TempData["success"] = "Category Updated Successfully !";
            TempData["error"] = "Category Couldn't Updated !";
            return RedirectToPage("Index");
        }
        return Page();    
           
        }
    }

