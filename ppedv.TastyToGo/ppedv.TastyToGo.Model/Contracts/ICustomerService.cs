using ppedv.TastyToGo.Model.DomainModel;

namespace ppedv.TastyToGo.Model.Contracts
{
    public interface ICustomerService
    {
        bool DoesCustomerGetRabatt(Customer customer);
    }
}