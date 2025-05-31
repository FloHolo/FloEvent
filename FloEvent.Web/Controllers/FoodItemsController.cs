using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FloEvent.Catering.Data;
using FloEvent.Web.ViewModels;

namespace FloEvent.Web.Controllers
{
    public class FoodItemsController : Controller
    {
        private readonly CateringDbContext _context;

        public FoodItemsController(CateringDbContext context)
        {
            _context = context;
        }

        // GET: FoodItems
        public async Task<IActionResult> Index()
        {
            var viewModels = await _context.FoodItems
                .Select(f => new FoodItemViewModel
                {
                    FoodItemId = f.FoodItemId,
                    Name = f.Name,
                    Ingredients = f.Ingredients,
                    Diet = f.Diet,
                    UnitPrice = f.UnitPrice
                })
                .ToListAsync();

            return View(viewModels); 
        }

        // GET: FoodItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var foodItem = await _context.FoodItems
                .FirstOrDefaultAsync(m => m.FoodItemId == id);
            if (foodItem == null) return NotFound();

            var viewModel = new FoodItemViewModel
            {
                FoodItemId = foodItem.FoodItemId,
                Name = foodItem.Name,
                Ingredients = foodItem.Ingredients,
                Diet = foodItem.Diet,
                UnitPrice = foodItem.UnitPrice
            };

            return View(viewModel); 
        }

        // GET: FoodItems/Create
        public IActionResult Create()
        {
            var viewModel = new FoodItemViewModel
            {
                DietOptions = GetDietOptions()
            };
            return View(viewModel);
        }

        // POST: FoodItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var foodItem = new FoodItem
                {
                    Name = viewModel.Name,
                    Ingredients = viewModel.Ingredients,
                    Diet = viewModel.Diet,
                    UnitPrice = viewModel.UnitPrice
                };

                _context.Add(foodItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel.DietOptions = GetDietOptions();
            return View(viewModel);
        }

        // GET: FoodItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null) return NotFound();

            var viewModel = new FoodItemViewModel
            {
                FoodItemId = foodItem.FoodItemId,
                Name = foodItem.Name,
                Ingredients = foodItem.Ingredients,
                Diet = foodItem.Diet,
                UnitPrice = foodItem.UnitPrice,
                DietOptions = GetDietOptions()
            };

            return View(viewModel);
        }

        // POST: FoodItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FoodItemViewModel viewModel)
        {
            if (id != viewModel.FoodItemId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var foodItem = await _context.FoodItems.FindAsync(id);
                    if (foodItem == null) return NotFound();

                    foodItem.Name = viewModel.Name;
                    foodItem.Ingredients = viewModel.Ingredients;
                    foodItem.Diet = viewModel.Diet;
                    foodItem.UnitPrice = viewModel.UnitPrice;

                    _context.Update(foodItem);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodItemExists(viewModel.FoodItemId)) return NotFound();
                    throw;
                }
            }

            viewModel.DietOptions = GetDietOptions();
            return View(viewModel);
        }

        // GET: FoodItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var foodItem = await _context.FoodItems
                .FirstOrDefaultAsync(m => m.FoodItemId == id);
            if (foodItem == null) return NotFound();

            var viewModel = new FoodItemViewModel
            {
                FoodItemId = foodItem.FoodItemId,
                Name = foodItem.Name,
                Ingredients = foodItem.Ingredients,
                Diet = foodItem.Diet,
                UnitPrice = foodItem.UnitPrice
            };

            return View(viewModel); 
        }

        // POST: FoodItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem != null)
            {
                _context.FoodItems.Remove(foodItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FoodItemExists(int id)
        {
            return _context.FoodItems.Any(e => e.FoodItemId == id);
        }

        // Helper method for diet dropdown
        private IEnumerable<SelectListItem> GetDietOptions()
        {
            return Enum.GetValues(typeof(Diet))
                .Cast<Diet>()
                .Select(d => new SelectListItem
                {
                    Value = d.ToString(),
                    Text = d.ToString()
                });
        }
    }
}
