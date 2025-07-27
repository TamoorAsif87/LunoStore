namespace Catalog.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Category> Categories =>
    new List<Category>
    {
        Category.Create(Guid.Parse("C0D7ADED-F411-48E5-B613-E37A44DDE625"), "Home & Kitchen"),
        Category.Create(Guid.Parse("A1B2C3D4-E5F6-4789-ABCD-123456789001"), "Books"),
        Category.Create(Guid.Parse("B2C3D4E5-F6A7-4890-BCDE-234567890102"), "Electronics"),
        Category.Create(Guid.Parse("C3D4E5F6-A7B8-4901-CDEF-345678901203"), "Clothing"),
        Category.Create(Guid.Parse("D4E5F6A7-B8C9-4012-DEFA-456789012304"), "Toys & Games"),
        Category.Create(Guid.Parse("E5F6A7B8-C9D0-4123-EFAB-567890123405"), "Sports"),
        Category.Create(Guid.Parse("F6A7B8C9-D0E1-4234-FABC-678901234506"), "Beauty"),
        Category.Create(Guid.Parse("A7B8C9D0-E1F2-4345-ABCD-789012345607"), "Automotive")
    };

    public static IEnumerable<Product> Products =>
   new List<Product>
   {
         Product.Create
       (
            Guid.Parse("8D0A5AC1-5C5B-4E95-B738-1DF2A9E5A001"),
            "Non-stick Frying Pan",
            "Durable and easy to clean pan for everyday cooking.",
            29.99m,
            120,
            new List<string>(), 
            new() { "Black", "Red" },
            Guid.Parse("C0D7ADED-F411-48E5-B613-E37A44DDE625") 
        ),

    Product.Create
       (
            Guid.Parse("6A9C7263-1B11-4E94-BE8F-AD2F9DC8C002"),
            "Wireless Bluetooth Headphones",
            "Noise-canceling headphones with long battery life.",
            89.99m,
            75,
            new List<string>(), 
            new() { "Black", "Blue" },
            Guid.Parse("B2C3D4E5-F6A7-4890-BCDE-234567890102") 
        ),

    Product.Create
       (
            Guid.Parse("F48B71B7-210C-49DD-89B9-451C5B63C003"),
            "Men's Cotton T-Shirt",
            "Soft, breathable t-shirt in various sizes.",
            15.50m,
            200,
            new List<string>(),
            new() { "White", "Navy" },
            Guid.Parse("C3D4E5F6-A7B8-4901-CDEF-345678901203") 
        ),

    Product.Create
       (
            Guid.Parse("143C7D5E-3A40-4FB8-A902-C54A4583D004"),
            "The Great Gatsby",
            "Classic novel by F. Scott Fitzgerald.",
            9.99m,
            150,
           new List<string>(),
            new List<string>(), 
            Guid.Parse("A1B2C3D4-E5F6-4789-ABCD-123456789001") 
        ),

    Product.Create
       (
            Guid.Parse("A672D8D5-F0AB-448E-9B0E-15C859D6D005"),
            "Basketball",
            "Official size and weight for indoor/outdoor play.",
            25.00m,
            100,
            new List<string>(),
            new() { "Orange" },
            Guid.Parse("E5F6A7B8-C9D0-4123-EFAB-567890123405") 
        ),

    Product.Create
       (
            Guid.Parse("CC6F8B30-2F9C-4C6E-B23B-203DC4DFB006"),
            "Toy Building Blocks Set",
            "Creative and educational toy for kids.",
            39.99m,
            60,
            new List<string>(), 
            new() { "Multi-color" },
            Guid.Parse("D4E5F6A7-B8C9-4012-DEFA-456789012304") 
        ),

    Product.Create
       (
            Guid.Parse("B1D84BE6-B37A-4A23-BAAD-66D177C0C007"),
            "Moisturizing Face Cream",
            "Hydrates and nourishes your skin.",
            18.75m,
            90,
            new List<string>(),
            new() { "White" },
            Guid.Parse("F6A7B8C9-D0E1-4234-FABC-678901234506") 
        ),

    Product.Create
       (
            Guid.Parse("540A2266-9EF6-44A7-B31F-DAECADF1D008"),
            "Car Phone Mount",
            "Universal phone holder for all car dashboards.",
            11.99m,
            110,
            new List<string>(),
            new() { "Black" },
            Guid.Parse("A7B8C9D0-E1F2-4345-ABCD-789012345607") 
        )

   };
}