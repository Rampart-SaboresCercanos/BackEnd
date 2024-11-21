using System.Text.Json;
using BackEnd.Orders.Domain.Model.Command;
using Org.BouncyCastle.Asn1.Cms; //jsonserialaizer

namespace BackEnd.Orders.Domain.Model.Aggregates;

public class Order
{
    public int Id { get; set; }
    public int customerId { get; set; }
    public DateTime orderDate { get; set; }
    public DateTime deliveryDate { get; set; }
    
    public string deliveryTime { get; set; } 
    public string paymentMethod { get; set; } = string.Empty;
    //public double totalAmount { get; set; }
    public string status { get; set; }= string.Empty;
    
    public string PreferencesJson { get; set; } = string.Empty;
    
    
    //lo crea vacio
    protected Order() { }
    
    //lo crea con el command
    public Order(CreateOrderCommand command)
    {
        customerId = command.customerId;
        orderDate = command.orderDate;
        deliveryDate = command.deliveryDate;
        deliveryTime = command.deliveryTime;
        paymentMethod = command.paymentMethod;
        status = command.status;
    }
}