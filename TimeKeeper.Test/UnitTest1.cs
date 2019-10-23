using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        public string message;

        [OneTimeSetUp]
        public void Setup()
        {
            //Arrange
            message = "Hello test!";
        }

        [Test]
        public void Test1()
        {
            //Act
            string msg = "Hello test!";

            //Assert
            Assert.AreEqual(msg, message);
        }

        [TestCase(1, 2, 3)]
        [TestCase(3, 4, 7)]
        [TestCase(10, 11, 21)]
        public void TestCases(int x, int y, int suma)
        {
            //Act
            int result = x + y;

            //Assert
            Assert.AreEqual(result, suma);
        }

        [Test]
        public void TestSum(
            [Values(1, 2, 3)] int x,
            [Values(4, 5)] int y)
        {
            int suma = calc(x, y);

            Assert.AreEqual(suma, x + y);
        }

        int calc(int x, int y)
        {
            return x + y;
        }
    }
}