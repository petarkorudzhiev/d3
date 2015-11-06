using d3.Sales.Application.CommandModels.Cart;
using d3.Sales.Domain.Carts;
using d3.Sales.Domain.Customers;
using d3.Sales.Domain.Products;
using d3.Sales.Domain.Purchases;
using d3.Sales.Domain.Vouchers;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace d3.WebUI.Controllers
{
    public class CartController : Controller
    {
        private ISession _session;
        private ICartRepository _cartRepository;
        private IProductRepository _productRepository;
        private IVoucherRepository _voucherRepository;
        private IPurchaseRepository _purchaseRepository;

        public CartController(ISession session, 
                              ICartRepository cartRepository, 
                              IProductRepository productRepository, 
                              IVoucherRepository voucherRepository, 
                              IPurchaseRepository purchaseRepository)
        {
            _session = session;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _voucherRepository = voucherRepository;
            _purchaseRepository = purchaseRepository;
        }


        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (ITransaction transaction = _session.BeginTransaction())
            {
                var cart = _cartRepository.GetCartByCustomerId(model.CustomerId);
                var product = _productRepository.GetProduct(model.ProductId);

                cart.AddProduct(product, new Quantity(model.Quantity));

                transaction.Commit();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveProduct(RemoveProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (ITransaction transaction = _session.BeginTransaction())
            {
                var cart = _cartRepository.GetCartByCustomerId(model.CustomerId);
                var product = _productRepository.GetProduct(model.ProductId);

                cart.RemoveProduct(product);

                transaction.Commit();
            }   

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ApplyVoucher(ApplyVoucherViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (ITransaction transaction = _session.BeginTransaction())
            {
                var cart = _cartRepository.GetCartByCustomerId(model.CustomerId);
                var voucher = _voucherRepository.GetVaucherByCode(model.VoucherCode);

                cart.ApplyVoucher(voucher);

                transaction.Commit();
            }  

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (ITransaction transaction = _session.BeginTransaction())
            {
                var cart = _cartRepository.GetCartByCustomerId(model.CustomerId);
                var address = new DeliveryAddress(model.City, model.Address);
                var phone = new Phone(model.Phone);

                var purchase = cart.Checkout(address, phone);

                _purchaseRepository.Add(purchase);

                transaction.Commit();
            }  

            return RedirectToAction("Index");
        }
    }
}