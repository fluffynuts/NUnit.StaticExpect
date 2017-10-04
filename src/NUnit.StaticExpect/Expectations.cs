using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace NUnit.StaticExpect
{
    public static partial class Expectations
    {
        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="T:NUnit.Framework.AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display if the condition is false</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void Expect(bool condition, string message, params object[] args)
        {
            Assert.That(condition, message, args);
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="T:NUnit.Framework.AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        public static void Expect(bool condition)
        {
            Assert.That(condition);
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="T:NUnit.Framework.AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="getExceptionMessage">A function to build the message included with the Exception</param>
        public static void Expect(bool condition, Func<string> getExceptionMessage)
        {
            Assert.That(condition, getExceptionMessage);
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="T:NUnit.Framework.AssertionException" />.
        /// </summary>
        /// <param name="condition">A lambda that returns a Boolean</param>
        /// <param name="message">The message to display if the condition is false</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void Expect(Func<bool> condition, string message, params object[] args)
        {
            Assert.That(condition, message, args);
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="T:NUnit.Framework.AssertionException" />.
        /// </summary>
        /// <param name="condition">A lambda that returns a Boolean</param>
        public static void Expect(Func<bool> condition)
        {
            Assert.That(condition);
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="T:NUnit.Framework.AssertionException" />.
        /// </summary>
        /// <param name="condition">A lambda that returns a Boolean</param>
        /// <param name="getExceptionMessage">A function to build the message included with the Exception</param>
        public static void Expect(Func<bool> condition, Func<string> getExceptionMessage)
        {
            Assert.That(condition, getExceptionMessage);
        }

        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <typeparam name="TActual">The Type being compared.</typeparam>
        /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        /// <param name="expr">A Constraint expression to be applied</param>
        public static void Expect<TActual>(ActualValueDelegate<TActual> del, IResolveConstraint expr)
        {
            Assert.That(del, expr);
        }

        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <typeparam name="TActual">The Type being compared.</typeparam>
        /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        /// <param name="expr">A Constraint expression to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void Expect<TActual>(ActualValueDelegate<TActual> del, IResolveConstraint expr, string message,
            params object[] args)
        {
            Assert.That(del, expr, message, args);
        }

        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <typeparam name="TActual">The Type being compared.</typeparam>
        /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        /// <param name="expr">A Constraint expression to be applied</param>
        /// <param name="getExceptionMessage">A function to build the message included with the Exception</param>
        public static void Expect<TActual>(ActualValueDelegate<TActual> del, IResolveConstraint expr,
            Func<string> getExceptionMessage)
        {
            Assert.That(del, expr, getExceptionMessage);
        }

        /// <summary>
        /// Asserts that the code represented by a delegate throws an exception
        /// that satisfies the constraint provided.
        /// </summary>
        /// <param name="code">A TestDelegate to be executed</param>
        /// <param name="constraint">A ThrowsConstraint used in the test</param>
        public static void Expect(TestDelegate code, IResolveConstraint constraint)
        {
            Assert.That(code, constraint);
        }

        /// <summary>
        /// Asserts that the code represented by a delegate throws an exception
        /// that satisfies the constraint provided.
        /// </summary>
        /// <param name="code">A TestDelegate to be executed</param>
        /// <param name="constraint">A ThrowsConstraint used in the test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void Expect(TestDelegate code, IResolveConstraint constraint, string message, params object[] args)
        {
            Assert.That(code, constraint, message, args);
        }

        /// <summary>
        /// Asserts that the code represented by a delegate throws an exception
        /// that satisfies the constraint provided.
        /// </summary>
        /// <param name="code">A TestDelegate to be executed</param>
        /// <param name="constraint">A ThrowsConstraint used in the test</param>
        /// <param name="getExceptionMessage">A function to build the message included with the Exception</param>
        public static void Expect(TestDelegate code, IResolveConstraint constraint, Func<string> getExceptionMessage)
        {
            Assert.That(code, constraint, getExceptionMessage);
        }

        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <typeparam name="TActual">The Type being compared.</typeparam>
        /// <param name="actual">The actual value to test</param>
        /// <param name="expression">A Constraint to be applied</param>
        public static void Expect<TActual>(TActual actual, IResolveConstraint expression)
        {
            Assert.That(actual, expression);
        }

        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <typeparam name="TActual">The Type being compared.</typeparam>
        /// <param name="actual">The actual value to test</param>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void Expect<TActual>(TActual actual, IResolveConstraint expression, string message,
            params object[] args)
        {
            Assert.That(actual, expression, message, args);
        }

        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <typeparam name="TActual">The Type being compared.</typeparam>
        /// <param name="actual">The actual value to test</param>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="getExceptionMessage">A function to build the message included with the Exception</param>
        public static void Expect<TActual>(TActual actual, IResolveConstraint expression,
            Func<string> getExceptionMessage)
        {
            Assert.That(actual, expression, getExceptionMessage);
        }
    }
}