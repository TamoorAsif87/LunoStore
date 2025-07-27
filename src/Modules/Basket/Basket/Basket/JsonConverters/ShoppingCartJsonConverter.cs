using System.Reflection;

namespace Basket.Basket.JsonConverters;

public class ShoppingCartJsonConverter : JsonConverter<ShoppingCart>
{
    public override ShoppingCart? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;

        var id = root.TryGetProperty("Id", out var idProp) && idProp.ValueKind == JsonValueKind.String
            ? idProp.GetGuid()
            : Guid.NewGuid();

        var userName = root.TryGetProperty("UserName", out var userProp) && userProp.ValueKind == JsonValueKind.String
            ? userProp.GetString()!
            : throw new JsonException("Missing or invalid 'UserName'.");

        var cart = ShoppingCart.Create(id, userName);

        var itemsElement = root.GetProperty("Items");



        var items = new List<ShoppingCartItem>();
        foreach (var itemElement in itemsElement.EnumerateArray())
        {
            var item = itemElement.Deserialize<ShoppingCartItem>(options);
            if (item != null)
            {
                items.Add(item);
            }
        }

        if (items != null)
        {
            var itemsField = typeof(ShoppingCart).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
            itemsField?.SetValue(cart, items);

        }

        return cart;
    }

    public override void Write(Utf8JsonWriter writer, ShoppingCart value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("Id", value.Id.ToString());
        writer.WriteString("UserName", value.UserName);
        writer.WritePropertyName("Items");
        JsonSerializer.Serialize(writer, value.Items, options);
        writer.WriteNumber("TotalPrice", value.TotalPrice);

        writer.WriteEndObject();
    }
}
