namespace TEST.Models
{
    public class FoodItemModel
    {
        public int item_id { get; set; }
        public string item_name { get; set; }
        public decimal item_price { get; set; }
        public int category_id { get; set; }
    }

    public class FoodItemData
    {
        public string item_name { get; set; }
        public decimal item_price { get; set; }
        public int category_id { get; set; }
    }
}
