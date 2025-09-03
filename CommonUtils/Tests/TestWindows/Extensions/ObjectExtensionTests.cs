using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using CommonUtils.Extensions;

namespace Windows.Tests.Extensions;

/// <summary>
/// Тесты для расширений объектов
/// </summary>
[TestClass]
public class ObjectExtensionTests
{
    [TestMethod]
    public void IsNull_WithNullObject_ShouldReturnTrue()
    {
        // Arrange
        object? obj = null;

        // Act
        var result = obj.IsNull();

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsNull_WithNonNullObject_ShouldReturnFalse()
    {
        // Arrange
        var obj = new object();

        // Act
        var result = obj.IsNull();

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void IsNotNull_WithNullObject_ShouldReturnFalse()
    {
        // Arrange
        object? obj = null;

        // Act
        var result = obj.IsNotNull();

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void IsNotNull_WithNonNullObject_ShouldReturnTrue()
    {
        // Arrange
        var obj = new object();

        // Act
        var result = obj.IsNotNull();

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    [DataRow("test", "test")]
    [DataRow("", "")]
    public void IfNull_WithNonNullValue_ShouldReturnOriginalValue(string input, string expected)
    {
        // Act
        var result = input.IfNull("default");

        // Assert
        result.Should().Be(expected);
    }

    [TestMethod]
    public void IfNull_WithNullValue_ShouldReturnDefaultValue()
    {
        // Arrange
        string? input = null;

        // Act
        var result = input.IfNull("default");

        // Assert
        result.Should().Be("default");
    }

    [TestMethod]
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

    [TestMethod]
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

    [TestMethod]
    public void SafeCast_WithValidCast_ShouldReturnCastedObject()
    {
        // Arrange
        object obj = "test string";

        // Act
        var result = obj.SafeCast<string>();

        // Assert
        result.Should().Be("test string");
    }

    [TestMethod]
    public void SafeCast_WithInvalidCast_ShouldReturnDefault()
    {
        // Arrange
        object obj = "test string";

        // Act
        var result = obj.SafeCast<int>();

        // Assert
        result.Should().Be(0);
    }

    [TestMethod]
    public void GetPropertyValue_WithExistingProperty_ShouldReturnValue()
    {
        // Arrange
        var obj = new TestClass { Name = "TestName", Value = 123 };

        // Act
        var result = obj.GetPropertyValue("Name");

        // Assert
        result.Should().Be("TestName");
    }

    [TestMethod]
    public void GetPropertyValue_WithNonExistingProperty_ShouldReturnNull()
    {
        // Arrange
        var obj = new TestClass { Name = "TestName", Value = 123 };

        // Act
        var result = obj.GetPropertyValue("NonExisting");

        // Assert
        result.Should().BeNull();
    }

    private class TestClass
    {
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
    }
}
