using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;

namespace QuanLyPhongTro.Controllers
{
    public class BaiDangsController : Controller
    {
        private readonly RoomManagementContext _context;

        public BaiDangsController(RoomManagementContext context)
        {
            _context = context;
        }

        // GET: BaiDangs
        public async Task<IActionResult> Index()
        {
            var roomManagementContext = _context.baiDangs.Include(b => b.PhongTro);
            return View(await roomManagementContext.ToListAsync());
        }

        // GET: BaiDangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.baiDangs == null)
            {
                return NotFound();
            }

            var baiDang = await _context.baiDangs
                .Include(b => b.PhongTro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baiDang == null)
            {
                return NotFound();
            }

            return View(baiDang);
        }

        // GET: BaiDangs/Create
        public IActionResult Create()
        {
            ViewData["PhongTroId"] = new SelectList(_context.phongTros, "Id", "Id");
            return View();
        }

        // POST: BaiDangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDe,NgayTao,NoiDung,Anh,TrangThai,NguoiDungID,PhongTroId")] BaiDang baiDang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baiDang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhongTroId"] = new SelectList(_context.phongTros, "Id", "Id", baiDang.PhongTroId);
            return View(baiDang);
        }

        // GET: BaiDangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.baiDangs == null)
            {
                return NotFound();
            }

            var baiDang = await _context.baiDangs.FindAsync(id);
            if (baiDang == null)
            {
                return NotFound();
            }
            ViewData["PhongTroId"] = new SelectList(_context.phongTros, "Id", "Id", baiDang.PhongTroId);
            return View(baiDang);
        }

        // POST: BaiDangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDe,NgayTao,NoiDung,Anh,TrangThai,NguoiDungID,PhongTroId")] BaiDang baiDang)
        {
            if (id != baiDang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baiDang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaiDangExists(baiDang.Id))
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
            ViewData["PhongTroId"] = new SelectList(_context.phongTros, "Id", "Id", baiDang.PhongTroId);
            return View(baiDang);
        }

        // GET: BaiDangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.baiDangs == null)
            {
                return NotFound();
            }

            var baiDang = await _context.baiDangs
                .Include(b => b.PhongTro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baiDang == null)
            {
                return NotFound();
            }

            return View(baiDang);
        }

        // POST: BaiDangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.baiDangs == null)
            {
                return Problem("Entity set 'RoomManagementContext.baiDangs'  is null.");
            }
            var baiDang = await _context.baiDangs.FindAsync(id);
            if (baiDang != null)
            {
                _context.baiDangs.Remove(baiDang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaiDangExists(int id)
        {
          return (_context.baiDangs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
