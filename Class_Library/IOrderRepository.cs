namespace Class_Library;

public interface IOrderRepository
{
    void SaveOrder(Order order);
    Order LoadOrder(int orderNumber);
}