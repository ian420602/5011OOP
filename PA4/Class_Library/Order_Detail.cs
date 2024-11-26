namespace Class_Library;

public class OrderDetail
{
    public int DetailNumber { get; set; }
    public string StockID { get; set; }
    public string StockName { get; set; }
    public float StockPrice { get; set; }
    public int Quantity { get; set; }
    public float SubTotal { get; private set; }

    public OrderDetail(int detailNumber, string stockID, string stockName, float stockPrice, int quantity)
    {
        DetailNumber = detailNumber;
        StockID = stockID;
        StockName = stockName;
        StockPrice = stockPrice;
        Quantity = quantity;
        CalculateSubTotal();
    }

    public void CalculateSubTotal()
    {
        SubTotal = StockPrice * Quantity;

        //  "ELECT" case
        if (StockID.StartsWith("ELECT"))
        {
            SubTotal += SubTotal * 0.05f;
        }
    }
}
