using ppedv.TastyToGo.Model.Contracts;
using ppedv.TastyToGo.Model.DomainModel;
using System.ComponentModel;

namespace ppedv.TastyToGo.Core
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork uow;

        public CustomerService(IUnitOfWork uow)
        {
            this.uow = uow;

        }

        public bool DoesCustomerGetRabatt(Customer customer)
        {
            return customer.Name.StartsWith("A");
        }
    }

}
