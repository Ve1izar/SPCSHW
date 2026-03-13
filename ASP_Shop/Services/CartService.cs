using ASP_Shop.ViewModels;


namespace ASP_Shop.Services
{
    public class CartService
    {
        private static string Key = "fj4298ty4f9248yfwfoi92u4yf1fwhj";

        public static void SetItems(ISession session, IEnumerable<SessionCartItemVM> items) => session.Set(Key, items);

        public static List<SessionCartItemVM> GetItems(ISession session)
        {
            var items = session.Get<List<SessionCartItemVM>>(Key);
            return items ?? new List<SessionCartItemVM>();
        }

        public static int GetCount(ISession session) => GetItems(session).Sum(i => i.Count);

        public static void AddToCart(ISession session, int productId)
        {
            var items = GetItems(session);
            var existing = items.FirstOrDefault(i => i.ProductId == productId);

            if (existing == null) items.Add(new SessionCartItemVM { ProductId = productId, Count = 1 });
            else existing.Count++;

            SetItems(session, items);
        }

        public static void Decrease(ISession session, int productId)
        {
            var items = GetItems(session);
            var existing = items.FirstOrDefault(i => i.ProductId == productId);

            if (existing != null)
            {
                existing.Count--;
                if (existing.Count < 1) items.Remove(existing);
            }
            SetItems(session, items);
        }

        public static void RemoveFromCart(ISession session, int productId)
        {
            var items = GetItems(session);
            items.RemoveAll(i => i.ProductId == productId);
            SetItems(session, items);
        }
    }
}
