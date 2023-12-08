using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Projet2nd.Models
{
    public class Service
    {

        [Key]
        public int IdService{ get; set; }

        public string nameService { get; set; }
        public string descriptionService { get; set; }
        public string prixService { get; set; }

        public string imageService { get; set; }
       
        public string etatService { get; set; }

        public string idUser { get; set; }



    }
}
