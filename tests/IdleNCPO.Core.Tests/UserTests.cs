using IdleNCPO.Core.Entities;
using IdleNCPO.Core.Enums;

namespace IdleNCPO.Core.Tests;

/// <summary>
/// Unit tests for the User entity.
/// </summary>
public class UserTests
{
  [Fact]
  public void Constructor_ShouldInitializeWithDefaultValues()
  {
    // Arrange & Act
    var user = new User();

    // Assert
    Assert.NotEqual(Guid.Empty, user.Id);
    Assert.Equal(EnumStatus.Active, user.Status);
    Assert.True(user.CreatedAt <= DateTime.UtcNow);
    Assert.Null(user.UpdatedAt);
  }

  [Fact]
  public void Email_ShouldConvertToLowerCase()
  {
    // Arrange
    var user = new User();

    // Act
    user.Email = "Test@Example.COM";

    // Assert
    Assert.Equal("test@example.com", user.Email);
  }

  [Fact]
  public void FullName_ShouldCombineFirstAndLastName()
  {
    // Arrange
    var user = new User
    {
      FirstName = "John",
      LastName = "Doe"
    };

    // Act & Assert
    Assert.Equal("John Doe", user.FullName);
  }

  [Fact]
  public void FullName_ShouldTrimWhenOnlyFirstNameProvided()
  {
    // Arrange
    var user = new User
    {
      FirstName = "John",
      LastName = string.Empty
    };

    // Act & Assert
    Assert.Equal("John", user.FullName);
  }

  [Fact]
  public void SetPasswordHash_ShouldUpdatePasswordAndTimestamp()
  {
    // Arrange
    var user = new User();
    var initialUpdatedAt = user.UpdatedAt;

    // Act
    user.SetPasswordHash("hashedPassword123");

    // Assert
    Assert.NotNull(user.UpdatedAt);
    Assert.NotEqual(initialUpdatedAt, user.UpdatedAt);
  }

  [Fact]
  public void ValidatePasswordHash_ShouldReturnTrueForMatchingHash()
  {
    // Arrange
    var user = new User();
    var passwordHash = "hashedPassword123";
    user.SetPasswordHash(passwordHash);

    // Act
    var result = user.ValidatePasswordHash(passwordHash);

    // Assert
    Assert.True(result);
  }

  [Fact]
  public void ValidatePasswordHash_ShouldReturnFalseForNonMatchingHash()
  {
    // Arrange
    var user = new User();
    user.SetPasswordHash("hashedPassword123");

    // Act
    var result = user.ValidatePasswordHash("wrongPassword");

    // Assert
    Assert.False(result);
  }

  [Fact]
  public void DisplayName_ShouldReturnFullNameWhenNotSet()
  {
    // Arrange
    var user = new User
    {
      FirstName = "John",
      LastName = "Doe"
    };

    // Act & Assert
    Assert.Equal("John Doe", user.DisplayName);
  }

  [Fact]
  public void DisplayName_ShouldReturnSetValueWhenProvided()
  {
    // Arrange
    var user = new User
    {
      FirstName = "John",
      LastName = "Doe",
      DisplayName = "Johnny"
    };

    // Act & Assert
    Assert.Equal("Johnny", user.DisplayName);
  }
}
