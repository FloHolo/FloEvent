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
            return View(await _context.FoodItem.ToListAsync());
        }

        // GET: FoodItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = await _context.FoodItem
            .FirstOrDefaultAsync(m => m.FoodItemId == id);
            if (foodItem == null)
            {
                return NotFound();
            }

            return View(foodItem);
        }


        // GET: FoodItems/Create
        public IActionResult Create()
        {
            var viewModel = new FoodItemViewModel
            {
                DietOptions = Enum.GetValues(typeof(Diet))
                    .Cast<Diet>()
                    .Select(d => new SelectListItem
                    {
                        Value = d.ToString(),
                        Text = d.ToString()
                    })
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
                _context.Add(viewModel.FoodItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate DietOptions if model state is invalid
            viewModel.DietOptions = Enum.GetValues(typeof(Diet))
         .Cast<Diet>()
         .Select(d => new SelectListItem
         {
             Value = d.ToString(),
             Text = d.ToString()
         });

            return View(viewModel);
        }



        // GET: FoodItems/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = await _context.FoodItem.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            var viewModel = new FoodItemViewModel
            {
                FoodItem = foodItem,
                DietOptions = Enum.GetValues(typeof(Diet))
            .Cast<Diet>()
            .Select(d => new SelectListItem
            {
                Value = d.ToString(),
                Text = d.ToString()
            })
            };

            return View(viewModel);
        }


        // POST: FoodItems/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FoodItemViewModel viewModel)
        {
            if (id != viewModel.FoodItem.FoodItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.FoodItem);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodItemExists(viewModel.FoodItem.FoodItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Repopulate DietOptions if model state is invalid
            viewModel.DietOptions = Enum.GetValues(typeof(Diet))
         .Cast<Diet>()
         .Select(d => new SelectListItem
         {
             Value = d.ToString(),
             Text = d.ToString()
         });

            return View(viewModel);
        }


        // POST: FoodItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodItem = await _context.FoodItem.FindAsync(id);
            if (foodItem != null)
            {
                _context.FoodItem.Remove(foodItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodItemExists(int id)
        {
            return _context.FoodItem.Any(e => e.FoodItemId == id);
        }
    }
}
