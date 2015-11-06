using d3.Sales.Application.CommandModels.Voucher;
using d3.Sales.Domain.Products;
using d3.Sales.Domain.Vouchers;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace d3.WebUI.Controllers
{
    public class VoucherController : Controller
    {
        private ISession _session;
        private IVoucherRepository _voucherRepository;
        private IVoucherCodeGenerator _codeGenerator;

        public VoucherController(ISession session, IVoucherRepository voucherRepository, IVoucherCodeGenerator codeGenerator)
        {
            _session = session;
            _voucherRepository = voucherRepository;
            _codeGenerator = codeGenerator;
        }

        // GET: Voucher
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateVoucherViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (ITransaction transaction = _session.BeginTransaction())
            {
                var voucher = Voucher.CreateVoucher(model.CustomerId, new Money(model.Amount), _codeGenerator);

                _voucherRepository.Add(voucher);

                transaction.Commit();
            }

            return RedirectToAction("Index");
        }
    }
}