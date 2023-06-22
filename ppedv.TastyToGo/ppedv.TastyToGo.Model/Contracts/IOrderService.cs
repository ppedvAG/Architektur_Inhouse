using ppedv.TastyToGo.Model.DomainModel;

namespace ppedv.TastyToGo.Model.Contracts
{
    public interface IOrderService
    {
        decimal CalcOrderSum(Order order);
        Customer GetBestPayingCustomer();
    }
}