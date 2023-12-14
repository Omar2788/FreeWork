using System.ComponentModel.DataAnnotations;

namespace Projet2nd.Models
{
    public class PurchasedServiceViewModel
    {
        [Key]
        public int Id { get; set; }
        public int ServiceId { get; set; }
       
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string ServicePrice { get; set; }
        public string BuyerUsername { get; set; }
    }
}
