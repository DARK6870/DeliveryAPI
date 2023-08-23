namespace TEST.Models
{
    public class RestaurantModel
    {
        public int restaurant_id { get; set; }
        public string restaurant_name { get; set; }
        public int adress_id { get; set; }
    }

    public class RestaurantData
    {
        public string restaurant_name { get; set; }
        public int adress_id { get; set; }
    }

    public class Menu
    {
        public int restaurant_id { get; set; }
        public int item_id { get; set; }
        public string item_name { get; set; }
        public decimal item_price { get; set; }
    }
}
