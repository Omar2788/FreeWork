using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projet2nd.Areas.Identity.Data;
using Projet2nd.Models;
using System.Security.Claims;

namespace Projet2nd.Controllers
{
    public class ServiceController : Controller
    {
        private readonly Projet2ndContext context;

        public ServiceController(Projet2ndContext context)
        {
            this.context = context;
        }
        // GET: ServiceController
        public ActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Service> userServices = context.Services.Where(x => x.idUser == userId).ToList();
            return View(userServices);
            
        }

        // GET: ServiceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ServiceController/Create
        public ActionResult Create()
        {
            TempData["IsAddingService"] = 1;
            return View();
        }

        // POST: ServiceController/Create
        // POST: ServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Service service)
        {
            try
            {
                
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    service.idUser = userId;

                if (context.Services.Where(x => x.nameService == service.nameService).Count() > 0)
                {
                    ViewBag.error = "le service existe";
                    return View(service);
                }
                if (service.ImageFile != null && service.ImageFile.Length > 0)
                {
                    // Enregistrez l'image sur le serveur

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(service.ImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName);


                    using (var Stream = new FileStream(filePath, FileMode.Create))
                    {
                        service.ImageFile.CopyTo(Stream);

                    }
                    service.imageService = fileName;

                }
                if (string.IsNullOrEmpty(service.etatService))
                {
                    service.etatService = "Available";
                }
                context.Services.Add(service);
                    context.SaveChanges();

                    // Check the user's role and set ViewBag.IsVendeur
                    ViewBag.IsVendeur = User.IsInRole("vendeur");

                    return RedirectToAction(nameof(Index));
                
                
            }
            catch
            {
                return View();
            }
        }



        // GET: ServiceController/Edit/5
        public ActionResult Edit(int id)
        {
            Service service = context.Services.Find(id);
            return View(service);
        }

        // POST: ServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Service service)
        {
            try
            {
                Service existingService = context.Services.Find(id);

                if (service.ImageFile != null && service.ImageFile.Length > 0)
                {
                    // Save the new image to the server
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(service.ImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        service.ImageFile.CopyTo(stream);
                    }

                    // Update the imageService property with the new file name
                    existingService.imageService = fileName;
                }

                // Update other properties
                existingService.nameService = service.nameService;
                existingService.prixService = service.prixService;
                existingService.etatService = service.etatService;
                existingService.descriptionService = service.descriptionService;

                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: ServiceController/Delete/5
        public ActionResult Delete(int id)
        {
            Service service = context.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: ServiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Service service)
        {
            try
            {
                context.Services.Remove(service);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult PurchasedServices()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var purchasedServices = context.Commande
                .Where(commande => context.Services.Any(service => service.IdService == commande.ServiceId && service.idUser == userId))
                .Select(commande => new PurchasedServiceViewModel
                {
                    ServiceId = commande.ServiceId,
                    ServiceName = commande.Service.nameService,
                    ServiceDescription = commande.Service.descriptionService,
                    ServicePrice = commande.Service.prixService,
                    BuyerUsername = context.Users.Where(user => user.Id == commande.UserId).Select(user => user.UserName).FirstOrDefault(),
                    
                })
                .ToList();

            return View(purchasedServices);
        }

    }
}
