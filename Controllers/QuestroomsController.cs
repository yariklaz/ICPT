using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuestBooking.Domain.Model;
using QuestBooking.Infrastructure;

namespace QuestBooking.Infrastructure.Controllers
{
    public class QuestroomsController : Controller
    {
        private readonly QuestBookingIcptContext _context;

        public QuestroomsController(QuestBookingIcptContext context)
        {
            _context = context;
        }

        // GET: Questrooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questrooms.ToListAsync());
        }

        // GET: Questrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questroom = await _context.Questrooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questroom == null)
            {
                return NotFound();
            }

            return View(questroom);
        }

        // GET: Questrooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questrooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,MaxPlayers,BasePrice,DurationMinutes,Id")] Questroom questroom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questroom);
        }

        // GET: Questrooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questroom = await _context.Questrooms.FindAsync(id);
            if (questroom == null)
            {
                return NotFound();
            }
            return View(questroom);
        }

        // POST: Questrooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,MaxPlayers,BasePrice,DurationMinutes,Id")] Questroom questroom)
        {
            if (id != questroom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questroom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestroomExists(questroom.Id))
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
            return View(questroom);
        }

        // GET: Questrooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questroom = await _context.Questrooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questroom == null)
            {
                return NotFound();
            }

            return View(questroom);
        }

        // POST: Questrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questroom = await _context.Questrooms.FindAsync(id);
            if (questroom != null)
            {
                _context.Questrooms.Remove(questroom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestroomExists(int id)
        {
            return _context.Questrooms.Any(e => e.Id == id);
        }
    }
}
