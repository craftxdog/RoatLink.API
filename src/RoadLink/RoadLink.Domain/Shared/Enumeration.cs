using System.Reflection;

namespace RoadLink.Domain.Shared;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>> where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumeration();
    public int Id { get; protected init; }
    public string Name { get; protected init; }

    public Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static TEnum? FromValue(int id)
    {
        return Enumerations.TryGetValue(id, out TEnum? enumeration) ? enumeration : default;
    }

    public static TEnum? FromName(string name)
    {
        return Enumerations.Values.SingleOrDefault(x => x?.Name == name);
    }

    public static List<TEnum> GetValues()
    {
        return Enumerations.Values.ToList();
    }
    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null) return false;
        return GetType() == other.GetType() && Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration<TEnum> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }

    public static Dictionary<int, TEnum> CreateEnumeration()
    {
        var enumerationType = typeof(TEnum);
        var fieldsForType = enumerationType.GetFields(
            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy 
        ).Where(info => enumerationType.IsAssignableFrom(info.FieldType)).Select(info =>(TEnum) info.GetValue(default)! );
        return fieldsForType.ToDictionary(x => x.Id, x => x);
    }
}