namespace OrderManagement.DataModels.Models
{
    public class Order
    {
        private string _orderNumber;
        private int _quantity;
        private double _price;

        public string OrderNumber
        {
            get { return _orderNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Order number cannot be null or empty.");

                _orderNumber = value;
            }
        }

        public string OrderLineNumber { get; set; }
        public string ProductNumber { get; set; }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Quantity cannot be negative.");
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public double Price
        {
            get { return _price; }
            set
            {
                if (value < 0) 
                    throw new ArgumentException("Price cannot be negative.");

                _price = value;
            }

            
        }
        public string ProductGroup { get; set; }
        public DateTime OrderDate { get; set; }

        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
    }
}
