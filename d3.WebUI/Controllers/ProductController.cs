using d3.Sales.Application.AppServices;
using d3.Sales.Application.CommandModels.Product;
using d3.Sales.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace d3.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductAppService _appService;

        public ProductController(IProductAppService appService)
        {
            _appService = appService;
        }

        // GET: Product
        public ActionResult Index()
        {
            var model = _appService.GetIndexData();

            return View(model);
        }

        public ActionResult Create()
        {
            var model = _appService.GetCreateData();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = _appService.GetCreateData(model);
                return View(model);
            }

            try
            {
                _appService.Create(model);
                return RedirectToAction("Index");
            }
            catch(ApplicationException ex)
            {
                model = _appService.GetCreateData(model);
                model.AddMessage(ex.Message);
                return View(model);
            }
        }
    }
}