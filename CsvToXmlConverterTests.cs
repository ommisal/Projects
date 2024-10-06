using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OrderManagement.CsvsToXmlGenerator.CsvToXmlConversion;

namespace OrderManagement.Tests.CsvToXmlConversion
{
    public class CsvToXmlConverterTests
    {
        private readonly CsvParser _parser;//Created to have consistent instance throughout the unit tests.

        public CsvToXmlConverterTests()//as readonly used, value assinged in constructor
        {
            _parser = new CsvParser();
        }

        [Fact]
        public void ValidCsv_ShouldReturnXpectedXml()
        {
            
            string csvContent =
                "|OrderNumber|OrderLineNumber|ProductNumber|Quantity|Name|Description|Price|ProductGroup|OrderDate|CustomerName|CustomerNumber|\r\n" +
                "|17890|0001|123451324A|1|X-Wing Starfighter|Super awesome starfighter|99.99|Star Wars|2014-01-25|Daniel Johansson|737268|\r\n";

            string tempFilePath = Path.Combine(Path.GetTempPath(), "validCsv_order.txt");
            File.WriteAllText(tempFilePath, csvContent);
            var csvPaths = new List<string> { tempFilePath };


            //Act
            XDocument result = _parser.ConvertCsvsToXml(csvPaths);

            //Assert
            var orderElements = result.Descendants("Order");
            Assert.Single(orderElements);

            File.Delete(tempFilePath);
        }
        [Fact]
        public void MultipleOrders_ShouldReturnAllOrders()
        {
            
            string csvContent =
                "|OrderNumber|OrderLineNumber|ProductNumber|Quantity|Name|Description|Price|ProductGroup|OrderDate|CustomerName|CustomerNumber|\r\n" +
                "|17890|0001|123451324A|1|X-Wing Starfighter|Super awesome starfighter|99.99|Star Wars|2014-01-25|Daniel Johansson|737268|\r\n"+ "|17890|0002|4000-AAA|2|3x2 Red piece||0.15|Normal|2014-01-25|Daniel Johansson|737268|";

            string tempFilePath = Path.Combine(Path.GetTempPath(), "validCsv_order.txt");
            File.WriteAllText(tempFilePath, csvContent);
            var csvPaths = new List<string> { tempFilePath };


            //Act
            XDocument result = _parser.ConvertCsvsToXml(csvPaths);

            //Assert
            var orderElements = result.Descendants("Order");
            Assert.Equal(2,orderElements.Count());

            File.Delete(tempFilePath);
        }

        [Fact]
        public void emptyCsv_ShouldThrowInvalidOperation()
        {
            //Arrange
            string tempFilePath = Path.Combine(Path.GetTempPath(), "emptyCsv.txt");
            File.WriteAllText(tempFilePath, "");
            var csvPaths = new List<string> { tempFilePath };

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _parser.ConvertCsvsToXml(csvPaths));

            //Assert
            Assert.Contains($"The file is empty.", exception.Message);


            File.Delete(tempFilePath);
        }
        [Fact]
        public void MismatchedHeaderAndFieldColumns_ThrowsFormatException()
        {
            // Arrange
            string csvContent = "OrderNumber|OrderLineNumber|ProductNumber|Quantity|Price\n" +
                                "123|1|P001|2|10.00|ExtraField"; // added extra field in data row
            string tempFilePath = Path.Combine(Path.GetTempPath(), "mismatched_columns.txt");
            File.WriteAllText(tempFilePath, csvContent);
            var csvPaths = new List<string> { tempFilePath };

            // Act & Assert
            var exception = Assert.Throws<FormatException>(() => _parser.ConvertCsvsToXml(csvPaths));
            Assert.Contains("Mismatched field count in file", exception.Message);
            Assert.Contains("Expected 5 fields but got 6 fields", exception.Message);

            // Clean up
            File.Delete(tempFilePath);
        }

        [Fact]
        public void ConvertCsvsToXml_InvalidDataFormat_ThrowsFormatException()
        {
            // Arrange
            string csvContent = "|OrderNumber|OrderLineNumber|ProductNumber|Quantity|Name|Description|Price|ProductGroup|OrderDate|CustomerName|CustomerNumber|\r\n" +
                                "|17890|0001|123451324A|1|X-Wing Starfighter|Super awesome starfighter|Om|Star Wars|2014-01-25|Daniel Johansson|737268|\r\n"; // Invalid quantity
            string tempFilePath = Path.Combine(Path.GetTempPath(), "invalid_data.txt");
            File.WriteAllText(tempFilePath, csvContent);
            var csvPaths = new List<string> { tempFilePath };

            // Act and Assert
            var exception = Assert.Throws<FormatException>(() => _parser.ConvertCsvsToXml(csvPaths));
            
            
            
            File.Delete(tempFilePath);
        }

    }
}
