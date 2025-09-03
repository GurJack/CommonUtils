using Xunit;
using FluentAssertions;
using CommonUtils.Extensions;

namespace CommonUtils.Tests.Extensions;

/// <summary>
/// Тесты для расширений объектов
/// </summary>
public class ObjectExtensionTests
{
    [Fact]
    public void IsNull_WithNullObject_ShouldReturnTrue()
    {
        // Arrange
        object? obj = null;

        // Act
        var result = obj.IsNull();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsNull_WithNonNullObject_ShouldReturnFalse()
    {
        // Arrange
        var obj = new object();

        // Act
        var result = obj.IsNull();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsNotNull_WithNullObject_ShouldReturnFalse()
    {
        // Arrange
        object? obj = null;

        // Act
        var result = obj.IsNotNull();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsNotNull_WithNonNullObject_ShouldReturnTrue()
    {
        // Arrange
        var obj = new object();

        // Act
        var result = obj.IsNotNull();

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(null, "default")]
    [InlineData("test", "test")]
    [InlineData("", "")]
    public void IfNull_ShouldReturnCorrectValue(string? input, string expected)
    {
        // Act
        var result = input.IfNull("default");

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void ToJson_ShouldSerializeObject()
    {
        // Arrange
        var obj = new { Name = "Test", Value = 123 };

        // Act
        var result = obj.ToJson();

        // Assert
        result.Should().Contain("Test");
        result.Should().Contain("123");
    }

    [Fact]
    public void CloneObject_ShouldCreateDeepCopy()
    {
        // Arrange
        var original = new TestClass { Name = "Original", Value = 42 };

        // Act
        var cloned = original.CloneObject();

        // Assert
        cloned.Should().NotBeSameAs(original);
        cloned.Name.Should().Be(original.Name);
        cloned.Value.Should().Be(original.Value);
    }

    private class TestClass
    {
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
    }
}
