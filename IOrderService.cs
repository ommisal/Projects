using OrderManagement.DataModels.Models;
namespace OrderManagement.RetriveAPI.Interface;
public interface IOrderService
{
    List<Order> GetOrdersByNumberFromXml(string orderNumber);
}
