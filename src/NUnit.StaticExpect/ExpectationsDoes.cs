using System;
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
        /// <summary>
        /// Returns a constraint that succeeds if the value
        /// is a file or directory and it exists.
        /// </summary>
        public static FileOrDirectoryExistsConstraint Exist => Does.Exist;

        /// <summary>
        /// Returns a new CollectionContainsConstraint checking for the
        /// presence of a particular object in the collection.
        /// </summary>
        public static SomeItemsConstraint Contain(object expected)
        {
            return Does.Contain(expected);
        }

        /// <summary>
        /// Returns a new ContainsConstraint. This constraint
        /// will, in turn, make use of the appropriate second-level
        /// constraint, depending on the type of the actual argument.
        /// This overload is only used if the item sought is a string,
        /// since any other type implies that we are looking for a
        /// collection member.
        /// </summary>
        public static ContainsConstraint Contain(string expected)
        {
            return Does.Contain(expected);
        }

        /// <summary>
        /// Returns a new DictionaryContainsKeyConstraint checking for the
        /// presence of a particular key in the Dictionary key collection.
        /// </summary>
        /// <param name="expected">The key to be matched in the Dictionary key collection</param>
        public static Func<object, DictionaryContainsKeyConstraint> ContainKey =>
            Does.ContainKey;

        /// <summary>
        /// Returns a new DictionaryContainsValueConstraint checking for the
        /// presence of a particular value in the Dictionary value collection.
        /// </summary>
        /// <param name="expected">The value to be matched in the Dictionary value collection</param>
        public static Func<object, DictionaryContainsValueConstraint> ContainValue =>
            Does.ContainValue;

        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value starts with the substring supplied as an argument.
        /// </summary>
        public static Func<string, StartsWithConstraint> StartWith =>
            Does.StartWith;

        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value ends with the substring supplied as an argument.
        /// </summary>
        public static Func<string, EndsWithConstraint> EndWith =>
            Does.EndWith;

        /// <summary>
        /// Returns a constraint that succeeds if the actual
        /// value matches the regular expression supplied as an argument.
        /// </summary>
        public static Func<string, RegexConstraint> Match =>
            Does.Match;
    }
}