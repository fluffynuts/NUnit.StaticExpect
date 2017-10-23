using NUnit.Framework;
using PeanutButter.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using PeanutButter.RandomGenerators;
using static PeanutButter.RandomGenerators.RandomValueGen;
using NUnit.Framework.Constraints;
#pragma warning disable 618

namespace NUnit.StaticExpect.Tests
{
    [SuppressMessage("Microsoft.Design", "CS0618:Obsolete")]
    [TestFixture]
    public class TestAssertionHelperCompatibility
    {
        [TestCaseSource(nameof(GetAssertionHelperMethodSigs))]
        public void ShouldImplementMethod_(Signature sig)
        {
            var rewrite = CheckForSpecialCase(sig);

            var usedSig = rewrite ?? sig;

            var member = FindMethod(typeof(Expectations), usedSig);

            Assert.That(member, Is.Not.Null, $"{usedSig.Name} not found on Expectations");
            Assert.That(member.ReturnType.Name,
                Is.EqualTo(usedSig.ReturnType.Name),
                $"{usedSig.Name}: return types don't match");

            // Ignore special case tests if rewritten signature matches
            // (otherwise an unexpected signature has been encountered and will be flagged above).
            if (rewrite != null)
            {
                Assert.Ignore($"Ignored by special case: {sig}");
            }
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
                public static Signature OriginalSignature =>
                    new Signature(
                        nameof(AssertionHelper.Exactly),
                        typeof(ConstraintExpression),
                        new[] { typeof(Int32) }
                    );

                public static Signature RewriteSignature =>
                    new Signature(
                        nameof(AssertionHelper.Exactly),
                        typeof(ItemsConstraintExpression),
                        new[] { typeof(Int32) }
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
                        () => AssertionHelper.Expect(collection, AssertionHelper.Exactly(1).EqualTo(seek))
                    );
                    var ex2 = Assert.Throws<AssertionException>(
                        () => Expectations.Expect(collection, Expectations.Exactly(1).EqualTo(seek))
                    );
                    // Act

                    // Assert
                    Assert.That(ex1.Message, Is.EqualTo(ex1.Message));
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
                            AssertionHelper.Expect(
                                collection,
                                AssertionHelper.Exactly(1).EqualTo(seek)
                            ),
                        Throws.Nothing
                    );

                    Assert.That(() =>
                            Expectations.Expect(
                                collection,
                                Expectations.Exactly(1).EqualTo(seek)
                            ),
                        Throws.Nothing
                    );

                    // Assert
                }
            }

            [TestFixture]
            public class InRange
            {
                public static Signature OriginalSignature =>
                    new Signature(
                        nameof(AssertionHelper.InRange),
                        typeof(RangeConstraint),
                        new[] { typeof(IComparable), typeof(IComparable) }
                    );

                public static Signature RewriteSignature =>
                    new Signature(
                        nameof(AssertionHelper.InRange),
                        typeof(RangeConstraint),
                        new[] { typeof(object), typeof(object) }
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
                        AssertionHelper.Expect(check, (new AssertionHelper()).InRange(min, max)));
                    var ex2 = Assert.Throws<AssertionException>(() =>
                        Expectations.Expect(check, Expectations.InRange(min, max)));
                    // Assert
                    Assert.That(ex1.Message, Is.EqualTo(ex1.Message));
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
                            AssertionHelper.Expect(check, (new AssertionHelper()).InRange(min, max)),
                        Throws.Nothing);
                    Assert.That(() =>
                            Expectations.Expect(check, Expectations.InRange(min, max)),
                        Throws.Nothing);


                    // Assert
                }
            }
        }

        private Signature CheckForSpecialCase(Signature sig)
        {
            var specialCases = typeof(SpecialCases).GetNestedTypes();

            foreach (var specialCase in specialCases)
            {
                var found = specialCase.GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .Where(pi => pi.Name == "OriginalSignature")
                    .Select(pi => pi.GetValue(null) as Signature)
                    .Where(v => v != null)
                    .Any(v => v.Name == sig.Name && v.ParameterTypes.DeepEquals(sig.ParameterTypes));

                if (found)
                {
                    var rewrite = specialCase.GetProperties(BindingFlags.Public | BindingFlags.Static)
                        .Where(pi => pi.Name == "RewriteSignature")
                        .Select(pi => pi.GetValue(null) as Signature)
                        .SingleOrDefault(v => v != null);

                    if (rewrite != null)
                    {
                        return rewrite;
                    }
                    else
                    {
                        Assert.Fail($"Special case: {sig} does not hold a valid rewrite signature");
                    }
                }
            }

            return null;
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
            Assert.That(member, Is.Not.Null);
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