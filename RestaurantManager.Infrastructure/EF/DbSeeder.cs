using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Infrastructure.EF
{
    public static class DbSeeder
    {
        private const string SeedUser = "seed";

        public static async Task SeedAsync(IServiceProvider services, CancellationToken ct = default)
        {
            using var scope = services.CreateScope();

            // TODO: Rename to your actual DbContext type
            var db = scope.ServiceProvider.GetRequiredService<RestaurantManagerDbContext>();

            // --- URL helpers (deterministic placeholders) ---
            static string Img(string context, int i, int w, int h) =>
                $"https://picsum.photos/seed/{Uri.EscapeDataString($"{context}-{i}")}/{w}/{h}";

            static string RestaurantImg(string slug, int i) => Img($"restaurant-{slug}", i, 1400, 900);
            static string MenuImg(string slug, int i) => Img($"menu-{slug}", i, 1200, 800);
            static string ItemImg(string slug, int i) => Img($"item-{slug}", i, 900, 900);

            static Guid G(string s) => Guid.Parse(s);

            var now = DateTime.UtcNow;

            // --- Owners ---
            // Your Restaurant.OwnerId points to Identity user id (string). Since you didn’t provide ApplicationUser,
            // we seed with stable string ids. If you prefer real Identity users via UserManager<ApplicationUser>,
            // tell me and I’ll adapt.
            const string owner1 = "6795613f-83de-4ef4-89fa-1d50c2e228c2";
            const string owner2 = "48afd214-a145-4683-ab8b-3d988b62fe9a";

            // --------------------------
            // Categories (shared)
            // --------------------------
            var categories = new List<Category>
        {
            NewCategory(G("11111111-1111-1111-1111-111111111111"), "Starters", isActive: true, now),
            NewCategory(G("22222222-2222-2222-2222-222222222222"), "Salads",   isActive: true, now),
            NewCategory(G("33333333-3333-3333-3333-333333333333"), "Mains",    isActive: true, now),
            NewCategory(G("44444444-4444-4444-4444-444444444444"), "Desserts", isActive: true, now),
            NewCategory(G("55555555-5555-5555-5555-555555555555"), "Drinks",   isActive: true, now),
        };

            // --------------------------
            // Restaurants
            // --------------------------
            var rBulgarian = NewRestaurant(
                id: G("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                name: "Stara Planina Grill",
                description: "Traditional Bulgarian grill with fresh salads and classic favorites.",
                location: "Veliko Tarnovo, Bulgaria",
                cuisine: "Bulgarian",
                status: OpenClosed.Open,
                imgUrl: RestaurantImg("stara-planina-grill", 1),
                ownerId: owner1,
                now: now
            );

            var rJapanese = NewRestaurant(
                id: G("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                name: "Sakura Ramen House",
                description: "Cozy ramen bar with handmade noodles, gyoza and seasonal specials.",
                location: "Sofia, Bulgaria",
                cuisine: "Japanese",
                status: OpenClosed.Open,
                imgUrl: RestaurantImg("sakura-ramen-house", 1),
                ownerId: owner2,
                now: now
            );

            var rItalian = NewRestaurant(
                id: G("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                name: "La Piazza Trattoria",
                description: "Italian classics—pizza, pasta and desserts—served in a warm trattoria vibe.",
                location: "Plovdiv, Bulgaria",
                cuisine: "Italian",
                status: OpenClosed.Closed,
                imgUrl: RestaurantImg("la-piazza-trattoria", 1),
                ownerId: owner1,
                now: now
            );

            var restaurants = new List<Restaurant> { rBulgarian, rJapanese, rItalian };

            // --------------------------
            // Menus
            // --------------------------
            var menus = new List<Menu>
        {
            // Bulgarian
            NewMenu(G("a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1"), rBulgarian.Id,
                name: "Main Menu", description: "Everyday classics from the grill and fresh salads.",
                type: MenuType.Default, isActive: true, imgUrl: MenuImg("stara-planina-main", 1), now),

            NewMenu(G("a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2"), rBulgarian.Id,
                name: "Summer Menu", description: "Lighter, refreshing seasonal dishes.",
                type: MenuType.Summer, isActive: true, imgUrl: MenuImg("stara-planina-summer", 1), now),

            // Japanese
            NewMenu(G("b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1"), rJapanese.Id,
                name: "Ramen & Bites", description: "Signature ramen bowls and shareable starters.",
                type: MenuType.Default, isActive: true, imgUrl: MenuImg("sakura-ramen-default", 1), now),

            NewMenu(G("b2b2b2b2-b2b2-b2b2-b2b2-b2b2b2b2b2b2"), rJapanese.Id,
                name: "Winter Specials", description: "Warm bowls and comforting seasonal favorites.",
                type: MenuType.Winter, isActive: true, imgUrl: MenuImg("sakura-ramen-winter", 1), now),

            // Italian
            NewMenu(G("c1c1c1c1-c1c1-c1c1-c1c1-c1c1c1c1c1c1"), rItalian.Id,
                name: "Pizza & Pasta", description: "Hand-stretched pizza and classic pasta dishes.",
                type: MenuType.Default, isActive: true, imgUrl: MenuImg("la-piazza-default", 1), now),

            NewMenu(G("c2c2c2c2-c2c2-c2c2-c2c2-c2c2c2c2c2c2"), rItalian.Id,
                name: "Autumn Menu", description: "Seasonal plates inspired by autumn flavors.",
                type: MenuType.Autumn, isActive: true, imgUrl: MenuImg("la-piazza-autumn", 1), now),
        };

            // Quick category ids
            var catStarters = categories.Single(c => c.Name == "Starters").Id;
            var catSalads = categories.Single(c => c.Name == "Salads").Id;
            var catMains = categories.Single(c => c.Name == "Mains").Id;
            var catDesserts = categories.Single(c => c.Name == "Desserts").Id;
            var catDrinks = categories.Single(c => c.Name == "Drinks").Id;

            // Map menus for easy use
            var mBulMain = menus.Single(m => m.Id == G("a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1"));
            var mBulSummer = menus.Single(m => m.Id == G("a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2"));

            var mJapDefault = menus.Single(m => m.Id == G("b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1"));
            var mJapWinter = menus.Single(m => m.Id == G("b2b2b2b2-b2b2-b2b2-b2b2-b2b2b2b2b2b2"));

            var mItaDefault = menus.Single(m => m.Id == G("c1c1c1c1-c1c1-c1c1-c1c1-c1c1c1c1c1c1"));
            var mItaAutumn = menus.Single(m => m.Id == G("c2c2c2c2-c2c2-c2c2-c2c2-c2c2c2c2c2c2"));

            // --------------------------
            // Menu Items
            // --------------------------
            var items = new List<MenuItem>
        {
            // Bulgarian - Main
            NewItem(G("d1010101-0101-0101-0101-010101010101"), mBulMain.Id, catStarters, "Tarator", 6.90m, ItemImg("tarator", 1), true, now),
            NewItem(G("d1010101-0101-0101-0101-010101010102"), mBulMain.Id, catSalads,   "Shopska Salad", 8.50m, ItemImg("shopska-salad", 1), true, now),
            NewItem(G("d1010101-0101-0101-0101-010101010103"), mBulMain.Id, catMains,    "Kebapche (2 pcs)", 9.90m, ItemImg("kebapche", 1), true, now),
            NewItem(G("d1010101-0101-0101-0101-010101010104"), mBulMain.Id, catMains,    "Kyufte (2 pcs)", 10.50m, ItemImg("kyufte", 1), true, now),
            NewItem(G("d1010101-0101-0101-0101-010101010105"), mBulMain.Id, catDesserts, "Baklava", 6.50m, ItemImg("baklava", 1), true, now),
            NewItem(G("d1010101-0101-0101-0101-010101010106"), mBulMain.Id, catDrinks,   "Ayran", 3.50m, ItemImg("ayran", 1), true, now),

            // Bulgarian - Summer
            NewItem(G("d2020202-0202-0202-0202-020202020201"), mBulSummer.Id, catStarters, "Cold Cucumber Soup", 7.50m, ItemImg("cold-cucumber-soup", 1), true, now),
            NewItem(G("d2020202-0202-0202-0202-020202020202"), mBulSummer.Id, catSalads,   "Tomato & Cheese Salad", 8.20m, ItemImg("tomato-cheese-salad", 1), true, now),
            NewItem(G("d2020202-0202-0202-0202-020202020203"), mBulSummer.Id, catMains,    "Grilled Chicken Fillet", 13.90m, ItemImg("grilled-chicken", 1), true, now),
            NewItem(G("d2020202-0202-0202-0202-020202020204"), mBulSummer.Id, catDrinks,   "Homemade Lemonade", 4.90m, ItemImg("lemonade", 1), true, now),

            // Japanese - Default
            NewItem(G("e1010101-0101-0101-0101-010101010101"), mJapDefault.Id, catStarters, "Gyoza (6 pcs)", 9.80m, ItemImg("gyoza", 1), true, now),
            NewItem(G("e1010101-0101-0101-0101-010101010102"), mJapDefault.Id, catSalads,   "Seaweed Salad", 8.90m, ItemImg("seaweed-salad", 1), true, now),
            NewItem(G("e1010101-0101-0101-0101-010101010103"), mJapDefault.Id, catMains,    "Shoyu Ramen", 16.90m, ItemImg("shoyu-ramen", 1), true, now),
            NewItem(G("e1010101-0101-0101-0101-010101010104"), mJapDefault.Id, catMains,    "Chicken Teriyaki", 18.50m, ItemImg("chicken-teriyaki", 1), true, now),
            NewItem(G("e1010101-0101-0101-0101-010101010105"), mJapDefault.Id, catDesserts, "Matcha Cheesecake", 8.50m, ItemImg("matcha-cheesecake", 1), true, now),
            NewItem(G("e1010101-0101-0101-0101-010101010106"), mJapDefault.Id, catDrinks,   "Yuzu Lemonade", 5.90m, ItemImg("yuzu-lemonade", 1), true, now),

            // Japanese - Winter
            NewItem(G("e2020202-0202-0202-0202-020202020201"), mJapWinter.Id, catStarters, "Miso Soup", 5.50m, ItemImg("miso-soup", 1), true, now),
            NewItem(G("e2020202-0202-0202-0202-020202020202"), mJapWinter.Id, catMains,    "Tonkotsu Ramen", 18.90m, ItemImg("tonkotsu-ramen", 1), true, now),
            NewItem(G("e2020202-0202-0202-0202-020202020203"), mJapWinter.Id, catDrinks,   "Hot Jasmine Tea", 4.20m, ItemImg("jasmine-tea", 1), true, now),

            // Italian - Default
            NewItem(G("f1010101-0101-0101-0101-010101010101"), mItaDefault.Id, catStarters, "Bruschetta", 8.50m, ItemImg("bruschetta", 1), true, now),
            NewItem(G("f1010101-0101-0101-0101-010101010102"), mItaDefault.Id, catSalads,   "Caprese", 10.90m, ItemImg("caprese", 1), true, now),
            NewItem(G("f1010101-0101-0101-0101-010101010103"), mItaDefault.Id, catMains,    "Pizza Margherita", 16.50m, ItemImg("pizza-margherita", 1), true, now),
            NewItem(G("f1010101-0101-0101-0101-010101010104"), mItaDefault.Id, catMains,    "Spaghetti Carbonara", 17.90m, ItemImg("carbonara", 1), true, now),
            NewItem(G("f1010101-0101-0101-0101-010101010105"), mItaDefault.Id, catDesserts, "Tiramisu", 8.90m, ItemImg("tiramisu", 1), true, now),
            NewItem(G("f1010101-0101-0101-0101-010101010106"), mItaDefault.Id, catDrinks,   "Espresso", 3.20m, ItemImg("espresso", 1), true, now),

            // Italian - Autumn
            NewItem(G("f2020202-0202-0202-0202-020202020201"), mItaAutumn.Id, catStarters, "Minestrone Soup", 9.20m, ItemImg("minestrone", 1), true, now),
            NewItem(G("f2020202-0202-0202-0202-020202020202"), mItaAutumn.Id, catMains,    "Pumpkin Risotto", 18.20m, ItemImg("pumpkin-risotto", 1), true, now),
            NewItem(G("f2020202-0202-0202-0202-020202020203"), mItaAutumn.Id, catDesserts, "Panna Cotta", 8.40m, ItemImg("panna-cotta", 1), true, now),
        };

            // --------------------------
            // Persist (idempotent)
            // --------------------------
            await using var tx = await db.Database.BeginTransactionAsync(ct);

            // Load existing IDs once (fast, avoids per-row Any())
            var existingCategoryIds = await db.Categories.AsNoTracking().Select(x => x.Id).ToHashSetAsync(ct);
            var existingRestaurantIds = await db.Restaurants.AsNoTracking().Select(x => x.Id).ToHashSetAsync(ct);
            var existingMenuIds = await db.Menus.AsNoTracking().Select(x => x.Id).ToHashSetAsync(ct);
            var existingItemIds = await db.MenuItems.AsNoTracking().Select(x => x.Id).ToHashSetAsync(ct);

            db.Categories.AddRange(categories.Where(x => !existingCategoryIds.Contains(x.Id)));
            db.Restaurants.AddRange(restaurants.Where(x => !existingRestaurantIds.Contains(x.Id)));
            db.Menus.AddRange(menus.Where(x => !existingMenuIds.Contains(x.Id)));
            db.MenuItems.AddRange(items.Where(x => !existingItemIds.Contains(x.Id)));

            await db.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);
        }

        private static Category NewCategory(Guid id, string name, bool isActive, DateTime now) =>
            new()
            {
                Id = id,
                Name = name,
                IsActive = isActive,
                CreatedAt = now,
                CreatedBy = SeedUser,
                UpdatedAt = now,
                UpdatedBy = SeedUser
            };

        private static Restaurant NewRestaurant(
            Guid id,
            string name,
            string description,
            string location,
            string cuisine,
            OpenClosed status,
            string imgUrl,
            string ownerId,
            DateTime now) =>
            new()
            {
                Id = id,
                Name = name,
                Description = description,
                Location = location,
                Cuisine = cuisine,
                Status = status,
                ImgUrl = imgUrl,
                OwnerId = ownerId,
                CreatedAt = now,
                CreatedBy = SeedUser,
                UpdatedAt = now,
                UpdatedBy = SeedUser
            };

        private static Menu NewMenu(
            Guid id,
            Guid restaurantId,
            string name,
            string description,
            MenuType type,
            bool isActive,
            string imgUrl,
            DateTime now) =>
            new()
            {
                Id = id,
                RestaurantId = restaurantId,
                Name = name,
                Description = description,
                Type = type,
                IsActive = isActive,
                ImgUrl = imgUrl,
                CreatedAt = now,
                CreatedBy = SeedUser,
                UpdatedAt = now,
                UpdatedBy = SeedUser
            };

        private static MenuItem NewItem(
            Guid id,
            Guid menuId,
            Guid categoryId,
            string name,
            decimal price,
            string imgUrl,
            bool isActive,
            DateTime now) =>
            new()
            {
                Id = id,
                MenuId = menuId,
                CategoryId = categoryId,
                Name = name,
                Price = price,
                ImgUrl = imgUrl,
                IsActive = isActive,
                CreatedAt = now,
                CreatedBy = SeedUser,
                UpdatedAt = now,
                UpdatedBy = SeedUser
            };
    }
}
