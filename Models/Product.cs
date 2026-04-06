namespace QL_CUA_HANG_BAN_XE_DAP.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
    }
}