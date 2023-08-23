namespace TEST.Models
{
    public class FoodOrderModel
    {
            public int Food_order_id { get; set; }
            public int customer_id { get; set; }
            public int adress_id { get; set; }
            public int driver_id { get; set; }
            public int status_id { get; set; }
            public int restaurant_id { get; set; }
            public int deliveryfee { get; set; }
            public decimal totalamount { get; set; }
            public DateTime order_datetime { get; set; }
            public DateTime req_datetime { get; set; }
    }
}
