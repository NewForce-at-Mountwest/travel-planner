using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Models;
using TravelGuide.Models.ViewModels;

namespace TravelGuide.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public TripsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Gives us access to the user that's currently logged in
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Trips
        public async Task<IActionResult> Index(bool viewPastTrips, string searchQuery)
        {

            ApplicationUser user = await GetCurrentUserAsync();
            List<Trip> tripList = await _context.Trip
                .Include(t => t.client)
                .OrderBy(t => t.StartDate)
                .Where(t => t.client.ApplicationUserId == user.Id) // Only get trips for clients this user has created
                .ToListAsync();

            // First, check to see if the user searched anything. If so, we'll filter by that
            if (searchQuery != null)
            {
                // .Contains() is case sensitive, we should normalize the search query to make it case insensitive
                tripList = tripList.Where(t => t.Location.Contains(searchQuery)).ToList();
                
            }

           
            if (viewPastTrips)
            {
                tripList = tripList.Where(t => t.EndDate < DateTime.Now).ToList();
            }
            else
            {
                tripList = tripList.Where(t => t.EndDate > DateTime.Now).ToList();
            }


            return View(tripList);
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Trip trip = await _context.Trip
                .Include(t => t.client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: Trips/Create
        public async Task<IActionResult> Create()
        {

            ApplicationUser user = await GetCurrentUserAsync();
            TripViewModel vm = new TripViewModel()
            {
                Clients = new SelectList(_context.Client.Where(c => c.ApplicationUserId == user.Id), "Id", "FirstName")
            };


            return View(vm);
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TripViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vm.trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ApplicationUser user = await GetCurrentUserAsync();
            vm.Clients = new SelectList(
                _context.Client
                .Where(c => c.ApplicationUserId == user.Id),
                "Id", 
                "FirstName");
            return View(vm);
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentTrip = await _context.Trip.FindAsync(id);
            if (currentTrip == null)
            {
                return NotFound();
            }

            
            ApplicationUser user = await GetCurrentUserAsync();
            TripViewModel vm = new TripViewModel()
            {
                trip = currentTrip,
                Clients = new SelectList(_context.Client.Where(c => c.ApplicationUserId == user.Id), "Id", "FirstName")
            };
            
            return View(vm);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TripViewModel vm)
        {
            if (id != vm.trip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vm.trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(vm.trip.Id))
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
            // Todo: refactor to view model
            ApplicationUser user = await GetCurrentUserAsync();
            vm.Clients = new SelectList(_context.Client.Where(c => c.ApplicationUserId == user.Id), "Id", "FirstName")
            ;
            return View(vm);
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip
                .Include(t => t.client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _context.Trip.FindAsync(id);
            _context.Trip.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int id)
        {
            return _context.Trip.Any(e => e.Id == id);
        }
    }
}
