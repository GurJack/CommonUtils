using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using CommonUtils;

namespace Windows.Tests.Helpers;

/// <summary>
/// Тесты для генератора случайных данных
/// </summary>
[TestClass]
public class RandomGeneratorTests
{
    [TestMethod]
    [DataRow(5)]
    [DataRow(10)]
    [DataRow(20)]
    public void GenerateString_ShouldReturnStringOfCorrectLength(int length)
    {
        // Act
        var result = RandomGenerator.GenerateString(length);

        // Assert
        result.Should().HaveLength(length);
        result.Should().MatchRegex("^[a-zA-Z0-9]+$");
    }

    [TestMethod]
    public void GenerateString_WithZeroLength_ShouldReturnEmptyString()
    {
        // Act
        var result = RandomGenerator.GenerateString(0);

        // Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    [DataRow(1, 10)]
    [DataRow(100, 200)]
    [DataRow(-10, 10)]
    public void GenerateInt_ShouldReturnValueInRange(int min, int max)
    {
        // Act
        var result = RandomGenerator.GenerateInt(min, max);

        // Assert
        result.Should().BeInRange(min, max);
    }

    [TestMethod]
    [DataRow(1.0, 10.0)]
    [DataRow(-5.5, 5.5)]
    public void GenerateDouble_ShouldReturnValueInRange(double min, double max)
    {
        // Act
        var result = RandomGenerator.GenerateDouble(min, max);

        // Assert
        result.Should().BeInRange(min, max);
    }

    [TestMethod]
    public void GenerateBool_ShouldReturnBooleanValue()
    {
        // Act
        var results = new List<bool>();
        for (int i = 0; i < 100; i++)
        {
            results.Add(RandomGenerator.GenerateBool());
        }

        // Assert
        // Проверяем, что есть и true, и false (статистически должно быть так)
        results.Should().Contain(true);
        results.Should().Contain(false);
    }

    [TestMethod]
    public void GenerateGuid_ShouldReturnUniqueGuids()
    {
        // Act
        var guid1 = RandomGenerator.GenerateGuid();
        var guid2 = RandomGenerator.GenerateGuid();

        // Assert
        guid1.Should().NotBe(guid2);
        guid1.Should().NotBe(Guid.Empty);
        guid2.Should().NotBe(Guid.Empty);
    }

    [TestMethod]
    public void GenerateDateTime_ShouldReturnDateTimeInRange()
    {
        // Arrange
        var minDate = new DateTime(2020, 1, 1);
        var maxDate = new DateTime(2025, 12, 31);

        // Act
        var result = RandomGenerator.GenerateDateTime(minDate, maxDate);

        // Assert
        result.Should().BeOnOrAfter(minDate);
        result.Should().BeOnOrBefore(maxDate);
    }

    [TestMethod]
    public void GenerateEmail_ShouldReturnValidEmailFormat()
    {
        // Act
        var result = RandomGenerator.GenerateEmail();

        // Assert
        result.Should().Contain("@");
        result.Should().MatchRegex(@"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,}$");
    }

    [TestMethod]
    public void GeneratePassword_ShouldReturnPasswordWithCorrectLength()
    {
        // Arrange
        const int length = 12;

        // Act
        var result = RandomGenerator.GeneratePassword(length);

        // Assert
        result.Should().HaveLength(length);
        // Пароль должен содержать различные типы символов
        result.Should().MatchRegex(@"[A-Z]"); // Большие буквы
        result.Should().MatchRegex(@"[a-z]"); // Маленькие буквы
        result.Should().MatchRegex(@"[0-9]"); // Цифры
    }

    [TestMethod]
    public void GenerateAlphaString_ShouldContainOnlyLetters()
    {
        // Act
        var result = RandomGenerator.GenerateAlphaString(10);

        // Assert
        result.Should().HaveLength(10);
        result.Should().MatchRegex("^[a-zA-Z]+$");
    }

    [TestMethod]
    public void GenerateNumericString_ShouldContainOnlyNumbers()
    {
        // Act
        var result = RandomGenerator.GenerateNumericString(8);

        // Assert
        result.Should().HaveLength(8);
        result.Should().MatchRegex("^[0-9]+$");
    }
}
