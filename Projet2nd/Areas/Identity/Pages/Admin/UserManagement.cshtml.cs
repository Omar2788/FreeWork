using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projet2nd.Areas.Identity.Data;

namespace Projet2nd.Areas.Identity.Pages.Admin
{
    public class UserManagementModel : PageModel
    {
        private readonly UserManager<Projet2ndUser> _userManager;

        public UserManagementModel(UserManager<Projet2ndUser> userManager)
        {
            _userManager = userManager;
        }

        public List<Projet2ndUser> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _userManager.Users.ToListAsync();
        }

        public async Task OnPostDeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            RedirectToPage("./UserManagement");
        }
    }
}
