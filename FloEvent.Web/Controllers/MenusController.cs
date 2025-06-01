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
    public class MenusController : Controller
    {
        private readonly CateringDbContext _context;

        public MenusController(CateringDbContext context)
        {
            _context = context;
        }

        // GET: Menu
        public async Task<IActionResult> Index()
        {
          var viewModel = await _context.Menus
                .Select(m => new MenuViewModel
                {
                    MenuId = m.MenuId,
                    MenuName = m.MenuName
                })
                .ToListAsync();

            return View(viewModel);
        }

        // GET: Menu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .Include(m => m.MenuFoodItems)
                    .ThenInclude(mf => mf.FoodItem)
                .FirstOrDefaultAsync(m => m.MenuId == id);

            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }


        // GET: Menu/Create
        public IActionResult Create()
        {
            var viewModel = new MenuViewModel
            {
                FoodItemOptions = _context.FoodItems.Select(fi => new SelectListItem
                {
                    Value = fi.FoodItemId.ToString(),
                    Text = fi.Name
                }).ToList()
            };

            return View(viewModel);
        }
        // POST: Menu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var menu = new Menu
                {
                    MenuName = viewModel.MenuName,
                    MenuFoodItems = viewModel.SelectedFoodItemIds
                        .Select(id => new MenuFoodItems
                        {
                            FoodItemId = id
                        }).ToList()
                };

                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate options if ModelState is invalid
            viewModel.FoodItemOptions = await _context.FoodItems
                .Select(fi => new SelectListItem
                {
                    Value = fi.FoodItemId.ToString(),
                    Text = fi.Name
                }).ToListAsync();

            return View(viewModel);
        }


        // GET: Menu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var menu = await _context.Menus
                .Include(m => m.MenuFoodItems)
                .FirstOrDefaultAsync(m => m.MenuId == id);


            if (menu == null) return NotFound();

            var viewModel = new MenuViewModel
            {
                MenuId = menu.MenuId,
                MenuName = menu.MenuName,
                SelectedFoodItemIds = menu.MenuFoodItems.Select(mfi => mfi.FoodItemId).ToList(),
                FoodItemOptions = _context.FoodItems.Select(fi => new SelectListItem
                {
                    Value = fi.FoodItemId.ToString(),
                    Text = fi.Name
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: Menu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuViewModel viewModel)
        {
            if (id != viewModel.MenuId) return NotFound();

            if (!ModelState.IsValid)
            {
                viewModel.FoodItemOptions = await _context.FoodItems
                    .Select(fi => new SelectListItem
                    {
                        Value = fi.FoodItemId.ToString(),
                        Text = fi.Name
                    }).ToListAsync();
                return View(viewModel);
            }

            var menu = await _context.Menus
                .Include(m => m.MenuFoodItems)
                .FirstOrDefaultAsync(m => m.MenuId == id);

            if (menu == null) return NotFound();

            menu.MenuName = viewModel.MenuName;

            // Clear and reassign food items
            menu.MenuFoodItems.Clear();
            foreach (var foodItemId in viewModel.SelectedFoodItemIds)
            {
                menu.MenuFoodItems.Add(new MenuFoodItems
                {
                    MenuId = id,
                    FoodItemId = foodItemId
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Menu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.MenuId == id);

            if (menu == null) return NotFound();

            var viewModel = new MenuViewModel
            {
                MenuId = menu.MenuId,
                MenuName = menu.MenuName
            };

            return View(viewModel);
        }

        // POST: Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menus
                .Include(m => m.MenuFoodItems)
                .FirstOrDefaultAsync(m => m.MenuId == id);

            if (menu == null)
            {
                return NotFound();
            }

            // First remove related join table entries
            _context.MenuFoodItems.RemoveRange(menu.MenuFoodItems);

            // Then remove the Menu itself
            _context.Menus.Remove(menu);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
