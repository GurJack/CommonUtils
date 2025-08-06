using System;

namespace CommonUtils.Extensions
{
    /// <summary>
    /// Type extensions.
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// Determinates is the type nullable.
        /// </summary>
        /// <param name="theType"></param>
        /// <returns></returns>
        public static bool IsNullableType(this Type theType)
        {
            return (theType.IsGenericType
                && theType.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Gets the real type: if type is nullable it will be underlying type.
        /// </summary>
        public static Type GetRealType(this Type type)
        {
            if (type == null)
            {
                return null;
            }

            var isNullableType = type.IsNullableType();

            return isNullableType
                ? Nullable.GetUnderlyingType(type)
                : type;
        }

        /// <summary>
        /// Determines whether the current System.Type derives from the specified System.Type or equal to it.
        /// </summary>
        public static bool IsSubclassOrEqual(this Type potentialDescendant, Type potentialBase)
        {
            return potentialDescendant == potentialBase ||
                   potentialDescendant.IsSubclassOf(potentialBase);
        }

        /// <summary>
        /// Determines whether the current generic System.Type derives from the specified generic System.Type or equal to it.
        /// </summary>
        public static bool IsSubclassOrEqualFromGenericType(this Type genericPotentialDescendant, Type genericPotentialBase)
        {
            if (genericPotentialDescendant == null)
            {
                return false;
            }

            if (genericPotentialDescendant == typeof(object))
            {
                return false;
            }

            var potentialDescendant = genericPotentialDescendant.IsGenericType ? genericPotentialDescendant.GetGenericTypeDefinition() : genericPotentialDescendant;
            var potentialBase = genericPotentialBase.IsGenericType ? genericPotentialBase.GetGenericTypeDefinition() : genericPotentialBase;

            if (potentialDescendant == potentialBase)
            {
                return true;
            }

            return IsSubclassOrEqualFromGenericType(potentialDescendant.BaseType, potentialBase);
        }
    }
}