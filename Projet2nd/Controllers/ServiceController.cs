using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
                Service serv = context.Services.Find(id);
                serv.nameService = service.nameService;
                serv.prixService = service.prixService;
                serv.etatService = service.etatService;
                serv.descriptionService = service.descriptionService;
                serv.imageService = service.imageService;

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



    }
}
