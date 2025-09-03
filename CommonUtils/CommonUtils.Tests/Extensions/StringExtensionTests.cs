using Xunit;
using FluentAssertions;
using CommonUtils.Extensions;

namespace CommonUtils.Tests.Extensions;

/// <summary>
/// Тесты для расширений строк
/// </summary>
public class StringExtensionTests
{
    [Theory]
    [InlineData("hello", "Hello")]
    [InlineData("WORLD", "World")]
    [InlineData("", "")]
    [InlineData("a", "A")]
    [InlineData("hELLO wORLD", "Hello world")]
    public void ToTitleCase_ShouldConvertCorrectly(string input, string expected)
    {
        // Act
        var result = input.ToTitleCase();

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("", true)]
    [InlineData(" ", false)]
    [InlineData("test", false)]
    [InlineData(null, true)]
    public void IsNullOrEmpty_ShouldReturnCorrectResult(string? input, bool expected)
    {
        // Act
        var result = input.IsNullOrEmpty();

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("", true)]
    [InlineData(" ", true)]
    [InlineData("  ", true)]
    [InlineData("test", false)]
    [InlineData(null, true)]
    public void IsNullOrWhiteSpace_ShouldReturnCorrectResult(string? input, bool expected)
    {
        // Act
        var result = input.IsNullOrWhiteSpace();

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("test", 2, "te")]
    [InlineData("test", 10, "test")]
    [InlineData("", 5, "")]
    [InlineData("test", 0, "")]
    public void Left_ShouldReturnCorrectSubstring(string input, int length, string expected)
    {
        // Act
        var result = input.Left(length);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("test", 2, "st")]
    [InlineData("test", 10, "test")]
    [InlineData("", 5, "")]
    [InlineData("test", 0, "")]
    public void Right_ShouldReturnCorrectSubstring(string input, int length, string expected)
    {
        // Act
        var result = input.Right(length);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
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

    [Theory]
    [InlineData("123", true)]
    [InlineData("abc", false)]
    [InlineData("12.34", false)]
    [InlineData("", false)]
    [InlineData("-123", true)]
    public void IsNumeric_ShouldReturnCorrectResult(string input, bool expected)
    {
        // Act
        var result = input.IsNumeric();

        // Assert
        result.Should().Be(expected);
    }
}
