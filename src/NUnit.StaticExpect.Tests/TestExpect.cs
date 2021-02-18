using System;
using System.Reflection;
using NUnit.Framework;
using static NUnit.StaticExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;
using System.Linq;
using System.Text.RegularExpressions;
using PeanutButter.Utils;

namespace NUnit.StaticExpect.Tests
{
    public class TestExpect
    {
        [TestFixture]
        public class StaticImportOfExpect
        {
            [Test]
            public void WithBoolCondition_Message_Args()
            {
                // Arrange
                var msg = MessageWithArg.CreateRandom();

                // Pre-Assert

                // Act
                ShouldThrow(
                    () => Expect(false, msg.Message, msg.Arg),
                    msg.Expected
                );
                ShouldNotThrow(
                    () => Expect(true, msg.Message, msg.Arg)
                );

                // Assert
            }

            [Test]
            public void WithBool()
            {
                // Arrange

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(false));
                ShouldNotThrow(() => Expect(true));

                // Assert
            }

            [Test]
            public void With_Bool_AndMessageFunc()
            {
                // Arrange
                var expected = GetRandomString();

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(false, () => expected), expected);
                ShouldNotThrow(() => Expect(true, () => expected));

                // Assert
            }

            [Test]
            public void WithFuncBool_AndMessage_AndArgs()
            {
                // Arrange
                var msg = MessageWithArg.CreateRandom();

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(() => false, msg.Message, msg.Arg), msg.Expected);
                ShouldNotThrow(() => Expect(() => true, msg.Message, msg.Arg));

