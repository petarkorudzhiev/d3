using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3.Sales.Application.CommandModels
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            Messages = new List<string>();
        }
        public List<string> Messages { get; set; }

        public void AddMessage(string message)
        {
            Messages.Add(message);
        }
    }
}
