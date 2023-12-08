using Microsoft.AspNetCore.Identity;
using Projet2nd.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet2nd.Models
{
    public class Commande
    {
        [Key]
        public int IdCommande { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; } // Foreign key for the user

        [ForeignKey("Service")]
        public int ServiceId { get; set; } // Foreign key for the service

        public virtual Projet2ndUser User { get; set; } // Navigation property for the user
        public virtual Service Service { get; set; } // Navigation property for the service


    }
}
