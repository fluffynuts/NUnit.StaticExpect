using System;
using System.Linq;
using System.Reflection;

namespace NUnit.StaticExpect.Tests
{
    public static class Extensions
    {
        /// <summary>
        /// Searches for methods on the current type with the specified name and parameters.
        /// This implementation uses <see cref="Similar(Type, Type)"/> to determine equality,
        /// which copes better with generics than the library GetMethod() methods does.
        /// </summary>
        /// <param name="type">The type to search.</param>
        /// <param name="name">The name of the method to search for.</param>
        /// <param name="parameters">The parameters in the method signature.</param>
        /// <param name="flags">A <see cref="BindingFlags"/> array with which to filter the search.</param>
        /// <returns></returns>
        public static MethodInfo GetMethodWithGenerics(
                                    this Type type,
                                    string name, Type[] parameters,
                                    BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
        {
            var methods = type.GetMethods(flags);

            foreach (var method in methods)
            {
                var parmeterTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();

                if (method.Name == name && parmeterTypes.Count() == parameters.Length)
                {
                    bool match = true;

                    for (int i = 0; i < parameters.Length; i++)
                        match &= parmeterTypes[i].Similar(parameters[i]);

                    if (match)
                        return method;
                }
            }

            return null;
        }

        /// <summary>
        /// Compares a type to the current one to see if they are "similar".
        /// "Similarity" is based on the equality operator but compares on
        /// Type.GenericTypeDefinition if Type.IsGenericType is true and
        /// generic parameter positions if they are both generic parameters.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <param name="type">The type.</param>
        /// <returns>True if types are similar, false otherwise</returns>
        public static bool Similar(this Type reference, Type type)
        {
            if (reference.IsGenericParameter && type.IsGenericParameter)
            {
                return reference.GenericParameterPosition == type.GenericParameterPosition;
            }

            return ComparableType(reference) == ComparableType(type);

            Type ComparableType(Type cType)
                => cType.IsGenericType ? cType.GetGenericTypeDefinition() : cType;
        }

    }
}
