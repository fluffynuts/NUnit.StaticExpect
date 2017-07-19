using NUnit.Framework;
using static NUnit.StaticExpect.Expectations;

namespace NUnit.StaticExpect.TestPackage
{
    [TestFixture]
    public class TestUsage
    {
        [Test]
        public void TestExpect()
        {
            // Arrange

            // Pre-Assert

            // Act
            Expect(true, Is.True);

            // Assert
        }
    }
}
