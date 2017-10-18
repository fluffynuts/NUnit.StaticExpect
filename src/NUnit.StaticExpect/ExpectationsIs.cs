using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Constraints;
#pragma warning disable 1584,1711,1572,1581,1580

namespace NUnit.StaticExpect
{
    public static partial class Expectations
    {
        /// <summary>
        ///     Returns a constraint that tests whether a collection is ordered
        /// </summary>
        public static CollectionOrderedConstraint Ordered => Is.Ordered;

        /// Summary:
        /// <summary>
        ///     Returns a ConstraintExpression that negates any following constraint.
        /// </summary>
        public static ConstraintExpression Not => Is.Not;

        /// <summary>
        ///     Returns a ConstraintExpression, which will apply the following constraint to
        ///     all members of a collection, succeeding if all of them succeed.
        /// </summary>
        public static ConstraintExpression All => Is.All;

        /// <summary>
        ///     Returns a constraint that tests for null
        /// </summary>
        public static NullConstraint Null => Is.Null;

        /// <summary>
        ///     Returns a constraint that tests for True
        /// </summary>
        public static TrueConstraint True => Is.True;

        /// <summary>
        ///     Returns a constraint that tests for a positive value
        /// </summary>
        public static GreaterThanConstraint Positive => Is.Positive;

        /// <summary>
        ///     Returns a constraint that tests for a negative value
        /// </summary>
        public static LessThanConstraint Negative => Is.Negative;

        /// <summary>
        ///     Returns a constraint that tests for False
        /// </summary>
        public static FalseConstraint False => Is.False;

        /// <summary>
        ///     Returns a constraint that tests for equality with zero
        /// </summary>
        public static EqualConstraint Zero => Is.Zero;

        /// <summary>
        ///     Returns a constraint that tests for NaN
        /// </summary>
        public static NaNConstraint NaN => Is.NaN;

        /// <summary>
        ///     Returns a constraint that tests for empty
        /// </summary>
        public static EmptyConstraint Empty => Is.Empty;

        /// <summary>
        ///     Returns a constraint that tests whether a collection contains all unique items.
        /// </summary>
        public static UniqueItemsConstraint Unique => Is.Unique;

#if NETSTANDARD1_4
#else
        /// <summary>
        ///     Returns a constraint that tests whether an object graph is serializable in binary
        ///     format.
        /// </summary>
        public static BinarySerializableConstraint BinarySerializable =>
            Is.BinarySerializable;

        /// <summary>
        ///     Returns a constraint that tests whether an object graph is serializable in xml
        ///     format.
        /// </summary>
        public static XmlSerializableConstraint XmlSerializable =>
            Is.XmlSerializable;
#endif

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is assignable from the
        ///     type supplied as an argument.
        /// </summary>
        /// <param name="expectedType"></param>
        /// <returns></returns>
        public static AssignableFromConstraint AssignableFrom(Type expectedType)
        {
            return Is.AssignableFrom(expectedType);
        }

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is assignable from the
        ///     type supplied as an argument.
        /// </summary>
        /// <typeparam name="TExpected"></typeparam>
        /// <returns></returns>
        public static AssignableFromConstraint AssignableFrom<TExpected>()
        {
            return Is.AssignableFrom<TExpected>();
        }

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is assignable to the
        ///     type supplied as an argument.
        /// </summary>
        /// <param name="expectedType"></param>
        /// <returns></returns>
        public static AssignableToConstraint AssignableTo(Type expectedType)
        {
            return Is.AssignableTo(expectedType);
        }

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is assignable to the
        ///     type supplied as an argument.
        /// </summary>
        /// <typeparam name="TExpected"></typeparam>
        /// <returns></returns>
        public static AssignableToConstraint AssignableTo<TExpected>()
        {
            return Is.AssignableTo<TExpected>();
        }

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is greater than or equal
        ///     to the supplied argument
        /// </summary>
        public static Func<object, GreaterThanOrEqualConstraint> AtLeast =>
            Is.AtLeast;

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is less than or equal
        ///     to the supplied argument
        /// </summary>
        public static Func<object, LessThanOrEqualConstraint> AtMost =>
            Is.AtMost;

