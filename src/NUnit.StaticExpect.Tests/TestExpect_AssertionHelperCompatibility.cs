using NUnit.Framework;
using PeanutButter.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.StaticExpect.Tests
{
    [SuppressMessage("Microsoft.Design", "CS0618:Obsolete")]
    [TestFixture]
    public class TestExpect_AssertionHelperCompatibility
    {
        [TestCaseSource(nameof(GetAssertionHelperMethodSigs))]
        public void ShouldImplementMethod_(Signature sig)
        {
            var member = FindMethod(typeof(Expectations), sig);

            Assert.That(member, Is.Not.Null, $"{sig.Name} not found on Expectations");
            Assert.That(member.ReturnType.Name, Is.EqualTo(sig.ReturnType.Name), $"{sig.Name}: return types don't match");
        }

        [TestCaseSource(nameof(GetAssertionHelperPropertySigs))]
        public void ShouldImplementProperty_(Signature sig)
        {
            var member = FindProperty(typeof(Expectations), sig);

            Assert.That(member, Is.Not.Null, $"{sig.Name} not found on Expectations");
        }

        private MethodInfo FindMethod(Type type, Signature sig)
        {
            // As a method
            MethodInfo member = type.GetMethodWithGenerics(
                                        sig.Name,
                                        sig.ParameterTypes.ToArray(),
                                        BindingFlags.Public | BindingFlags.Static);
            if (member != null)
                return member;

            // Could also be a static property pass-through to a Func<>
            var propinfo = type.GetProperty(sig.Name)?.GetValue(null);

            // GetPropertyValue throws if member not found - we just want to return null
            try { member = (MethodInfo)propinfo?.GetPropertyValue("Method"); }
            catch (MemberNotFoundException) { return null; }

            // Check parameter types match (compared on names)
            Assert.That(member.GetParameters().Select(mi => mi.ParameterType.Name),
                            Is.EqualTo(sig.ParameterTypes.Select(mi => mi.Name)),
                            $"{sig.Name} implemented as pass-through property, but parameter types don't match.");
            return member;
        }

        private PropertyInfo FindProperty(Type type, Signature sig)
            => type.GetProperty(
                            sig.Name,
                            BindingFlags.Public | BindingFlags.Static,
                            null,
                            sig.ReturnType,
                            sig.ParameterTypes.ToArray(),
                            null);

        private static IEnumerable<Signature> GetAssertionHelperMethodSigs()
            => GetMethodSigs(typeof(AssertionHelper));

        private static IEnumerable<Signature> GetAssertionHelperPropertySigs()
            => GetPropertySigs(typeof(AssertionHelper));

        private static IEnumerable<Signature> GetMethodSigs(Type type)
        {
            // Get MethodInfos, filter and project into signatures
            var sigs = type.GetMethods(
                                  BindingFlags.Public
                                | BindingFlags.DeclaredOnly
                                | BindingFlags.Static 
                                | BindingFlags.Instance)
                            .Where(mi => !mi.Name.StartsWith("get_"))
                            .Where(mi => !mi.Name.StartsWith("set_"))
                            .Select(o => new Signature(o.Name, o.ReturnType, o.GetParameters().Select(pi => pi.ParameterType)));

            return sigs;
        }

        private static IEnumerable<Signature> GetPropertySigs(Type type)
        {
            var properties = type.GetProperties(
                                    BindingFlags.Public
                                    | BindingFlags.DeclaredOnly
                                    | BindingFlags.Static
                                    | BindingFlags.Instance)
                                .Select(pi => new Signature(
                                                    pi.Name,
                                                    pi.PropertyType,
                                                    pi.GetIndexParameters().Select(pr => pr.ParameterType)));

            return properties;
        }

        public struct Signature
        {
            public string Name;
            public Type ReturnType;
            public IEnumerable<Type> ParameterTypes;

            public Signature(string name, Type returnType, IEnumerable<Type> parameterTypes = null)
            {
                Name = name;
                ReturnType = returnType;
                ParameterTypes = parameterTypes;
            }

            // Used in automatic test naming - parameters ensure overrides register
            public override string ToString()
                => $"{Name}({String.Join(", ", ParameterTypes.Select(p => p.Name))})";
        }
    }

}
