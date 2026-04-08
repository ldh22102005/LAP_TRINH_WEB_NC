namespace QL_CUA_HANG_BAN_XE_DAP.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new();

        public int TotalQuantity => Items.Sum(x => x.Quantity);
        public double TotalAmount => Items.Sum(x => x.TotalPrice);

        public void AddItem(Product product, int quantity)
        {
            var existingItem = Items.FirstOrDefault(x => x.ProductId == product.Id);

            if (existingItem == null)
            {
                Items.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Image = product.Image,
                    Price = product.Price,
                    Quantity = quantity
                });
                return;
            }

            existingItem.Quantity += quantity;
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var item = Items.FirstOrDefault(x => x.ProductId == productId);
            if (item == null)
            {
                return;
            }

            if (quantity <= 0)
            {
                Items.Remove(item);
                return;
            }

            item.Quantity = quantity;
        }

        public void RemoveItem(int productId)
        {
            var item = Items.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}