        /// <summary>
        ///     Returns a constraint that tests two items for equality
        /// </summary>
        public static Func<object, EqualConstraint> EqualTo => Is.EqualTo;

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is a collection containing
        ///     the same elements as the collection supplied as an argument.
        /// </summary>
        public static Func<IEnumerable, CollectionEquivalentConstraint>
            EquivalentTo => Is.EquivalentTo;

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is greater than the
        ///     supplied argument
        /// </summary>
        public static Func<object, GreaterThanConstraint> GreaterThan => Is.GreaterThan;

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is greater than or equal
        ///     to the supplied argument
        /// </summary>
        public static Func<object, GreaterThanOrEqualConstraint> GreaterThanOrEqualTo =>
            Is.GreaterThanOrEqualTo;

        /// <summary>
        ///     Returns a constraint that tests whether the actual value falls inclusively within
        ///     a specified range.
        /// <param name="from">Inclusive beginning of the range. Must be less than or equal to to.</param>
        /// <param name="to">Inclusive end of the range. Must be greater than or equal to from.</param>
        /// <remarks>from must be less than or equal to true</remarks>
        /// </summary>
        public static Func<Object, Object, RangeConstraint> InRange =>
            Is.InRange;

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is of the type supplied
        ///     as an argument or a derived type.
        /// </summary>
        /// <typeparam name="TExpected"></typeparam>
        /// <returns></returns>
        public static InstanceOfTypeConstraint InstanceOf<TExpected>()
        {
            return Is.InstanceOf<TExpected>();
        }

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is of the type supplied
        ///     as an argument or a derived type.
        /// </summary>
        /// <param name="expectedType"></param>
        /// <returns></returns>
        public static InstanceOfTypeConstraint InstanceOf(Type expectedType)
        {
            return Is.InstanceOf(expectedType);
        }

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is less than the supplied
        ///     argument
        /// </summary>
        public static Func<object, LessThanConstraint> LessThan => Is.LessThan;

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is less than or equal
        ///     to the supplied argument
        /// </summary>
        public static Func<object, LessThanOrEqualConstraint> LessThanOrEqualTo =>
            Is.LessThanOrEqualTo;

        /// <summary>
        ///     Returns a constraint that tests that two references are the same object
        /// </summary>
        public static Func<object, SameAsConstraint> SameAs => Is.SameAs;

        /// <summary>
        ///     Returns a constraint that tests whether the path provided is the same as an expected
        ///     path after canonicalization.
        /// </summary>
        public static Func<string, SamePathConstraint> SamePath => Is.SamePath;

        /// <summary>
        ///     Returns a constraint that tests whether the path provided is the same path or
        ///     under an expected path after canonicalization.
        /// </summary>
        public static Func<string, SamePathOrUnderConstraint> SamePathOrUnder =>
            Is.SamePathOrUnder;

        /// <summary>
        ///     Returns a constraint that tests whether the path provided is a subpath of the
        ///     expected path after canonicalization.
        /// </summary>
        public static Func<string, SubPathConstraint> SubPathOf =>
            Is.SubPathOf;

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is a subset of the collection
        ///     supplied as an argument.
        /// </summary>
        public static Func<IEnumerable, CollectionSubsetConstraint> SubsetOf =>
            Is.SubsetOf;

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is a superset of the
        ///     collection supplied as an argument.
        /// </summary>
        public static Func<IEnumerable, CollectionSupersetConstraint> SupersetOf =>
            Is.SupersetOf;

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is of the exact type
        ///     supplied as an argument.
        /// </summary>
        /// <typeparam name="TExpected"></typeparam>
        /// <returns></returns>
        public static ExactTypeConstraint TypeOf<TExpected>()
        {
            return Is.TypeOf<TExpected>();
        }

        /// <summary>
        ///     Returns a constraint that tests whether the actual value is of the exact type
        ///     supplied as an argument.
        /// </summary>
        /// <param name="expectedType"></param>
        /// <returns></returns>
        public static ExactTypeConstraint TypeOf(Type expectedType)
        {
            return Is.TypeOf(expectedType);
        }
    }
}