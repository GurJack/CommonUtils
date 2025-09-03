using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using CommonUtils.Extensions;

namespace Windows.Tests.Extensions;

/// <summary>
/// Тесты для расширений строк
/// </summary>
[TestClass]
public class StringExtensionTests
{
    [TestMethod]
    [DataRow("hello", "Hello")]
    [DataRow("WORLD", "World")]
    [DataRow("", "")]
    [DataRow("a", "A")]
    [DataRow("hELLO wORLD", "Hello world")]
    public void ToTitleCase_ShouldConvertCorrectly(string input, string expected)
    {
        // Act
        var result = input.ToTitleCase();

        // Assert
        result.Should().Be(expected);
    }

    [TestMethod]
    [DataRow("", true)]
    [DataRow(" ", false)]
    [DataRow("test", false)]
    public void IsNullOrEmpty_ShouldReturnCorrectResult(string input, bool expected)
    {
        // Act
        var result = input.IsNullOrEmpty();

        // Assert
        result.Should().Be(expected);
    }

    [TestMethod]
    public void IsNullOrEmpty_WithNull_ShouldReturnTrue()
    {
        // Arrange
        string? input = null;

        // Act
        var result = input.IsNullOrEmpty();

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    [DataRow("", true)]
    [DataRow(" ", true)]
    [DataRow("  ", true)]
    [DataRow("test", false)]
    public void IsNullOrWhiteSpace_ShouldReturnCorrectResult(string input, bool expected)
    {
        // Act
        var result = input.IsNullOrWhiteSpace();

        // Assert
        result.Should().Be(expected);
    }

    [TestMethod]
    public void IsNullOrWhiteSpace_WithNull_ShouldReturnTrue()
    {
        // Arrange
        string? input = null;

        // Act
        var result = input.IsNullOrWhiteSpace();

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    [DataRow("test", 2, "te")]
    [DataRow("test", 10, "test")]
    [DataRow("", 5, "")]
    [DataRow("test", 0, "")]
    public void Left_ShouldReturnCorrectSubstring(string input, int length, string expected)
    {
        // Act
        var result = input.Left(length);

        // Assert
        result.Should().Be(expected);
    }

    [TestMethod]
    [DataRow("test", 2, "st")]
    [DataRow("test", 10, "test")]
    [DataRow("", 5, "")]
    [DataRow("test", 0, "")]
    public void Right_ShouldReturnCorrectSubstring(string input, int length, string expected)
    {
        // Act
        var result = input.Right(length);

        // Assert
        result.Should().Be(expected);
    }

    [TestMethod]
    public void ToMd5_ShouldReturnCorrectHash()
    {
        // Arrange
        const string input = "test";
        const string expectedHash = "098f6bcd4621d373cade4e832627b4f6";

        // Act
        var result = input.ToMd5();

        // Assert
        result.Should().Be(expectedHash);
    }

    [TestMethod]
    [DataRow("123", true)]
    [DataRow("abc", false)]
    [DataRow("12.34", false)]
    [DataRow("", false)]
    [DataRow("-123", true)]
    public void IsNumeric_ShouldReturnCorrectResult(string input, bool expected)
    {
        // Act
        var result = input.IsNumeric();

        // Assert
        result.Should().Be(expected);
    }

    [TestMethod]
    [DataRow("test@example.com", true)]
    [DataRow("invalid-email", false)]
    [DataRow("", false)]
    [DataRow("test.email@domain.co.uk", true)]
    public void IsValidEmail_ShouldReturnCorrectResult(string input, bool expected)
    {
        // Act
        var result = input.IsValidEmail();

        // Assert
        result.Should().Be(expected);
    }

    [TestMethod]
    public void ToBase64_ShouldEncodeCorrectly()
    {
        // Arrange
        const string input = "Hello World";
        const string expected = "SGVsbG8gV29ybGQ=";

        // Act
        var result = input.ToBase64();

        // Assert
        result.Should().Be(expected);
    }

    [TestMethod]
    public void FromBase64_ShouldDecodeCorrectly()
    {
        // Arrange
        const string input = "SGVsbG8gV29ybGQ=";
        const string expected = "Hello World";

        // Act
        var result = input.FromBase64();

        // Assert
        result.Should().Be(expected);
    }
}
