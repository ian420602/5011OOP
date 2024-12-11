namespace Class_Library;


// Represents an order

public class Order
{
    public int OrderNumber { get; set; }           // Order unique identifier
    public DateTime DateTime { get; set; }         // Date and time when the order was made
    public string CustomerName { get; set; }       // Name of the customer
    public string CustomerPhone { get; set; }      // Phone number of the customer
    public float TaxAmount { get; private set; }   // Tax applied to the total order amount
    public float TariffAmount { get; private set; } // Tariff applied to certain items
    public float TotalAmount { get; private set; } // Total amount of the order including tax and tariff
    public List<OrderDetail> OrderDetails { get; set; } // List of items in the order


    // Constructor to initialize an order with necessary information.
    public Order(int orderNumber, string customerName, string customerPhone)
    {
        // Pre: Ensure customer name and phone number are provided.
        if (string.IsNullOrWhiteSpace(customerName)) throw new ArgumentException("Customer name cannot be empty.");
        if (string.IsNullOrWhiteSpace(customerPhone)) throw new ArgumentException("Customer phone cannot be empty.");
        if (orderNumber < 0) throw new ArgumentException("Order number must be non-negative.");

        // Initialize properties
        DateTime = DateTime.Now;
        OrderNumber = orderNumber;
        CustomerName = customerName;
        CustomerPhone = customerPhone;
        OrderDetails = new List<OrderDetail>();

        // Post: Ensure the OrderDetails list is initialized.
        if (OrderDetails == null) throw new Exception("Order details list must be initialized.");
    }


    // Calculates the total amount for the order, including tax and tariff.
    public void CalculateTotalAmount()
    {
        // Pre: Ensure there is at least one detail item in the order.
        if (OrderDetails == null || OrderDetails.Count == 0)
            throw new InvalidOperationException("Order must have at least one detail item.");

        float subtotal = 0;

        // Sum 
        foreach (var detail in OrderDetails)
        {
            subtotal += detail.CalculateSubTotal();
        }

        // Apply tax and tariff
        TariffAmount = 0;
        TaxAmount = subtotal * 0.10f; // 10% tax
        TotalAmount = subtotal + TaxAmount;

        // Post: The total amount must be correctly calculated.
        if (TotalAmount < 0)
            throw new Exception("Total amount should not be negative.");
    }


    // Adds tax to the total amount.
    public void AddTax()
    {
        // Pre: Ensure that the total amount has been calculated before applying tax.
        if (TotalAmount == 0)
            throw new InvalidOperationException("Total amount must be calculated before adding tax.");

        // Apply tax (10% of total amount)
        TaxAmount = TotalAmount * 0.10f;
        TotalAmount += TaxAmount;

        // Post: Ensure tax amount is added correctly.
        if (TaxAmount <= 0)
            throw new Exception("Tax amount should be greater than 0.");
    }


    // Checks the tariff for eligible items.
    public void CheckTariff()
    {
        // Pre: Ensure that the order details are not empty before checking tariff.
        if (OrderDetails == null || OrderDetails.Count == 0)
            throw new InvalidOperationException("Order must have at least one detail item to check tariff.");

        foreach (var detail in OrderDetails)
        {
            // If the StockID starts with "ELECT" apply a tariff
            if (detail.StockID.StartsWith("ELECT"))
            {
                TariffAmount += detail.CalculateSubTotal() * 0.05f; // Apply 5% tariff
            }
        }
        // Post: Ensure tariff has been correctly added.
        if (TariffAmount < 0)
            throw new Exception("Tariff amount cannot be negative.");
    }
    public Order DeepCopy()
    {
        // Create a new Order instance with primitive and simple type properties copied
        var newOrder = new Order(OrderNumber, CustomerName, CustomerPhone)
        {
            DateTime = this.DateTime, // Copy DateTime
            TaxAmount = this.TaxAmount,
            TariffAmount = this.TariffAmount,
            TotalAmount = this.TotalAmount
        };

        // Deep copy the OrderDetails list
        foreach (var detail in this.OrderDetails)
        {
            newOrder.OrderDetails.Add(detail.DeepCopy());
        }

        return newOrder;
    }
}
