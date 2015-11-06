using d3.Sales.Application.AppServices;
using d3.Sales.Application.CommandModels.Customer;
using d3.Sales.Domain.Customers;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace d3.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerAppService _appService;

        public CustomerController(ICustomerAppService appService)
        {
            _appService = appService;
        }

        // GET: Customer
        public ActionResult Index()
        {
            var model = _appService.GetIndexData();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Create(CreateCustomerViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _appService.Create(model);
            }
            catch(ApplicationException ex)
            {
                model.AddMessage(ex.Message);
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}