namespace Ordering.Data.Seed;

public static class OrderInitialData
{
    public static Order[] Orders =>
    [
        CreateOrder("8932abea-78bf-45fd-9237-e79f2d83588f", "143c7d5e-3a40-4fb8-a902-c54a4583d004", 9.99m, 2, "The Great Gatsby", ["Red", "Black"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753561633/p1hkc7cdr9y7qn5pzb0e.webp"]),
        CreateOrder("ca208076-5155-483a-9eed-9dd3a971f2d5", "540a2266-9ef6-44a7-b31f-daecadf1d008", 11.99m, 1, "Car Phone Mount", ["Black"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753561974/fma2p8uiffpt5cksirsm.webp"]),
        CreateOrder("4670b1ab-6d70-4813-acfa-18622f8311ff", "6a9c7263-1b11-4e94-be8f-ad2f9dc8c002", 89.99m, 1, "Wireless Bluetooth Headphones", ["Black", "Blue"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753564928/nutnbay4jgqtvzybz9kf.webp"]),
        CreateOrder("c18cccf6-950c-45c1-ba51-3639599dce25", "8d0a5ac1-5c5b-4e95-b738-1df2a9e5a001", 29.99m, 2, "Non-stick Frying Pan", ["Black", "Red"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753564947/xj5rvm8vd0tqqaihzxms.jpg"]),
        CreateOrder("8f8ce960-8e13-4d11-8d6f-76ac32f19a53", "f48b71b7-210c-49dd-89b9-451c5b63c003", 15.50m, 3, "Men's Cotton T-Shirt", ["White", "Navy"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753564966/k4idw4j5ybfk3uchewgg.webp"]),
        CreateOrder("7dacf93b-c50b-457a-9893-e6fc187adf27", "cc6f8b30-2f9c-4c6e-b23b-203dc4dfb006", 39.99m, 1, "Toy Building Blocks Set", ["Multi-color"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753564986/mw9tc9madrak1obiahjx.webp"]),
        CreateOrder("900731b5-f803-4cbd-ad9f-17c18c4097d6", "a672d8d5-f0ab-448e-9b0e-15c859d6d005", 25.00m, 1, "Basketball", ["Orange"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753565008/d2cfsbyi4grvgraxjxlk.png"]),
        CreateOrder("0a2f6c38-4565-433f-b187-63b6836ac4df", "b1d84be6-b37a-4a23-baad-66d177c0c007", 18.75m, 2, "Moisturizing Face Cream", ["White"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753565029/chn4w2qmvpgmxrv2bym8.jpg"]),
        CreateOrder("35b8b805-88f1-47dc-aa0b-d3dd0010710c", "6a9c7263-1b11-4e94-be8f-ad2f9dc8c002", 89.99m, 1, "Wireless Bluetooth Headphones", ["Blue"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753564930/are1avdmj7tcju3bso0u.jpg"]),
        CreateOrder("8932abea-78bf-45fd-9237-e79f2d83588f", "cc6f8b30-2f9c-4c6e-b23b-203dc4dfb006", 39.99m, 1, "Toy Building Blocks Set", ["Multi-color"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753564988/umtaqph7rozh1sdc12oi.webp"]),
        CreateOrder("ca208076-5155-483a-9eed-9dd3a971f2d5", "f48b71b7-210c-49dd-89b9-451c5b63c003", 15.50m, 1, "Men's Cotton T-Shirt", ["White"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753564968/uzklvplurzgxfycwdwxd.webp"]),
        CreateOrder("4670b1ab-6d70-4813-acfa-18622f8311ff", "143c7d5e-3a40-4fb8-a902-c54a4583d004", 9.99m, 1, "The Great Gatsby", ["Red"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753561635/gwgppir16blzqmhvtdmb.webp"]),
        CreateOrder("c18cccf6-950c-45c1-ba51-3639599dce25", "540a2266-9ef6-44a7-b31f-daecadf1d008", 11.99m, 2, "Car Phone Mount", ["Black"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753561975/abatmkuyvgjwtg0tdcvl.jpg"]),
        CreateOrder("8f8ce960-8e13-4d11-8d6f-76ac32f19a53", "b1d84be6-b37a-4a23-baad-66d177c0c007", 18.75m, 1, "Moisturizing Face Cream", ["White"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753565030/etjgxxzvipdzglqs4xki.webp"]),
        CreateOrder("7dacf93b-c50b-457a-9893-e6fc187adf27", "8d0a5ac1-5c5b-4e95-b738-1df2a9e5a001", 29.99m, 1, "Non-stick Frying Pan", ["Black"], ["https://res.cloudinary.com/dnumrrwxc/image/upload/v1753564948/fxgpsfvbihkojzv0ey8l.jpg"])
    ];

    private static Order CreateOrder(string customerId, string productId, decimal price, int quantity, string productName, List<string> colors, List<string> images)
    {
        var orderId = Guid.NewGuid();
        var address = Address.Of("John", "Doe", "john.doe@example.com", "123 Elm Street", "USA", "New York", "NY", "10001");
        var payment = Payment.Of("John Doe", "4111111111111111", new DateOnly(2026, 12, 31), "123");
        var order = Order.Create(orderId, customerId, address, address, payment);

        var orderItem = new OrderItem(
            Guid.Parse(productId),
            orderId,
            price,
            quantity,
            images,
            colors,
            productName
        );

        order.AddOrderItem(orderItem);
        return order;
    }
}
