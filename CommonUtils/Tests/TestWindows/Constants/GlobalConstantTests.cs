using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using CommonUtils;

namespace Windows.Tests.Constants;

/// <summary>
/// Тесты для глобальных констант
/// </summary>
[TestClass]
public class GlobalConstantTests
{
    [TestMethod]
    public void ApplicationName_ShouldNotBeNullOrEmpty()
    {
        // Act & Assert
        GlobalConstant.ApplicationName.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void Version_ShouldNotBeNullOrEmpty()
    {
        // Act & Assert
        GlobalConstant.Version.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void DefaultConnectionString_ShouldNotBeNullOrEmpty()
    {
        // Act & Assert
        GlobalConstant.DefaultConnectionString.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void DateTimeFormat_ShouldBeValidFormat()
    {
        // Arrange
        var testDate = new DateTime(2023, 12, 25, 14, 30, 45);

        // Act
        var result = testDate.ToString(GlobalConstant.DateTimeFormat);

        // Assert
        result.Should().NotBeNullOrEmpty();
        // Проверяем, что формат применился корректно
        DateTime.TryParseExact(result, GlobalConstant.DateTimeFormat, null,
            System.Globalization.DateTimeStyles.None, out _).Should().BeTrue();
    }

    [TestMethod]
    public void DateFormat_ShouldBeValidFormat()
    {
        // Arrange
        var testDate = new DateTime(2023, 12, 25);

        // Act
        var result = testDate.ToString(GlobalConstant.DateFormat);

        // Assert
        result.Should().NotBeNullOrEmpty();
        DateTime.TryParseExact(result, GlobalConstant.DateFormat, null,
            System.Globalization.DateTimeStyles.None, out _).Should().BeTrue();
    }

    [TestMethod]
    public void TimeFormat_ShouldBeValidFormat()
    {
        // Arrange
        var testTime = new DateTime(1, 1, 1, 14, 30, 45);

        // Act
        var result = testTime.ToString(GlobalConstant.TimeFormat);

        // Assert
        result.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void DefaultEncoding_ShouldNotBeNull()
    {
        // Act & Assert
        GlobalConstant.DefaultEncoding.Should().NotBeNull();
    }

    [TestMethod]
    public void MaxRetryAttempts_ShouldBePositive()
    {
        // Act & Assert
        GlobalConstant.MaxRetryAttempts.Should().BePositive();
    }

    [TestMethod]
    public void DefaultTimeout_ShouldBePositive()
    {
        // Act & Assert
        GlobalConstant.DefaultTimeout.Should().BePositive();
    }

    [TestMethod]
    public void TempDirectory_ShouldNotBeNullOrEmpty()
    {
        // Act & Assert
        GlobalConstant.TempDirectory.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void LogDirectory_ShouldNotBeNullOrEmpty()
    {
        // Act & Assert
        GlobalConstant.LogDirectory.Should().NotBeNullOrEmpty();
    }
}
