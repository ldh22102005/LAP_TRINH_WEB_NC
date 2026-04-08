namespace QL_CUA_HANG_BAN_XE_DAP.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }

        public double TotalPrice => Price * Quantity;
    }
}
