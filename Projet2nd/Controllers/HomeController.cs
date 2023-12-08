using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet2nd.Areas.Identity.Data;
using Projet2nd.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Projet2ndContext context;
    private readonly UserManager<Projet2ndUser> _userManager;

    public HomeController(ILogger<HomeController> logger, Projet2ndContext context, UserManager<Projet2ndUser> userManager)
    {
        _logger = logger;
        this.context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var isVendeur = User.IsInRole("vendeur");
        List<Service> services = context.Services.ToList();

        // Set TempData variable based on whether the user is adding a service or buying
        TempData["IsAddingService"] = isVendeur ? 1 : 0;

        ViewBag.IsVendeur = isVendeur;
        return View(services);
    }

    [HttpPost]
    [HttpPost]
    public ActionResult BuyAction(int serviceId)
    {
        if (User.Identity.IsAuthenticated)
        {
            var isVendeur = User.IsInRole("vendeur");

            if (isVendeur)
            {
                // Redirect for vendeur
                return RedirectToAction("VendeurAction", new { serviceId = serviceId });
            }
            else
            {
                // Check if the user is already in the "acheteur" role
                if (User.IsInRole("acheteur"))
                {
                    // Redirect for buyer
                    return RedirectToAction("BuyerAction", new { serviceId = serviceId });
                }
                else
                {
                    // User is authenticated but not in the "vendeur" or "acheteur" role
                    // Redirect to the login page
                    return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("BuyAction", "Home", new { serviceId = serviceId }) });
                }
            }
        }
        else
        {
            // User is not authenticated, redirect to login
            return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("BuyAction", "Home", new { serviceId = serviceId }) });
        }
    }
    public ActionResult Details(int id)
    {
        Service service = context.Services.SingleOrDefault(x => x.IdService == id);
        return View(service);
    }
    public ActionResult LoginOrRegister(int serviceId)
    {
        ViewBag.ServiceId = serviceId;
        return View();
    }

    public ActionResult VendeurAction(int serviceId)
    {
        ViewBag.Message = "Vendeurs cannot buy services";
        return View();
    }

    public ActionResult BuyerAction(int serviceId)
    {
        ViewBag.Message = "Buyers can buy services";
        return View();
    }


    [HttpPost]
    public ActionResult ConfirmPurchase(int serviceId)
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Commande commande = new Commande();

            commande.UserId = userId;
            commande.ServiceId = serviceId;

            context.Commande.Add(commande);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        else
        {
            return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("ConfirmPurchase", "Home", new { serviceId = serviceId }) });
        }
    }


    public ActionResult Commande()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var userServices = context.Services
        .Where(service => context.Commande.Any(command => command.ServiceId == service.IdService && command.UserId == userId))
        .ToList();

        return View(userServices);
    }



}
