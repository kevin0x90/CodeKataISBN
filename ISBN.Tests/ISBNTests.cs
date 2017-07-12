using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISBN.Tests
{
    [TestFixture]
    public class ISBNTests
    {
        [Test]
        [TestCase("", Description = "An empty ISBN")]
        [TestCase("9A8-0-1Y-1495O5-1", Description = "ISBN with invalid characters")]
        [TestCase("978-0-13-149505-1", Description = "ISBN with invalid check digit")]
        [TestCase("978 0-262 13472-9", Description = "ISBN with mixed space and dash seperators")]
        public void ISBN_GivenAnInvalidISBN_Then_ItShouldReturnFalse(string invalidISBN)
        {
            // Arrange
            var sut = new ISBNChecker();

            // Act
            var isValidISBN = sut.IsValidISBN(invalidISBN);

            // Assert
            Assert.That<bool>(isValidISBN, Is.False);
        }

        [Test]
        [TestCase("9780470059029", Description = "ISBN without seperators")]
        [TestCase("978 0 471 48648 0", Description = "ISBN with space as seperator")]
        [TestCase("978-0596809485", Description = "ISBN with dash as seperator")]
        [TestCase("978-0-13-149505-0", Description = "ISBN with multiple dash seperators")]
        [TestCase("978-0-262-13472-9", Description = "ISBN with multiple dash seperators")]
        public void ISBN_GivenValidISBN_Then_ItShouldReturnTrue(string isbn)
        {
            // Arrange
            var sut = new ISBNChecker();

            // Act
            var isValidISBN = sut.IsValidISBN(isbn);

            // Assert
            Assert.That<bool>(isValidISBN, Is.True);
        }
    }
}
