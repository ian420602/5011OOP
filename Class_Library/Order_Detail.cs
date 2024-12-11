namespace Class_Library;

// Represents an individual item in an order

public class OrderDetail
{
    public int DetailNumber { get; set; }  // Detail unique identifier
    public string StockID { get; set; }    // Stock identifier
    public string StockName { get; set; }  // Name of the stock item
    public float StockPrice { get; set; }  // Price per unit of the stock item
    public int Quantity { get; set; }      // Quantity of the item ordered
    public float SubTotal { get; private set; } // Subtotal of the order detail (Price * Quantity)


    // Constructor for a new order detail.
   
    public OrderDetail(int detailNumber, string stockID, string stockName, float stockPrice, int quantity)
    {
        // Precondition: Ensure stock ID and quantity are valid.
        if (string.IsNullOrWhiteSpace(stockID)) throw new ArgumentException("Stock ID cannot be empty.");
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than 0.");

        DetailNumber = detailNumber;
        StockID = stockID;
        StockName = stockName;
        StockPrice = stockPrice;
        Quantity = quantity;
    }


    // Calculates the subtotal for the order detail based on stock price and quantity.
    public float CalculateSubTotal()
    {
        // Pre: Ensure stock price and quantity are valid.
        if (StockPrice <= 0 || Quantity <= 0)
            throw new InvalidOperationException("Stock price and quantity must be greater than zero.");

        // Calculate and return subtotal
        SubTotal = StockPrice * Quantity;

        // Post: Subtotal must be greater than zero.
        if (SubTotal <= 0)
            throw new Exception("Subtotal should be greater than 0.");

        return SubTotal;
    }
    public OrderDetail DeepCopy()
    {
        return new OrderDetail(DetailNumber, StockID, StockName, StockPrice, Quantity)
        {
            SubTotal = this.SubTotal // Ensure subtotal is copied
        }; 
    }
}
