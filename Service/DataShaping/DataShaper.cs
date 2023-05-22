using System.Reflection;
using Contracts;
using Entities.Models;

namespace Service.DataShaping;

public class DataShaper<T> : IDataShaper<T>
{
    public DataShaper()
    {
        Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
    }

    public PropertyInfo[] Properties { get; set; }

    public IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string fieldsString)
    {
        var requiredProperties = GetRequiredProperties(fieldsString);
        return FetchData(entities, requiredProperties);
    }

    public ShapedEntity ShapeData(T entity, string fieldsString)
    {
        var requiredProperties = GetRequiredProperties(fieldsString);
        return FetchDataForEntity(entity, requiredProperties);
    }

    private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
    {
        if (string.IsNullOrWhiteSpace(fieldsString))
            return Properties.ToList(); // return all properties if fields are not specified

        var requiredProperties = new List<PropertyInfo>();
        var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var field in fields)
        {
            var property = Properties.FirstOrDefault(pi =>
                pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));

            if (property == null)
                continue;

            requiredProperties.Add(property);
        }

        return requiredProperties;
    }

    // extract the values from these required properties from the entity and put them into an ExpandoObject
    private ShapedEntity FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
    {
        var shapedObject = new ShapedEntity();

        foreach (var property in requiredProperties)
        {
            var objectPropertyValue = property.GetValue(entity);
            shapedObject.Entity.TryAdd(property.Name, objectPropertyValue);
        }

        var objectProperty = entity.GetType().GetProperty("Id");
        shapedObject.Id = (Guid)objectProperty.GetValue(entity);

        return shapedObject;
    }

    private IEnumerable<ShapedEntity> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
    {
        var shapedData = new List<ShapedEntity>();

        foreach (var entity in entities)
        {
            var shapedObject = FetchDataForEntity(entity, requiredProperties);
            shapedData.Add(shapedObject);
        }

        return shapedData;
    }
}