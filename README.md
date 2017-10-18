# NUnit.StaticExpect
Provides a mechanism for a static import of NUnit Expect() syntax

## Usage
1. Install the nuget package (`install-package nunit.staticexpect`)
2. Import the static methods: add the following using statement to your test class file:
```
using static NUnit.StaticExpect.Expectations;
```
3. Use the Expect() syntax, like you might have done when inheriting from `AssertionHelper`:
```
[Test]
public void Expect_True_IsTrue()
{
  // very contrived example
  Expect(true, Is.True);
}
```

## Missing something?
If this library _is_ missing something you're used to from `AssertionHelper`, log an
issue (: