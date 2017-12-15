using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using a2OEC.Models;

namespace a2OEC.Controllers
{
    public class ADFarmController : Controller
    {
        private readonly OECContext _context;

        public ADFarmController(OECContext context)
        {
            _context = context;
        }

        // GET: ADFarm
        public async Task<IActionResult> Index()
        {
            var oECContext = _context.Farm
                            .Include(f => f.ProvinceCodeNavigation)
                            //order the listing by farm name
                            .OrderBy(f => f.Name);
            return View(await oECContext.ToListAsync());
        }

        // GET: ADFarm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farm = await _context.Farm
                .Include(f => f.ProvinceCodeNavigation)
                .SingleOrDefaultAsync(m => m.FarmId == id);
            if (farm == null)
            {
                return NotFound();
            }

            return View(farm);
        }

        // GET: ADFarm/Create
        public IActionResult Create()
        {
            ViewData["ProvinceCode"] = new SelectList(_context.Province, "ProvinceCode", "ProvinceCode");
            return View();
        }

        // POST: ADFarm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FarmId,Name,Address,Town,County,ProvinceCode,PostalCode,HomePhone,CellPhone,Email,Directions,DateJoined,LastContactDate")] Farm farm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //3d. if the insert works, place a msg in your temp data
                    _context.Add(farm);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "Farm insert was successful";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //3c.add modelstate error
                    ModelState.AddModelError("Name", "Name Error");
                }
            }
            //3c
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating new item: {ex.GetBaseException().Message}");
            }
            ViewData["ProvinceCode"] = new SelectList(_context.Province, "ProvinceCode", "ProvinceCode", farm.ProvinceCode);
            return View(farm);
        }

        // GET: ADFarm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farm = await _context.Farm.SingleOrDefaultAsync(m => m.FarmId == id);
            if (farm == null)
            {
                return NotFound();
            }

            //3b.to display the province name, not code, in the drop down
            ViewData["ProvinceSelectList"] = new SelectList(_context.Province, "Name", "Name", farm.Name);
            return View(farm);
        }

        // POST: ADFarm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FarmId,Name,Address,Town,County,ProvinceCode,PostalCode,HomePhone,CellPhone,Email,Directions,DateJoined,LastContactDate")] Farm farm)
        {
            try
            {
                if (id != farm.FarmId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        //3d. if the insert works, place a msg in your temp data
                        farm.ProvinceCode = _context.Province.SingleOrDefault(p => p.Name == farm.ProvinceCode.ToString()).ProvinceCode;
                        _context.Update(farm);
                        await _context.SaveChangesAsync();
                        TempData["message"] = "Farm edit was successful";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!FarmExists(farm.FarmId))
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
            }
            catch (Exception ex)
            {
                //3c
                //catch any exception that is thrown on Edit, place its innermost message into modelstate
                //and allow processing to continue to the said path
                //which should redisplay the users data with the error
                ModelState.AddModelError("", $"Error inserting edit: {ex.GetBaseException().Message}");
            }
            //3b.to display the province name, not code, in the drop down
            ViewData["ProvinceSelectList"] = new SelectList(_context.Province, "Name", "Name", farm.Name);
            return View(farm);
        }

        // GET: ADFarm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farm = await _context.Farm
                .Include(f => f.ProvinceCodeNavigation)
                .SingleOrDefaultAsync(m => m.FarmId == id);
            if (farm == null)
            {
                return NotFound();
            }

            return View(farm);
        }

        // POST: ADFarm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(ModelState.IsValid)
            {
                //3d. if the insert works, place a msg in your temp data
                TempData["message"] = "Farm delete was successful";
                var farm = await _context.Farm.SingleOrDefaultAsync(m => m.FarmId == id);
                _context.Farm.Remove(farm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //3ci
                //put the innermost exception's message into TempData and return it to the delete view
                TempData["message"] = "Farm delete unsuccessful";
                return RedirectToAction("Delete", "ADFarm");
            }

        }

        private bool FarmExists(int id)
        {
            return _context.Farm.Any(e => e.FarmId == id);
        }
    }
}
