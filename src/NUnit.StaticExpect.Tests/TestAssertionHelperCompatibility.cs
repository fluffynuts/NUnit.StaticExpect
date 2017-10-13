using NUnit.Framework;
using PeanutButter.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using PeanutButter.RandomGenerators;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NUnit.StaticExpect.Tests
{
    [SuppressMessage("Microsoft.Design", "CS0618:Obsolete")]
    [TestFixture]
    public class TestAssertionHelperCompatibility
    {
        [TestCaseSource(nameof(GetAssertionHelperMethodSigs))]
        public void ShouldImplementMethod_(Signature sig)
        {
            CheckForSpecialCase(sig);

            var member = FindMethod(typeof(Expectations), sig);

            Assert.That(member, Is.Not.Null, $"{sig.Name} not found on Expectations");
            Assert.That(member.ReturnType.Name,
                Is.EqualTo(sig.ReturnType.Name),
                $"{sig.Name}: return types don't match");
        }

        [TestCaseSource(nameof(GetAssertionHelperPropertySigs))]
        public void ShouldImplementProperty_(Signature sig)
        {
            var member = FindProperty(typeof(Expectations), sig);

            Assert.That(member, Is.Not.Null, $"{sig.Name} not found on Expectations");
        }

        [TestFixture]
        public class SpecialCases
        {
            [TestFixture]
            public class Exactly
            {
                public static Signature Signature =>
                    new Signature(
                        nameof(AssertionHelper.Exactly),
                        typeof(Int32),
                        new[] {typeof(Int32)}
                    );

                [Test]
                public void ShouldFailSimilarly()
                {
                    // Arrange
                    var collection = GetRandomCollection<string>(4, 7)
                        .Distinct()
                        .ToArray();
                    var seek = GetAnother<string>(collection);

                    // Pre-Assert
                    var ex1 = Assert.Throws<AssertionException>(
                        () => Assert.That(collection, Has.Exactly(1).EqualTo(seek))
                    );
                    var ex2 = Assert.Throws<AssertionException>(
                        () => AssertionHelper.Expect(collection, AssertionHelper.Exactly(1).EqualTo(seek))
                    );
                    // Act

                    // Assert
                    Assert.That(ex1.Message, Is.EqualTo(ex2.Message));
                }

                [Test]
                public void ShouldPassOnTheSameLogic()
                {
                    // Arrange
                    var collection = GetRandomCollection<string>(4, 7)
                        .Distinct()
                        .ToArray();
                    var seek = collection.Randomize().First();

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                            Assert.That(collection, Has.Exactly(1).EqualTo(seek)),
                        Throws.Nothing
                    );
                    Assert.That(() =>
                            AssertionHelper.Expect(
                                collection,
                                AssertionHelper.Exactly(1).EqualTo(seek)
                            ),
                        Throws.Nothing
                    );

                    // Assert
                }
            }

            [TestFixture]
            public class InRange
            {
                public static Signature Signature =>
                    new Signature(
                        nameof(AssertionHelper.InRange),
                        typeof(void),
                        new[] {typeof(IComparable), typeof(IComparable)}
                    );

                [Test]
                public void ShouldFailSimilarly()
                {
                    // Arrange
                    var min = GetRandomInt(1, 5);
                    var max = GetRandomInt(10, 15);
                    var check = GetRandomInt(16, 21);

                    // Pre-Assert

                    // Act
                    var ex1 = Assert.Throws<AssertionException>(() =>
                        Assert.That(check, Is.InRange(min, max)));
                    var ex2 = Assert.Throws<AssertionException>(() =>
                        AssertionHelper.Expect(check, (new AssertionHelper()).InRange(min, max)));

                    // Assert
                    Assert.That(ex1.Message, Is.EqualTo(ex2.Message));
                }

                [Test]
                public void ShouldPassOnTheSameLogic()
                {
                    // Arrange
                    var min = GetRandomInt(0, 5);
                    var max = GetRandomInt(10, 15);
                    var check = GetRandomInt(6, 9);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                            Assert.That(check, Is.InRange(min, max)),
                        Throws.Nothing
                    );
                    Assert.That(() =>
                            AssertionHelper.Expect(check, (new AssertionHelper()).InRange(min, max)),
                        Throws.Nothing);


                    // Assert
                }
            }
        }

        private void CheckForSpecialCase(Signature sig)
        {
            var ignored = typeof(SpecialCases).GetNestedTypes()
                .Select(t => t.GetProperties(BindingFlags.Public | BindingFlags.Static))
                .SelectMany(propInfos => propInfos)
                .Select(pi => pi.GetValue(null) as Signature)
                .Where(v => v != null)
                .Any(v => v.Name == sig.Name && v.ParameterTypes.DeepEquals(sig.ParameterTypes));
            if (ignored)
            {
                Assert.Ignore($"Ignored by special case: {sig}");
            }
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
            try
            {
                member = (MethodInfo) propinfo?.GetPropertyValue("Method");
            }
            catch (MemberNotFoundException)
            {
                return null;
            }

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
                    BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Instance)
                .Where(mi => !mi.Name.StartsWith("get_"))
                .Where(mi => !mi.Name.StartsWith("set_"))
                .Select(o => new Signature(
                    o.Name,
                    o.ReturnType,
                    o.GetParameters().Select(pi => pi.ParameterType).ToArray()
                ));

            return sigs;
        }

        private static IEnumerable<Signature> GetPropertySigs(Type type)
        {
            var properties = type.GetProperties(
                    BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Instance)
                .Select(pi => new Signature(
                    pi.Name,
                    pi.PropertyType,
                    pi.GetIndexParameters().Select(pr => pr.ParameterType).ToArray()
                ));

            return properties;
        }

        public class Signature
        {
            public string Name { get; }
            public Type ReturnType { get; }
            public Type[] ParameterTypes { get; }

            public Signature(string name, Type returnType, Type[] parameterTypes = null)
            {
                Name = name;
                ReturnType = returnType;
                ParameterTypes = parameterTypes;
            }

            // Used in automatic test naming - parameters ensure overrides register
            public override string ToString()
                => $"{nameof(AssertionHelper)}.{Name}({String.Join(", ", ParameterTypes.Select(p => p.Name))})";
        }
    }
}