# NUnit.StaticExpect
Some users prefer a shorter form of assertion than is given by 
NUnit's `Assert.That`. If you statically import `NUnit.StaticExpect`, 
the Expect() method may be used instead...

## Usage
1. Install the nuget package (`install-package nunit.staticexpect`)
2. Import the static methods: add the following using statement to your test class file:
```
// C#
using static NUnit.StaticExpect.Expectations;
```
```
' VB
Imports NUnit.StaticExpect.Expectations
```

3. Use the Expect() syntax:
```
[Test]
public void Expect_True_IsTrue()
{
  Expect(bool condition);
  Expect(bool condition, string message, params object[] parms);

  Expect(ActualValueDelegate del, IResolveConstraint constraint)
  Expect(ActualValueDelegate del, IResolveConstraint constraint,
        string message, params object[] parms)

  Expect<TActual>(TActual actual, IResolveConstraint constraint)
  Expect<TActual>(TActual actual, IResolveConstraint constraint,
                  string message, params object[] parms)

  Expect(TestDelegate del, IResolveConstraint constraint);
}
```

In addition, `NUnit.StaticExpect` allows the test fixture where the static
import has been done to make direct use of many of the syntactic elements
that would normally require you to specify the `Is`, `Has` or `Does` 
classes in order to use them. For example, you can write...

```
Expect(actual, EqualTo("Hello"));
```

`NUnit.StaticExpect` depends on `NUnit` -- indeed, installing the 
`NUnit.StaticExpect` package should also install the relevant `NUnit` package
if not already installed. As such, all `NUnit` syntax is available, including
`Assert.Warn` and `Assume.That`. Most of `NUnit.StaticExpect`s methods and
properties are simply pass-through shims to the relevant `NUnit` ones.

## Why does this project exist?
NUnit has historically had a class named `AssertionHelper` which your
test fixtures could inherit to provide the above syntax. [NUnit issue #1212](https://github.com/nunit/nunit/issues/1212) discusses the 
deprecation of `AssertionHelper`. 

Personally, I had been using `AssertionHelper`
for about 6 months before I saw it marked as deprecated after an NUnit upgrade.
`NUnit.StaticExpect` has been made to provide the same functionality for anyone
who doesn't want to retrofit existing tests to use the newer `Assert.That`
syntax or for anyone who just plain prefers the `Expect()` syntax.

## Missing something?
`NUnit.StaticExpect` has a suite of tests to prove parity with `AssertionHelper`.
Still, the contributors are only human, so we may have missed something.
If this library _is_ missing something you're used to from `AssertionHelper`, log an
issue (: Pull requests are also welcome.