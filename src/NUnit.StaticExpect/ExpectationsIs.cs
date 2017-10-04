using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace NUnit.StaticExpect
{
    public static partial class Expectations
    {
        //
        // Summary:
        //     Returns a constraint that tests whether a collection is ordered
        public static CollectionOrderedConstraint Ordered => Is.Ordered;

        //
        // Summary:
        //     Returns a ConstraintExpression that negates any following constraint.
        public static ConstraintExpression Not => Is.Not;

        //
        // Summary:
        //     Returns a ConstraintExpression, which will apply the following constraint to
        //     all members of a collection, succeeding if all of them succeed.
        public static ConstraintExpression All => Is.All;

        //
        // Summary:
        //     Returns a constraint that tests for null
        public static NullConstraint Null => Is.Null;

        //
        // Summary:
        //     Returns a constraint that tests for True
        public static TrueConstraint True => Is.True;

        //
        // Summary:
        //     Returns a constraint that tests for a positive value
        public static GreaterThanConstraint Positive => Is.Positive;

        //
        // Summary:
        //     Returns a constraint that tests for a negative value
        public static LessThanConstraint Negative => Is.Negative;

        //
        // Summary:
        //     Returns a constraint that tests for False
        public static FalseConstraint False => Is.False;

        //
        // Summary:
        //     Returns a constraint that tests for equality with zero
        public static EqualConstraint Zero => Is.Zero;

        //
        // Summary:
        //     Returns a constraint that tests for NaN
        public static NaNConstraint NaN => Is.NaN;

        //
        // Summary:
        //     Returns a constraint that tests for empty
        public static EmptyConstraint Empty => Is.Empty;

        //
        // Summary:
        //     Returns a constraint that tests whether a collection contains all unique items.
        public static UniqueItemsConstraint Unique => Is.Unique;

#if NETSTANDARD1_4
#else
        //
        // Summary:
        //     Returns a constraint that tests whether an object graph is serializable in binary
        //     format.
        public static BinarySerializableConstraint BinarySerializable =>
            Is.BinarySerializable;

        //
        // Summary:
        //     Returns a constraint that tests whether an object graph is serializable in xml
        //     format.
        public static XmlSerializableConstraint XmlSerializable =>
            Is.XmlSerializable;
#endif

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is assignable from the
        //     type supplied as an argument.
        public static AssignableFromConstraint AssignableFrom(Type expectedType)
        {
            return Is.AssignableFrom(expectedType);
        }

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is assignable from the
        //     type supplied as an argument.
        public static AssignableFromConstraint AssignableFrom<TExpected>()
        {
            return Is.AssignableFrom<TExpected>();
        }

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is assignable to the
        //     type supplied as an argument.
        public static AssignableToConstraint AssignableTo(Type expectedType)
        {
            return Is.AssignableTo(expectedType);
        }

        // Summary:
        //     Returns a constraint that tests whether the actual value is assignable to the
        //     type supplied as an argument.
        public static AssignableToConstraint AssignableTo<TExpected>()
        {
            return Is.AssignableTo<TExpected>();
        }

        // Summary:
        //     Returns a constraint that tests whether the actual value is greater than or equal
        //     to the supplied argument
        public static Func<object, GreaterThanOrEqualConstraint> AtLeast =>
            Is.AtLeast;

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is less than or equal
        //     to the supplied argument
        public static Func<object, LessThanOrEqualConstraint> AtMost =>
            Is.AtMost;

        //
        // Summary:
        //     Returns a constraint that tests two items for equality
        public static Func<object, EqualConstraint> EqualTo => Is.EqualTo;

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is a collection containing
        //     the same elements as the collection supplied as an argument.
        public static Func<IEnumerable, CollectionEquivalentConstraint>
            EquivalentTo => Is.EquivalentTo;

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is greater than the
        //     supplied argument
        public static Func<object, GreaterThanConstraint> GreaterThan => Is.GreaterThan;

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is greater than or equal
        //     to the supplied argument
        public static Func<object, GreaterThanOrEqualConstraint> GreaterThanOrEqualTo =>
            Is.GreaterThanOrEqualTo;

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value falls inclusively within
        //     a specified range.
        //
        // Parameters:
        //   from:
        //     Inclusive beginning of the range. Must be less than or equal to to.
        //
        //   to:
        //     Inclusive end of the range. Must be greater than or equal to from.
        //
        // Remarks:
        //     from must be less than or equal to true
        public static Func<IComparable, IComparable, RangeConstraint> InRange =>
            Is.InRange;

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is of the type supplied
        //     as an argument or a derived type.
        public static InstanceOfTypeConstraint InstanceOf<TExpected>()
        {
            return Is.InstanceOf<TExpected>();
        }

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is of the type supplied
        //     as an argument or a derived type.
        public static InstanceOfTypeConstraint InstanceOf(Type expectedType)
        {
            return Is.InstanceOf(expectedType);
        }

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is less than the supplied
        //     argument
        public static Func<object, LessThanConstraint> LessThan => Is.LessThan;

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is less than or equal
        //     to the supplied argument
        public static Func<object, LessThanOrEqualConstraint> LessThanOrEqualTo =>
            Is.LessThanOrEqualTo;

        //
        // Summary:
        //     Returns a constraint that tests that two references are the same object
        public static Func<object, SameAsConstraint> SameAs => Is.SameAs;

        //
        // Summary:
        //     Returns a constraint that tests whether the path provided is the same as an expected
        //     path after canonicalization.
        public static Func<string, SamePathConstraint> SamePath => Is.SamePath;

        //
        // Summary:
        //     Returns a constraint that tests whether the path provided is the same path or
        //     under an expected path after canonicalization.
        public static Func<string, SamePathOrUnderConstraint> SamePathOrUnder =>
            Is.SamePathOrUnder;

        //        //
        // Summary:
        //     Returns a constraint that succeeds if the actual value contains the substring
        //     supplied as an argument.
        [Obsolete("Deprecated, use Does.Contain")]
        public static Func<string, SubstringConstraint> StringContaining =>
            Is.StringContaining;

        //
        // Summary:
        //     Returns a constraint that succeeds if the actual value ends with the substring
        //     supplied as an argument.
        [Obsolete("Deprecated, use Does.EndWith")]
        public static Func<string, EndsWithConstraint> StringEnding =>
            Is.StringEnding;

        //
        // Summary:
        //     Returns a constraint that succeeds if the actual value matches the regular expression
        //     supplied as an argument.
        [Obsolete("Deprecated, use Does.Match")]
        public static Func<string, RegexConstraint> StringMatching =>
            Is.StringMatching;

        //
        // Summary:
        //     Returns a constraint that succeeds if the actual value starts with the substring
        //     supplied as an argument.
        [Obsolete("Deprecated, use Does.StartWith")]
        public static Func<string, StartsWithConstraint> StringStarting =>
            Is.StringStarting;

        //
        // Summary:
        //     Returns a constraint that tests whether the path provided is a subpath of the
        //     expected path after canonicalization.
        public static Func<string, SubPathConstraint> SubPathOf =>
            Is.SubPathOf;

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is a subset of the collection
        //     supplied as an argument.
        public static Func<IEnumerable, CollectionSubsetConstraint> SubsetOf =>
            Is.SubsetOf;

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is a superset of the
        //     collection supplied as an argument.
        public static Func<IEnumerable, CollectionSupersetConstraint> SupersetOf =>
            Is.SupersetOf;

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is of the exact type
        //     supplied as an argument.
        public static ExactTypeConstraint TypeOf<TExpected>()
        {
            return Is.TypeOf<TExpected>();
        }

        //
        // Summary:
        //     Returns a constraint that tests whether the actual value is of the exact type
        //     supplied as an argument.
        public static ExactTypeConstraint TypeOf(Type expectedType)
        {
            return Is.TypeOf(expectedType);
        }
    }
}