using OrderManagement.DataModels.Models;
using System.Globalization;
using System.Xml.Linq;

namespace OrderManagement.CsvsToXmlGenerator.CsvToXmlConversion
{
    public class CsvParser
    {
        public XDocument ConvertCsvsToXml(List<string> csvPaths)
        {
            XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));
            XElement root = new XElement("Orders");
            xmlDoc.Add(root);

            foreach (string csvPath in csvPaths)
            {
                var orders = new List<Order>(); // List of Order objects


                using (StreamReader reader = new StreamReader(csvPath))//Clear resources after work done
                {
                    string headerLine = reader.ReadLine()?.Trim('|');
                    if (headerLine == null)//File is empty
                    {
                        throw new InvalidOperationException($"The file is empty.");
                    }

                    string[] headers = headerLine.Split('|');

                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {

                        line = line.Trim('|');
                        string[] fields = line.Split('|');

                        if (fields.Length != headers.Length)
                        {
                            throw new FormatException(
                                $"Mismatched field count in file {csvPath}. Expected {headers.Length} fields but got {fields.Length} fields.");
                        }


                        var order = new Order()// Create an Order object
                        {
                            OrderNumber = fields[0],
                            OrderLineNumber = fields[1],
                            ProductNumber = fields[2],
                            Quantity = int.TryParse(fields[3], out var qty)
                                ? qty
                                : throw new FormatException($"Invalid quantity format: '{fields[3]}'"),
                            Name = fields[4],
                            Description = string.IsNullOrWhiteSpace(fields[5]) ? null : fields[5],
                            Price = double.TryParse(fields[6].Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var price)
                                ? price
                                : throw new FormatException($"Invalid price format: '{fields[6]}'"),
                            ProductGroup = fields[7],
                            OrderDate = DateTime.TryParse(fields[8], out var orderDate)
                                ? orderDate
                                : throw new FormatException($"Invalid date format: '{fields[6]}'"),
                            CustomerName = fields[9],
                            CustomerNumber = fields[10]
                        };

                        orders.Add(order); // Add the Order object to the list



                    }
                }


                foreach (var order in orders) // Now convert the list of Order objects to XML
                {
                    XElement orderElement = new XElement("Order");
                    orderElement.Add(new XElement("OrderNumber", order.OrderNumber));
                    orderElement.Add(new XElement("OrderLineNumber", order.OrderLineNumber));
                    orderElement.Add(new XElement("ProductNumber", order.ProductNumber));
                    orderElement.Add(new XElement("Quantity", order.Quantity));
                    orderElement.Add(new XElement("Name", order.Name));
                    orderElement.Add(new XElement("Description", order.Description ?? ""));
                    orderElement.Add(new XElement("Price", order.Price));
                    orderElement.Add(new XElement("ProductGroup", order.ProductGroup));
                    orderElement.Add(new XElement("OrderDate", order.OrderDate));
                    orderElement.Add(new XElement("CustomerName", order.CustomerName));
                    orderElement.Add(new XElement("CustomerNumber", order.CustomerNumber));

                    root.Add(orderElement);
                }
            }

            return xmlDoc;
        }
    }
}
