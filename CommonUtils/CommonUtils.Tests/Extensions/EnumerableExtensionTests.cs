using Xunit;
using FluentAssertions;
using CommonUtils.Extensions;

namespace CommonUtils.Tests.Extensions;

/// <summary>
/// Тесты для расширений коллекций
/// </summary>
public class EnumerableExtensionTests
{
    [Fact]
    public void IsNullOrEmpty_WithNull_ShouldReturnTrue()
    {
        // Arrange
        IEnumerable<int>? collection = null;

        // Act
        var result = collection.IsNullOrEmpty();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsNullOrEmpty_WithEmptyCollection_ShouldReturnTrue()
    {
        // Arrange
        var collection = new List<int>();

        // Act
        var result = collection.IsNullOrEmpty();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsNullOrEmpty_WithNonEmptyCollection_ShouldReturnFalse()
    {
        // Arrange
        var collection = new List<int> { 1, 2, 3 };

        // Act
        var result = collection.IsNullOrEmpty();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ForEach_ShouldExecuteActionForEachElement()
    {
        // Arrange
        var collection = new List<int> { 1, 2, 3 };
        var results = new List<int>();

        // Act
        collection.ForEach(x => results.Add(x * 2));

        // Assert
        results.Should().Equal(2, 4, 6);
    }

    [Fact]
    public void IsEmpty_WithEmptyCollection_ShouldReturnTrue()
    {
        // Arrange
        var collection = new List<string>();

        // Act
        var result = collection.IsEmpty();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsEmpty_WithNonEmptyCollection_ShouldReturnFalse()
    {
        // Arrange
        var collection = new List<string> { "test" };

        // Act
        var result = collection.IsEmpty();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void HasItems_WithEmptyCollection_ShouldReturnFalse()
    {
        // Arrange
        var collection = new List<string>();

        // Act
        var result = collection.HasItems();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void HasItems_WithNonEmptyCollection_ShouldReturnTrue()
    {
        // Arrange
        var collection = new List<string> { "test" };

        // Act
        var result = collection.HasItems();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void DistinctBy_ShouldReturnDistinctElementsBySelector()
    {
        // Arrange
        var collection = new List<TestObject>
        {
            new() { Id = 1, Name = "Test1" },
            new() { Id = 2, Name = "Test2" },
            new() { Id = 1, Name = "Test3" }
        };

        // Act
        var result = collection.DistinctBy(x => x.Id).ToList();

        // Assert
        result.Should().HaveCount(2);
        result.Select(x => x.Id).Should().Equal(1, 2);
    }

    private class TestObject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
