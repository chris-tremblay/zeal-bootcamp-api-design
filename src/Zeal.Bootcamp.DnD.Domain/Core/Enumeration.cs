using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Zeal.Bootcamp.DnD.Domain.Core;

/// <summary>
///   A base Enumeration type used for Generic Type Enforcement.
/// </summary>
public abstract class Enumeration
{
    public object Value { get; protected set; }

    /// <summary>
    ///   Parses an <typeparamref name="TEnumeration"/> object from its <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The Display Name of the <typeparamref name="TEnumeration"/>.</param>
    /// <typeparam name="TEnumeration">The type of the enumeration.</typeparam>
    /// <returns>Return an instance of <typeparamref name="TEnumeration"/>.</returns>
    public static TEnumeration Parse<TEnumeration>(string value)
        where TEnumeration : Enumeration
        => Parse<TEnumeration>(value, "value", item =>
        {
            return item.Value.ToString()!.Equals(value, StringComparison.CurrentCultureIgnoreCase);
        });

    protected static TEnumeration[] GetEnumerations<TEnumeration>()
        where TEnumeration : Enumeration
    {
        Type enumerationType = typeof(TEnumeration);

        return enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(info => enumerationType.IsAssignableFrom(info.FieldType))
            .Select(info => info.GetValue(null))
            .Cast<TEnumeration>()
            .ToArray();
    }

    protected static TEnumeration Parse<TEnumeration>(object value, string description, Func<TEnumeration, bool> predicate)
        where TEnumeration : Enumeration
    {
        if (!TryParse(predicate, out TEnumeration? result))
        {
            string message = $"'{value}' is not a valid {description} in {typeof(TEnumeration)}";
            throw new ArgumentException(message, nameof(value));
        }

        return result!;
    }

    protected static bool TryParse<TEnumeration>(Func<TEnumeration, bool> predicate, out TEnumeration? result)
        where TEnumeration : Enumeration
    {
        result = GetEnumerations<TEnumeration>().FirstOrDefault(predicate);

        return result != null;
    }
}

/// <summary>
///   Represents a base enumeration type. Intended to replace the <see cref="Enum"/>.
/// </summary>
/// <typeparam name="TEnumeration">The type of the enumeration.</typeparam>
/// <typeparam name="TValue">The value of the enumeration.</typeparam>
[Serializable]
[DebuggerDisplay("{Value}")]
[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Generic Implemtentation")]
public abstract class Enumeration<TEnumeration, TValue>
    : Enumeration, IComparable<TEnumeration>, IEquatable<TEnumeration>
        where TEnumeration : Enumeration<TEnumeration, TValue>
        where TValue : IComparable
{
    private static readonly TEnumeration[] Enumerations = GetEnumerations<TEnumeration>();

    /// <summary>
    ///   Initializes a new instance of the <see cref="Enumeration{TEnumeration, TValue}"/> class.
    /// </summary>
    /// <param name="value">The enum value.</param>
    protected Enumeration(TValue value)
    {
        Value = value;
        base.Value = value;
    }

    /// <summary>
    ///   Gets the value of the enum.
    /// </summary>
    public new TValue Value { get; }

    /// <summary>
    ///   Gets the <typeparamref name="TEnumeration"/>.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>Returns an instance of <typeparamref name="TEnumeration"/>.</returns>
    public static TEnumeration FromValue(TValue value)
        => Parse<TEnumeration>(value, "value", item => IsEqual(value, item));

    /// <summary>
    ///   Gets all possible enum values.
    /// </summary>
    /// <returns>Returns a list of <typeparamref name="TEnumeration"/> objects.</returns>
    public static TEnumeration[] GetAll()
        => Enumerations;

    /// <summary>
    ///   Parses an <typeparamref name="TEnumeration"/> object from its <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The Display Name of the <typeparamref name="TEnumeration"/>.</param>
    /// <returns>Return an instance of <typeparamref name="TEnumeration"/>.</returns>
    public static TEnumeration Parse(string value)
        => Parse<TEnumeration>(value, "value", item => IsEqual(value, item));

    /// <summary>
    ///   Tries to parse a value, and returns a <see cref="bool"/> indicating if the parse was successful.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="result">The result of the parse operation.</param>
    /// <returns>
    ///   Returns a <see cref="bool"/> indicating whether the <paramref name="value"/> was successfully parsed.
    /// </returns>
    public static bool TryParse(TValue value, out TEnumeration? result)
        => TryParse(x => x.Value.Equals(value), out result);

    /// <summary>
    ///   Tries to parse a value, and returns a <see cref="bool"/> indicating if the parse was successful.
    /// </summary>
    /// <param name="valueOrDisplayName">The display name to parse.</param>
    /// <param name="result">The result of the parse operation.</param>
    /// <returns>
    ///   Returns a <see cref="bool"/> indicating whether the <paramref name="valueOrDisplayName"/> was successfully parsed.
    /// </returns>
    public static bool TryParse(string valueOrDisplayName, out TEnumeration? result)
         => TryParse(x => x.Value.ToString()!.Equals(valueOrDisplayName, StringComparison.CurrentCultureIgnoreCase), out result);

    /// <inheritdoc/>
    public int CompareTo(TEnumeration? other)
        => Value.CompareTo(other != null ? other.Value : null);

    /// <inheritdoc/>
    public override bool Equals(object? obj)
        => Equals(obj as TEnumeration);

    /// <inheritdoc/>
    public bool Equals(TEnumeration? other)
        => other != null && Value.Equals(other.Value);

    /// <inheritdoc/>
    public override int GetHashCode()
        => Value.GetHashCode();

    /// <inheritdoc/>
    public override sealed string ToString()
        => Value.ToString()!;

    private static bool IsEqual(object value, Enumeration item)
    {
        if (value.GetType() == typeof(string))
        {
            return item.Value.GetType().IsEnum
                ? string.Equals(((Enum)item.Value).ToString(), value as string, StringComparison.OrdinalIgnoreCase)
                : string.Equals((string)item.Value, value as string, StringComparison.OrdinalIgnoreCase);
        }

        return item.Value.Equals(value);
    }
}