using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using CommonUtils.Extensions;


namespace Windows.Tests.Extensions;

/// <summary>
/// Тесты для расширений коллекций
/// </summary>
[TestClass]
public class EnumerableExtensionTests
{
    [TestMethod]
    public void IsNullOrEmpty_WithNull_ShouldReturnTrue()
    {
        // Arrange
        IEnumerable<int>? collection = null;

        // Act
        var result = collection.IsNullOrEmpty();

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsNullOrEmpty_WithEmptyCollection_ShouldReturnTrue()
    {
        // Arrange
        var collection = new List<int>();

        // Act
        var result = collection.IsNullOrEmpty();

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsNullOrEmpty_WithNonEmptyCollection_ShouldReturnFalse()
    {
        // Arrange
        var collection = new List<int> { 1, 2, 3 };

        // Act
        var result = collection.IsNullOrEmpty();

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
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

    [TestMethod]
    public void IsEmpty_WithEmptyCollection_ShouldReturnTrue()
    {
        // Arrange
        var collection = new List<string>();

        // Act
        var result = collection.IsEmpty();

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsEmpty_WithNonEmptyCollection_ShouldReturnFalse()
    {
        // Arrange
        var collection = new List<string> { "test" };

        // Act
        var result = collection.IsEmpty();

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void HasItems_WithEmptyCollection_ShouldReturnFalse()
    {
        // Arrange
        var collection = new List<string>();

        // Act
        var result = collection.HasItems();

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void HasItems_WithNonEmptyCollection_ShouldReturnTrue()
    {
        // Arrange
        var collection = new List<string> { "test" };

        // Act
        var result = collection.HasItems();

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
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

    [TestMethod]
    public void Batch_ShouldGroupItemsIntoChunks()
    {
        // Arrange
        var collection = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        // Act
        var result = collection.Batch(3).ToList();

        // Assert
        result.Should().HaveCount(3);
        result[0].Should().Equal(1, 2, 3);
        result[1].Should().Equal(4, 5, 6);
        result[2].Should().Equal(7, 8, 9);
    }

    [TestMethod]
    public void SafeWhere_WithNullPredicate_ShouldReturnOriginalCollection()
    {
        // Arrange
        var collection = new List<int> { 1, 2, 3 };

        // Act
        var result = collection.SafeWhere(null!);

        // Assert
        result.Should().Equal(1, 2, 3);
    }

    [TestMethod]
    public void SafeWhere_WithValidPredicate_ShouldFilterCorrectly()
    {
        // Arrange
        var collection = new List<int> { 1, 2, 3, 4, 5 };

        // Act
        var result = collection.SafeWhere(x => x % 2 == 0);

        // Assert
        result.Should().Equal(2, 4);
    }

    private class TestObject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
