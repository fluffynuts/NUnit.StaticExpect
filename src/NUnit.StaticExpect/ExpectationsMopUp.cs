using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections;

namespace NUnit.StaticExpect
{
    public static partial class Expectations
    {
        /// <summary>
        /// Returns a new ContainsConstraint. This constraint
        /// will, in turn, make use of the appropriate second-level
        /// constraint, depending on the type of the actual argument.
        /// This overload is only used if the item sought is a string,
        /// since any other type implies that we are looking for a
        /// collection member.
        /// </summary>
        public static ContainsConstraint Contains(string expected)
        {
            return new ContainsConstraint(expected);
        }

        /// <summary>
        /// Returns a new <see cref="EqualConstraint"/> checking for the
        /// presence of a particular object in the collection.
        /// </summary>
        public static EqualConstraint Contains(object expected)
        {
            return Has.Some.EqualTo(expected);
        }

        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value contains the substring supplied as an argument.
        /// </summary>
        [Obsolete("Deprecated, use Contains")]
        public static SubstringConstraint ContainsSubstring(string expected)
        {
            return new SubstringConstraint(expected);
        }

        /// <summary>
        /// Returns a constraint that fails if the actual
        /// value contains the substring supplied as an argument.
        /// </summary>
        [Obsolete("Deprecated, use Does.Not.Contain")]
        public static SubstringConstraint DoesNotContain(string expected)
        {
            return new ConstraintExpression().Not.ContainsSubstring(expected);
        }

        /// <summary>
        /// Returns a constraint that fails if the actual
        /// value ends with the substring supplied as an argument.
        /// </summary>
        [Obsolete("Deprecated, use Does.Not.EndWith")]
        public static EndsWithConstraint DoesNotEndWith(string expected)
        {
            return new ConstraintExpression().Not.EndsWith(expected);
        }

        /// <summary>
        /// Returns a constraint that fails if the actual
        /// value matches the pattern supplied as an argument.
        /// </summary>
        [Obsolete("Deprecated, use Does.Not.Match")]
        public static RegexConstraint DoesNotMatch(string pattern)
        {
            return new ConstraintExpression().Not.Matches(pattern);
        }

        /// <summary>
        /// Returns a constraint that fails if the actual
        /// value starts with the substring supplied as an argument.
        /// </summary>
        [Obsolete("Deprecated, use Does.Not.StartWith")]
        public static StartsWithConstraint DoesNotStartWith(string expected)
        {
            return new ConstraintExpression().Not.StartsWith(expected);
        }

        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value ends with the substring supplied as an argument.
        /// </summary>
        public static EndsWithConstraint EndsWith(string expected)
        {
            return new EndsWithConstraint(expected);
        }
        
        /// <summary>
        /// Returns a ListMapper based on a collection.
        /// </summary>
        /// <param name="original">The original collection</param>
        /// <returns></returns>
#pragma warning disable 618
        public static ListMapper Map(ICollection original)
        {
            return new ListMapper(original);
        }
#pragma warning restore 618

        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value matches the regular expression supplied as an argument.
        /// </summary>
        public static RegexConstraint Matches(string pattern)
        {
            return new RegexConstraint(pattern);
        }


        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value starts with the substring supplied as an argument.
        /// </summary>
        public static StartsWithConstraint StartsWith(string expected)
        {
            return new StartsWithConstraint(expected);
        }

        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value contains the substring supplied as an argument.
        /// </summary>
        [Obsolete("Deprecated, use Contains")]
        public static SubstringConstraint StringContaining(string expected)
        {
            return new SubstringConstraint(expected);
        }

        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value ends with the substring supplied as an argument.
        /// </summary>
        [Obsolete("Deprecated, use Does.EndWith or EndsWith")]
        public static EndsWithConstraint StringEnding(string expected)
        {
            return new EndsWithConstraint(expected);
        }

        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value matches the regular expression supplied as an argument.
        /// </summary>
        [Obsolete("Deprecated, use Does.Match or Matches")]
        public static RegexConstraint StringMatching(string pattern)
        {
            return new RegexConstraint(pattern);
        }

        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value starts with the substring supplied as an argument.
        /// </summary>
        [Obsolete("Deprecated, use Does.StartWith or StartsWith")]
        public static StartsWithConstraint StringStarting(string expected)
        {
            return new StartsWithConstraint(expected);
        }
    }
}
