using OrderManagement.DataModels.Models;
using OrderManagement.RetriveAPI.Interface;
using System.Xml.Serialization;
namespace OrderManagement.RetriveAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly string _xmlFilePath; //to store path of Xml file 


        public OrderService(IConfiguration configuration)//used DI using constructor.
        {
            _xmlFilePath = configuration.GetValue<string>("XmlSettings:XmlFilePath");
        }
        public List<Order> GetOrdersByNumberFromXml(string orderNumber)
        {

            
            if (string.IsNullOrEmpty(_xmlFilePath))// Checking if _xmlFilePath is null or empty
            {
                throw new ArgumentNullException(nameof(_xmlFilePath), "The XML file path cannot be null or empty.");
            }

            
            if (!System.IO.File.Exists(_xmlFilePath))// Check if the file exists at the specified path
            {
                throw new FileNotFoundException($"The file at path '{_xmlFilePath}' was not found.");
            }

            var serializer = new XmlSerializer(typeof(List<Order>), new XmlRootAttribute("Orders"));

            using (var stream = new FileStream(_xmlFilePath, FileMode.Open))
            {
                var orders = (List<Order>)serializer.Deserialize(stream);
                return orders.Where(o => o.OrderNumber == orderNumber).ToList(); // Filter orders by order number
            }
        }
    }
}
