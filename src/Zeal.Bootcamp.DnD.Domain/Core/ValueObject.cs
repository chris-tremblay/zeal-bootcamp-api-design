namespace Zeal.Bootcamp.DnD.Domain.Core;

/// <summary>
///   An object identified by its attributes, not by a unique identity.
/// </summary>
/// <typeparam name="T">The type of the <see cref="ValueObject{T}"/>.</typeparam>
public abstract class ValueObject<T>
    where T : ValueObject<T>
{
    /// <summary>
    ///   Determines if one <see cref="ValueObject{T}"/> is not equal to another instance of a <see cref="ValueObject{T}"/>.
    /// </summary>
    /// <param name="a">Left side.</param>
    /// <param name="b">Right side.</param>
    /// <returns><see cref="bool"/>.</returns>
    public static bool operator !=(ValueObject<T>? a, ValueObject<T>? b)
        => !(a == b);

    /// <summary>
    ///   Determines if one <see cref="ValueObject{T}"/> equals another instance of a <see cref="ValueObject{T}"/>.
    /// </summary>
    /// <param name="a">Left Side.</param>
    /// <param name="b">Right Side.</param>
    /// <returns><see cref="bool"/>.</returns>
    public static bool operator ==(ValueObject<T>? a, ValueObject<T>? b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    /// <summary>
    ///   Determines the current object equals another instance of a <see cref="ValueObject{T}"/>.
    /// </summary>
    /// <param name="obj">The object to which to compare the current object.</param>
    /// <returns><see cref="bool"/>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not T valueObject)
            return false;

        return EqualsCore(valueObject);
    }

    /// <summary>
    ///   Gets a hash code.
    /// </summary>
    /// <returns>A Hash Code.</returns>
    public override int GetHashCode()
        => GetHashCodeCore();

    /// <summary>
    ///   The implementation of the Equals method for the derived type.
    /// </summary>
    /// <param name="other">The object that is being compared to the current object.</param>
    /// <returns>A <see cref="bool"/> indicating if the objects are equal.</returns>
    protected abstract bool EqualsCore(T other);

    /// <summary>
    ///   The implementation for the GetHashCode() method for the derived type.
    /// </summary>
    /// <returns>A Hash Code.</returns>
    protected abstract int GetHashCodeCore();
}