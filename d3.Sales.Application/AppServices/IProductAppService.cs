using d3.Sales.Application.CommandModels.Product;
using d3.Sales.Application.QueryModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Application.AppServices
{
    public interface IProductAppService
    {
        ProductsViewModel GetIndexData();

        CreateProductViewModel GetCreateData();
        CreateProductViewModel GetCreateData(CreateProductViewModel model);
        void Create(CreateProductViewModel model);
    }
}
