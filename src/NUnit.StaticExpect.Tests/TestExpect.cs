using System;
using NUnit.Framework;
using static NUnit.StaticExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NUnit.StaticExpect.Tests
{
    public class TestExpect
    {
        [Test]
        public void Expect_With_BoolCondition_Message_Args()
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
        public void Expect_With_Bool()
        {
            // Arrange

            // Pre-Assert

            // Act
            ShouldThrow(() => Expect(false));
            ShouldNotThrow(() => Expect(true));

            // Assert
        }

        [Test]
        public void Expect_With_Bool_AndMessageFunc()
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
        public void Expect_WithFuncBool_AndMessage_AndArgs()
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
        public void Expect_WithFuncBool()
        {
            // Arrange

            // Pre-Assert

            // Act
            ShouldThrow(() => Expect(() => false));
            ShouldNotThrow(() => Expect(() => true));

            // Assert
        }

        [Test]
        public void Expect_WithFuncBool_FuncMessage()
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
        public void Expect_WithDelegate_AndConstraint()
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
        public void Expect_WithDelegate_AndConstraint_AndMessage_AndArg()
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
        public void Expect_WithDelegate_AndConstraint_AndMessageFunc()
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
        public void Expect_WithTestDelegate_AndThrowConstraint()
        {
            // Arrange

            // Pre-Assert

            // Act
            ShouldThrow(() => Expect(() => { throw new NotImplementedException(); }, Throws.Exception.InstanceOf<ArgumentException>()));
            ShouldNotThrow(() => Expect(() => { throw new ArgumentException(); }, Throws.Exception.InstanceOf<ArgumentException>()));
            // Assert
        }

        [Test]
        public void Expect_WithTestDelegate_AndThrowConstraint_AndMessage_AndArg()
        {
            // Arrange
            var msg = MessageWithArg.CreateRandom();

            // Pre-Assert

            // Act
            ShouldThrow(() => Expect(() => { throw new NotImplementedException(); }, 
                Throws.Exception.InstanceOf<ArgumentException>(),
                msg.Message, msg.Arg), msg.Expected);
            ShouldNotThrow(() => Expect(() => { throw new ArgumentException(); }, 
                Throws.Exception.InstanceOf<ArgumentException>(),
                msg.Message, msg.Arg));
            // Assert
        }

        [Test]
        public void Expect_WithTestDelegate_AndThrowConstraint_AndMessageFunc()
        {
            // Arrange
            var msg = GetRandomString();

            // Pre-Assert

            // Act
            ShouldThrow(() => Expect(() => { throw new NotImplementedException(); }, 
                Throws.Exception.InstanceOf<ArgumentException>(),
                () => msg), msg);
            ShouldNotThrow(() => Expect(() => { throw new ArgumentException(); }, 
                Throws.Exception.InstanceOf<ArgumentException>(),
                () => msg));
            // Assert
        }


        [Test]
        public void Expect_WithValueAndExpression()
        {
            // Arrange

            // Pre-Assert

            // Act
            ShouldThrow(() => Expect(true, Is.False));
            ShouldNotThrow(() => Expect(null, Is.Null));

            // Assert
        }

        [Test]
        public void Expect_WithValueAndExpression_AndMessage_AndArgs()
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
        public void Expect_WithValueAndExpression_AndMessageFunc()
        {
            // Arrange
            var msg = GetRandomString();

            // Pre-Assert

            // Act
            ShouldThrow(() => Expect(true, Is.False, () => msg), msg);
            ShouldNotThrow(() => Expect(null, Is.Null, () => msg));

            // Assert
        }



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
                var fullMessage = GetRandomBoolean() ? "{0} " + messagePart : messagePart + " {0}";
                var arg = GetRandomString();
                var expected = fullMessage.Replace("{0}", arg);
                return new MessageWithArg(fullMessage, arg, expected);
            }
        }

        private void ShouldThrow(Action a)
        {
            Assert.That(
                () => a(),
                Throws.Exception.InstanceOf<AssertionException>());
        }

        private void ShouldNotThrow(Action a)
        {
            Assert.That(
                () => a(),
                Throws.Nothing);
        }

        private void ShouldThrow(Action a, string message)
        {
            Assert.That(
                () => a(),
                Throws.Exception.InstanceOf<AssertionException>().With.Message.Contains(message));
        }
    }
}