using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projet2nd.Areas.Identity.Data;

using Projet2nd.Models;

namespace Projet2nd.Areas.Identity.Pages.Admin
{
    public class AllServicesModel : PageModel
    {
        private readonly Projet2ndContext _context;

        public AllServicesModel(Projet2ndContext context)
        {
            _context = context;
        }

        public List<Service> Services { get; set; }

        public async Task OnGetAsync()
        {
            Services = await _context.Services.ToListAsync();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostDeleteAsync(int serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);

            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./AllServices");
        }
    }
}
