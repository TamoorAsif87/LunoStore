using Identity.Account.Models;

namespace Identity.Data.Seed;

public static class IdentityInitialData
{
    public static IEnumerable<IdentityRole> Roles =>
        new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = "F52A9C17-7B1F-42A5-93C1-1A8BC5DFA000",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "A93D3C34-8A7E-4C9A-B47B-51F3A8A6B001",
                Name = "User",
                NormalizedName = "USER"
            }
        };

    public static IEnumerable<ApplicationUser> Users =>
        new List<ApplicationUser>
        {
            ApplicationUser.Create(
                email: "admin@example.com",
                name: "Admin User",
                address: "123 Admin Lane",
                phone: "1234567890",
                country: "AdminLand",
                city: "AdminCity",
                profileImage: ""
            ),


            ApplicationUser.Create(
                email: "emma.jones@example.com",
                name: "Emma Jones",
                address: "742 Oak Street",
                phone: "5551237890",
                country: "Canada",
                city: "Toronto",
                profileImage: ""
            ),

            ApplicationUser.Create(
                email: "li.chen@example.com",
                name: "Li Chen",
                address: "11 Bamboo Avenue",
                phone: "5559871234",
                country: "China",
                city: "Beijing",
                profileImage: ""
            ),

            ApplicationUser.Create(
                email: "carlos.mendez@example.com",
                name: "Carlos Mendez",
                address: "98 Avenida del Sol",
                phone: "5553214567",
                country: "Mexico",
                city: "Guadalajara",
                profileImage: ""
            ),

            ApplicationUser.Create(
                email: "lena.schmidt@example.com",
                name: "Lena Schmidt",
                address: "32 Berliner Strasse",
                phone: "5554567890",
                country: "Germany",
                city: "Berlin",
                profileImage: ""
            ),

            ApplicationUser.Create(
                email: "raj.patel@example.com",
                name: "Raj Patel",
                address: "14 Lotus Lane",
                phone: "5556540987",
                country: "India",
                city: "Mumbai",
                profileImage: ""
            ),

            ApplicationUser.Create(
                email: "fatima.ahmed@example.com",
                name: "Fatima Ahmed",
                address: "21 Jasmine Road",
                phone: "5557654321",
                country: "Egypt",
                city: "Cairo",
                profileImage: ""
            ),

            ApplicationUser.Create(
                email: "john.smith@example.com",
                name: "John Smith",
                address: "99 Maple Avenue",
                phone: "5551122334",
                country: "USA",
                city: "New York",
                profileImage: ""
            ),

            ApplicationUser.Create(
                email: "hana.yamamoto@example.com",
                name: "Hana Yamamoto",
                address: "7 Sakura Street",
                phone: "5559988776",
                country: "Japan",
                city: "Tokyo",
                profileImage: ""
            )
        };

    public static Dictionary<string, string> UserPasswords => new()
    {
        //admin
        { "admin@example.com", "Admin123!" },

        //users
        { "emma.jones@example.com", "User123!" },
        { "li.chen@example.com", "User123!" },
        { "carlos.mendez@example.com", "User123!" },
        { "lena.schmidt@example.com", "User123!" },
        { "raj.patel@example.com", "User123!" },
        { "fatima.ahmed@example.com", "User123!" },
        { "john.smith@example.com", "User123!" },
        { "hana.yamamoto@example.com", "User123!" }
    };

    public static Dictionary<string, string> UserRoles => new()
    {   
        //admin
        { "admin@example.com", "Admin" },

        //users
         { "emma.jones@example.com", "User" },
         { "li.chen@example.com", "User" },
         { "carlos.mendez@example.com", "User" },
         { "lena.schmidt@example.com", "User" },
         { "raj.patel@example.com", "User" },
         { "fatima.ahmed@example.com", "User" },
         { "john.smith@example.com", "User" },
         { "hana.yamamoto@example.com", "User" }
    };
}
