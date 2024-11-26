namespace Class_Library;

public class Order
{
    public int OrderNumber { get; set; }
    public DateTime DateTime { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public float TaxAmount { get; private set; }
    public float TariffAmount { get; private set; }
    public float TotalAmount { get; private set; }

    private List<OrderDetail> orderDetails = new List<OrderDetail>();

    public Order(int orderNumber, string customerName, string customerPhone)
    {
        OrderNumber = orderNumber;
        DateTime = DateTime.Now;
        CustomerName = customerName;
        CustomerPhone = customerPhone;
    }

    public void AddNewOrder(OrderDetail detail)
    {
        orderDetails.Add(detail);
        CalculateTotalAmount();
    }

    public void CalculateTotalAmount()
    {
        // 
        TotalAmount = orderDetails.Sum(detail => detail.SubTotal);

        // 
        AddTax();
    }

    private void AddTax()
    {
        TaxAmount = TotalAmount * 0.10f;
        TotalAmount += TaxAmount;
    }
}
