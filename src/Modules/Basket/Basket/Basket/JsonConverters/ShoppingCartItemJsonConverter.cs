namespace Basket.Basket.JsonConverters;

public class ShoppingCartItemJsonConverter : JsonConverter<ShoppingCartItem>
{
    public override ShoppingCartItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var id = root.GetProperty("Id").GetGuid();
        var productId = root.GetProperty("ProductId").GetGuid();
        var shoppingCartId = root.GetProperty("ShoppingCartId").GetGuid();
        var quantity = root.GetProperty("Quantity").GetInt32();
        var price = root.GetProperty("Price").GetDecimal();
        var productName = root.GetProperty("ProductName").GetString()!;

        // Deserialize lists
        var productColors = root.GetProperty("ProductColors").EnumerateArray()
            .Select(e => e.GetString()!)
            .ToList();

        var productImages = root.GetProperty("ProductImages").EnumerateArray()
            .Select(e => e.GetString()!)
            .ToList();

        

        return new ShoppingCartItem(
            id,
            productId,
            shoppingCartId,
            quantity,
            price,
            productName,
            productColors,
            productImages
        );
    }

    public override void Write(Utf8JsonWriter writer, ShoppingCartItem value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("Id", value.Id.ToString());
        writer.WriteString("ProductId", value.ProductId.ToString());
        writer.WriteString("ShoppingCartId", value.ShoppingCartId.ToString());
        writer.WriteNumber("Quantity", value.Quantity);
        writer.WriteNumber("Price", value.Price);
        writer.WriteString("ProductName", value.ProductName);

        // Serialize ProductColors as JSON array
        writer.WritePropertyName("ProductColors");
        writer.WriteStartArray();
        foreach (var color in value.ProductColors)
        {
            writer.WriteStringValue(color);
        }
        writer.WriteEndArray();

        // Serialize ProductImages as JSON array
        writer.WritePropertyName("ProductImages");
        writer.WriteStartArray();
        foreach (var image in value.ProductImages)
        {
            writer.WriteStringValue(image);
        }
        writer.WriteEndArray();


        writer.WriteEndObject();
    }
}