                // Assert
            }

            [Test]
            public void WithFuncBool()
            {
                // Arrange

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(() => false));
                ShouldNotThrow(() => Expect(() => true));

                // Assert
            }

            [Test]
            public void WithFuncBool_FuncMessage()
            {
                // Arrange
                var msg = GetRandomString();

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(() => false, msg), msg);
                ShouldNotThrow(() => Expect(() => true, msg));

                // Assert
            }

            [Test]
            public void WithDelegate_AndConstraint()
            {
                // Arrange
                var str = GetRandomString();

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(() => str, Is.Not.EqualTo(str)));
                ShouldNotThrow(() => Expect(() => str, Is.EqualTo(str)));

                // Assert
            }

            [Test]
            public void WithDelegate_AndConstraint_AndMessage_AndArg()
            {
                // Arrange
                var msg = MessageWithArg.CreateRandom();
                var test = GetRandomInt();
                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(() => test, Is.Not.EqualTo(test), msg.Message, msg.Arg), msg.Expected);
                ShouldNotThrow(() => Expect(() => test, Is.EqualTo(test), msg.Message, msg.Arg));

                // Assert
            }

            [Test]
            public void WithDelegate_AndConstraint_AndMessageFunc()
            {
                // Arrange
                var msg = GetRandomString();
                var test = GetRandomDecimal();

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(() => test, Is.Not.EqualTo(test), () => msg), msg);
                ShouldNotThrow(() => Expect(() => test, Is.EqualTo(test), () => msg));

                // Assert
            }

            [Test]
            public void WithTestDelegate_AndThrowConstraint()
            {
                // Arrange

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(() =>
                    {
                        throw new NotImplementedException();
                    },
                    Throws.Exception.InstanceOf<ArgumentException>()));
                ShouldNotThrow(() => Expect(() =>
                    {
                        throw new ArgumentException();
                    },
                    Throws.Exception.InstanceOf<ArgumentException>()));
                // Assert
            }

            [Test]
            public void WithTestDelegate_AndThrowConstraint_AndMessage_AndArg()
            {
                // Arrange
                var msg = MessageWithArg.CreateRandom();

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(() =>
                        {
                            throw new NotImplementedException();
                        },
                        Throws.Exception.InstanceOf<ArgumentException>(),
                        msg.Message,
                        msg.Arg),
                    msg.Expected);
                ShouldNotThrow(() => Expect(() =>
                    {
                        throw new ArgumentException();
                    },
                    Throws.Exception.InstanceOf<ArgumentException>(),
                    msg.Message,
                    msg.Arg));
                // Assert
            }

            [Test]
            public void WithTestDelegate_AndThrowConstraint_AndMessageFunc()
            {
                // Arrange
                var msg = GetRandomString();

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(() =>
                        {
                            throw new NotImplementedException();
                        },
                        Throws.Exception.InstanceOf<ArgumentException>(),
                        () => msg),
                    msg);
                ShouldNotThrow(() => Expect(() =>
                    {
                        throw new ArgumentException();
                    },
                    Throws.Exception.InstanceOf<ArgumentException>(),
                    () => msg));
                // Assert
            }


            [Test]
            public void WithValueAndExpression()
            {
                // Arrange

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(true, Is.False));
                ShouldNotThrow(() => Expect(null, Is.Null));

                // Assert
            }

            [Test]
            public void WithValueAndExpression_AndMessage_AndArgs()
            {
                // Arrange
                var msg = MessageWithArg.CreateRandom();

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(true, Is.False, msg.Message, msg.Arg), msg.Expected);
                ShouldNotThrow(() => Expect(null, Is.Null, msg.Message, msg.Arg));

                // Assert
            }


            [Test]
            public void WithValueAndExpression_AndMessageFunc()
            {
                // Arrange
                var msg = GetRandomString();

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(true, Is.False, () => msg), msg);
                ShouldNotThrow(() => Expect(null, Is.Null, () => msg));

                // Assert
            }
        }

        [TestFixture]
        public class ShouldRedirectIs
        {
            [TestCaseSource(nameof(GetAllStaticMethodsOnIs))]
            public void ShouldRedirectMethod_(string name)
            {
                // Arrange
                var src = GetMethod(typeof(Is), name);
                var dest = GetRedirectedMethodFor(typeof(Expectations), name);

                // Pre-Assert

                // Act
                Assert.That(dest, Is.Not.Null, () => $"{name} missing on Expectations");
                Assert.That(dest, Is.EqualTo(src), () => $"{name} is does not pass-through to Is");

                // Assert
            }

            [TestCaseSource(nameof(GetAllStaticPropertiesOnIs))]
            public void ShouldRedirectProperty_(string name)
            {
                // Arrange
                var src = GetPropertyValue(typeof(Is), name);
                var dest = GetPropertyValue(typeof(Expectations), name);

                // Pre-Assert

                // Act
                Assert.That(dest, Is.Not.Null, () => $"{name} is missing on Expectations");
                Assert.That(dest, Is.TypeOf(src.GetType()), () => $"{name} does not pass-through to Is");

                // Assert
            }

            [Test]
            public void InstanceOf_Generic_ShouldBeFacade()
            {
                // Arrange

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(new { }, InstanceOf<TestExpect>()));
                ShouldThrow(() => Expect(new { }, InstanceOf(typeof(TestExpect))));
                ShouldNotThrow(() => Expect(new TestExpect(), InstanceOf<TestExpect>()));
                ShouldNotThrow(() => Expect(new TestExpect(), InstanceOf(typeof(TestExpect))));

                // Assert
            }

            [Test]
            public void TypeOf_Generic_ShouldBeFacade()
            {
                // Arrange

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(new Derived(), TypeOf<Base>()));
                ShouldThrow(() => Expect(new Derived(), TypeOf(typeof(Base))));
                ShouldNotThrow(() => Expect(new Derived(), TypeOf<Derived>()));
                ShouldNotThrow(() => Expect(new Derived(), TypeOf(typeof(Derived))));

                // Assert
            }

            [Test]
            public void AssignableTo_ShouldBeFacade()
            {
                // Arrange

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(new Base(), AssignableTo<Derived>()));
                ShouldThrow(() => Expect(new Base(), AssignableTo(typeof(Derived))));
                ShouldNotThrow(() => Expect(new Derived(), AssignableTo<Base>()));
                ShouldNotThrow(() => Expect(new Derived(), AssignableTo(typeof(Base))));

                // Assert
            }


            [Test]
            public void AssignableFrom_ShouldBeFacade()
            {
                // Arrange

                // Pre-Assert

                // Act
                ShouldThrow(() => Expect(new Derived(), AssignableFrom<Base>()));
                ShouldThrow(() => Expect(new Derived(), AssignableFrom(typeof(Base))));
                ShouldNotThrow(() => Expect(new Base(), AssignableFrom<Derived>()));
                ShouldNotThrow(() => Expect(new Base(), AssignableFrom(typeof(Derived))));

                // Assert
            }

            public class Base
            {
            }

            public class Derived : Base
            {
            }

            private static string[] GetAllStaticMethodsOnIs()
            {
                return GetAllStaticMethodsOn(typeof(Is))
                    .Except(new[]
                    {
// facades instead of pass-through
                        "InstanceOf",
                        "AssignableFrom",
                        "TypeOf",
                        "AssignableTo"
                    })
                    .ToArray();
            }

            private static string[] GetAllStaticPropertiesOnIs()
            {
                return GetAllStaticPropertiesOn(typeof(Is));
            }
        }

        [TestFixture]
        public class ShouldRedirectDoes
        {
            [TestCaseSource(nameof(GetAllStaticMethodsOnDoes))]
            public void ShouldRedirectMethod_(string name)
            {
                // Arrange
                var src = GetMethod(typeof(Does), name);
                var dest = GetRedirectedMethodFor(typeof(Expectations), name);

                // Pre-Assert

                // Act
                Assert.That(dest, Is.Not.Null, () => $"{name} missing on Expectations");
                Assert.That(dest, Is.EqualTo(src), () => $"{name} is does not pass-through to Is");

                // Assert
            }
            
            [Test]
            public void ShouldRedirectMethodMatchForRegex()
            {
                // Arrange
                var name = "Match";
                var src = typeof(Does).GetMethods()
                    .FirstOrDefault(mi => mi.Name == nameof(Does.Match) &&
                        mi.GetParameters().First().ParameterType == typeof(string));
                var dest = GetRedirectedMethodFor(typeof(Expectations), name);

                // Pre-Assert

                // Act
                Assert.That(dest, Is.Not.Null, () => $"{name} missing on Expectations");
                Assert.That(dest, Is.EqualTo(src), () => $"{name} is does not pass-through to Is");

                // Assert
            }

            [TestCaseSource(nameof(GetAllStaticPropertiesOnDoes))]
            public void ShouldRedirectProperty_(string name)
            {
                // Arrange
                var src = GetPropertyValue(typeof(Does), name);
                var dest = GetPropertyValue(typeof(Expectations), name);

                // Pre-Assert

                // Act
                Assert.That(dest, Is.Not.Null, () => $"{name} is missing on Expectations");
                Assert.That(dest, Is.TypeOf(src.GetType()), () => $"{name} does not pass-through to Is");

                // Assert
            }

            private static string[] GetAllStaticMethodsOnDoes()
            {
                return GetAllStaticMethodsOn(typeof(Does))
                    .Except(new[]
                    {
                        "Match",
                        "Contain"    // special cases
                    })
                    .ToArray();
            }

            private static string[] GetAllStaticPropertiesOnDoes()
            {
                return GetAllStaticPropertiesOn(typeof(Does));
            }
        }
        

        private static string[] GetAllStaticMethodsOn(Type type)
        {
            return type.GetMethods(_publicStatic)
                .Select(mi => mi.Name)
                .Where(name => !name.StartsWith("get_"))
                .Where(name => !name.StartsWith("set_"))
                .ToArray();
        }

        private static string[] GetAllStaticPropertiesOn(Type type)
        {
            return type.GetProperties(_publicStatic)
                .Select(pi => pi.Name)
                .ToArray();
        }

        private static object GetMethod(Type type, string name)
        {
            return type.GetMethod(name, _publicStatic);
        }

        private static object GetPropertyValue(Type type, string name)
        {
            return GetProperty(type, name)?.GetValue(null);
        }

        private static PropertyInfo GetProperty(Type type, string name)
        {
            return type.GetProperty(name, _publicStatic);
        }

        private static object GetRedirectedMethodFor(Type type, string name)
        {
            var propInfo = type.GetProperty(name, _publicStatic);
            var result = propInfo?.GetValue(null);
            return result.GetPropertyValue("Method");
        }

        private static BindingFlags _publicStatic = BindingFlags.Public | BindingFlags.Static;

        private class MessageWithArg
        {
            public string Message { get; }
            public string Arg { get; }
            public string Expected { get; }

            public MessageWithArg(string message, string arg, string expected)
            {
                Message = message;
                Arg = arg;
                Expected = expected;
            }

            public static MessageWithArg CreateRandom()
            {
                var messagePart = GetRandomString();
                var fullMessage = GetRandomBoolean()
                    ? "{0} " + messagePart
                    : messagePart + " {0}";
                var arg = GetRandomString();
                var expected = fullMessage.Replace("{0}", arg);
                return new MessageWithArg(fullMessage, arg, expected);
            }
        }

        private static void ShouldThrow(Action a)
        {
            Assert.That(
                () => a(),
                Throws.Exception.InstanceOf<AssertionException>());
        }

        private static void ShouldNotThrow(Action a)
        {
            Assert.That(
                () => a(),
                Throws.Nothing);
        }

        private static void ShouldThrow(Action a, string message)
        {
            Assert.That(
                () => a(),
                Throws.Exception.InstanceOf<AssertionException>().With.Message.Contains(message));
        }
    }
}