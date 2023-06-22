using ppedv.TastyToGo.Model.Contracts;
using ppedv.TastyToGo.Model.DomainModel;
using System.ComponentModel;

namespace ppedv.TastyToGo.Core
{
    public class CustomerService : ICustomerService
    {
        private IRepository repo;

        public CustomerService(IRepository repo)
        {
            this.repo = repo;
            throw new NotImplementedException();

        }

        public bool DoesCustomerGetRabatt(Customer customer)
        {
            return customer.Name.StartsWith("A");
        }
    }

}
