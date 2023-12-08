using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Projet2nd.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Projet2ndUser class
public class Projet2ndUser : IdentityUser
{
    public string Name{ get; set; }

}

