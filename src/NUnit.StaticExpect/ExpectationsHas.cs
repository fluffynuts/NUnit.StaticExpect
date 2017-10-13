using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace NUnit.StaticExpect
{
    public static partial class Expectations
    {
        /// <summary>
        /// Returns a ConstraintExpression that negates any
        /// following constraint.
        /// </summary>
        public static ConstraintExpression No => Has.No;

        /// <summary>
        /// Returns a ConstraintExpression, which will apply
        /// the following constraint to all members of a collection,
        /// succeeding if at least one of them succeeds.
        /// </summary>
        public static ConstraintExpression Some => Has.Some;

        /// <summary>
        /// Returns a ConstraintExpression, which will apply
        /// the following constraint to all members of a collection,
        /// succeeding if all of them fail.
        /// </summary>
        public static ConstraintExpression None => Has.None;

        /// <summary>
        /// Returns a ConstraintExpression, which will apply
        /// the following constraint to all members of a collection,
        /// succeeding only if a specified number of them succeed.
        /// </summary>
        public static Func<int, ItemsConstraintExpression> Exactly => Has.Exactly;

        /// <summary>
        /// Returns a new PropertyConstraintExpression, which will either
        /// test for the existence of the named property on the object
        /// being tested or apply any following constraint to that property.
        /// </summary>
        public static Func<string, ResolvableConstraintExpression> Property => Has.Property;

        /// <summary>
        /// Returns a new ConstraintExpression, which will apply the following
        /// constraint to the Length property of the object being tested.
        /// </summary>
        public static ResolvableConstraintExpression Length => Has.Length;

        /// <summary>
        /// Returns a new ConstraintExpression, which will apply the following
        /// constraint to the Count property of the object being tested.
        /// </summary>
        public static ResolvableConstraintExpression Count => Has.Count;

        /// <summary>
        /// Returns a new ConstraintExpression, which will apply the following
        /// constraint to the Message property of the object being tested.
        /// </summary>
        public static ResolvableConstraintExpression Message => Has.Message;

        /// <summary>
        /// Returns a new ConstraintExpression, which will apply the following
        /// constraint to the InnerException property of the object being tested.
        /// </summary>
        public static ResolvableConstraintExpression InnerException
            => Has.InnerException;

        /// <summary>
        /// Returns a new AttributeConstraint checking for the
        /// presence of a particular attribute on an object.
        /// </summary>
        public static ResolvableConstraintExpression Attribute(Type expectedType)
        {
            return Has.Attribute(expectedType);
        }

        /// <summary>
        /// Returns a new AttributeConstraint checking for the
        /// presence of a particular attribute on an object.
        /// </summary>
        public static ResolvableConstraintExpression Attribute<T>()
        {
            return Has.Attribute(typeof(T));
        }

        /// <summary>
        /// Returns a new CollectionContainsConstraint checking for the
        /// presence of a particular object in the collection.
        /// </summary>
        public static Func<object, EqualConstraint> Member =>
            Has.Member;
    }
}