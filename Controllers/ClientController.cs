using CRM_ManagementInterface.Data;
using CRM_ManagementInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRM_ManagementInterface.Controllers
{
    public class ClientController : Controller
    {
        private readonly CRMContext _context;

        public ClientController(CRMContext context)
        {
            _context = context;
        }

        // Index Action
        public async Task<IActionResult> Index()
        {
            var clients = await _context.Clients
                .Include(c => c.Title)
                .Include(c => c.ClientType)
                .ToListAsync();

            return View(clients);
        }

        // Create Actions
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Titles = new SelectList(_context.Title, "TitleId", "TitleName");
            ViewBag.ClientTypes = new SelectList(_context.ClientTypes, "ClientTypeId", "ClientTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Clients client)
        {
            if (ModelState.IsValid)
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Titles = new SelectList(_context.Title, "TitleId", "TitleName", client.TitleId);
            ViewBag.ClientTypes = new SelectList(_context.ClientTypes, "ClientTypeId", "ClientTypeName", client.ClientTypeId);
            return View(client);
        }

        // Edit Actions
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Title)
                .Include(c => c.ClientType)
                .FirstOrDefaultAsync(c => c.ClientId == id);

            if (client == null)
            {
                return NotFound();
            }

            // Populate dropdown lists
            ViewBag.Titles = new SelectList(_context.Title, "TitleId", "TitleName", client.TitleId);
            ViewBag.ClientTypes = new SelectList(_context.ClientTypes, "ClientTypeId", "ClientTypeName", client.ClientTypeId);

            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,TitleId,Name,Surname,Email,ContactNumber,Address,ClientTypeId")] Clients client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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

            // Repopulate dropdown lists in case of validation errors
            ViewBag.Titles = new SelectList(_context.Title, "TitleId", "TitleName", client.TitleId);
            ViewBag.ClientTypes = new SelectList(_context.ClientTypes, "ClientTypeId", "ClientTypeName", client.ClientTypeId);

            return View(client);
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(c => c.ClientId == id);
        }


        // Delete Actions
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Title)
                .Include(c => c.ClientType)
                .FirstOrDefaultAsync(m => m.ClientId == id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

       
    }
}

