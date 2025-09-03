using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using CommonUtils;
using CommonUtils.Extensions;

namespace Windows.Tests;

/// <summary>
/// Основные интеграционные тесты для CommonUtils
/// </summary>
[TestClass]
public class MainIntegrationTests
{
    [TestMethod]
    public void CommonUtils_ShouldLoadCorrectly()
    {
        // Arrange & Act
        var applicationName = GlobalConstant.ApplicationName;
        var version = GlobalConstant.Version;

        // Assert
        applicationName.Should().NotBeNullOrEmpty();
        version.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void StringExtensions_ShouldWorkCorrectly()
    {
        // Arrange
        const string testString = "hello world";

        // Act
        var titleCase = testString.ToTitleCase();
        var md5Hash = testString.ToMd5();

        // Assert
        titleCase.Should().Be("Hello world");
        md5Hash.Should().NotBeNullOrEmpty();
        md5Hash.Length.Should().Be(32); // MD5 всегда 32 символа
    }

    [TestMethod]
    public void RandomGenerator_ShouldGenerateValidData()
    {
        // Act
        var randomString = RandomGenerator.GenerateString(10);
        var randomInt = RandomGenerator.GenerateInt(1, 100);
        var randomGuid = RandomGenerator.GenerateGuid();

        // Assert
        randomString.Should().HaveLength(10);
        randomInt.Should().BeInRange(1, 100);
        randomGuid.Should().NotBe(Guid.Empty);
    }

    [TestMethod]
    public void EnumerableExtensions_ShouldWorkCorrectly()
    {
        // Arrange
        var collection = new List<int> { 1, 2, 3, 4, 5 };

        // Act
        var evenNumbers = collection.Where(x => x % 2 == 0).ToList();
        var hasItems = collection.HasItems();
        var isEmpty = new List<int>().IsEmpty();

        // Assert
        evenNumbers.Should().Equal(2, 4);
        hasItems.Should().BeTrue();
        isEmpty.Should().BeTrue();
    }

    [TestMethod]
    public void ObjectExtensions_ShouldWorkCorrectly()
    {
        // Arrange
        var testObject = new { Name = "Test", Value = 123 };
        string? nullString = null;

        // Act
        var json = testObject.ToJson();
        var safeValue = nullString.IfNull("default");
        var isNull = nullString.IsNull();

        // Assert
        json.Should().Contain("Test");
        json.Should().Contain("123");
        safeValue.Should().Be("default");
        isNull.Should().BeTrue();
    }
}
