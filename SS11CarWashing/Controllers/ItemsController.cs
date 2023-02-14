using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SS11CarWashing.Models;

namespace SS11CarWashing.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        public ItemsController(AppDbContext context
            , IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Item.Include(i => i.ItemType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Item == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.ItemType)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["ItemTypeId"] = new SelectList(_context.ItemType, "ItemTypeId", "ItemTypeName");
            ViewData["Brand"] = new SelectList(_context.Brand, "BrandId", "BrandName");
            ViewData["Models"] = new SelectList(_context.Models, "ModelId", "ModelName");
            ViewData["OilTypes"] = new SelectList(_context.OilType, "OilTypeId", "OilTypeName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemDTO item)
        {
            if (ModelState.IsValid)
            {
                var itm = new Item
                {
                    ItemId=Guid.NewGuid(),
                    Image=UploadFile(item),
                    IsStock=item.IsStock,
                    ItemName=item.ItemName,
                    Qty=item.Qty,
                    ItemTypeId=item.ItemTypeId,
                    OilTypeId=item.OilTypeId,
                    BrandId=item.BrandId,
                    ModelId=item.ModelId,
                    Price=item.Price,
                    Size=item.Size,
                };
                _context.Add(itm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemTypeId"] = new SelectList(_context.ItemType, "ItemTypeId", "ItemTypeName", item.ItemTypeId);
            ViewData["Brand"] = new SelectList(_context.Brand, "BrandId", "BrandName",item.BrandId);
            ViewData["Models"] = new SelectList(_context.Models, "ModelId", "ModelName",item.ModelId);
            ViewData["OilTypes"] = new SelectList(_context.OilType, "OilTypeId", "OilTypeName",item.OilTypeId);
            return View(item);
        }
        private string UploadFile(ItemDTO item)
        {
            string fileName = "";
            if(item.Image != null)
            {
                var path = _webHost.WebRootPath + "/UploadFiles/";
                fileName = item.Image.FileName;
                var fullPath = Path.Combine(path, fileName);
                using(var stream =new FileStream(fullPath, FileMode.Create))
                {
                    item.Image.CopyTo(stream);
                }
            }
            return fileName;
        }
        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Item == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["ItemTypeId"] = new SelectList(_context.ItemType, "ItemTypeId", "ItemTypeName", item.ItemTypeId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemId,ItemTypeId,Price,ItemName,ModelId,BrandId,OilTypeId,IsStock,Qty,Size,Image")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemTypeId"] = new SelectList(_context.ItemType, "ItemTypeId", "ItemTypeName", item.ItemTypeId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Item == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.ItemType)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Item == null)
            {
                return Problem("Entity set 'AppDbContext.Item'  is null.");
            }
            var item = await _context.Item.FindAsync(id);
            if (item != null)
            {
                _context.Item.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(Guid id)
        {
          return _context.Item.Any(e => e.ItemId == id);
        }
    }
}
