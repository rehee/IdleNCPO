using IdleNCPO.Core.Enums;

namespace IdleNCPO.Core.Entities;

/// <summary>
/// Represents a user in the system.
/// </summary>
public class User : BaseEntity
{
  private string email;
  private string passwordHash;
  protected string displayName;

  /// <summary>
  /// Gets or sets the username.
  /// </summary>
  public string Username { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the email address.
  /// </summary>
  public string Email
  {
    get => email;
    set => email = value?.ToLowerInvariant() ?? string.Empty;
  }

  /// <summary>
  /// Gets or sets the first name.
  /// </summary>
  public string FirstName { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the last name.
  /// </summary>
  public string LastName { get; set; } = string.Empty;

  /// <summary>
  /// Gets the full name of the user.
  /// </summary>
  public string FullName => $"{FirstName} {LastName}".Trim();

  /// <summary>
  /// Initializes a new instance of the <see cref="User"/> class.
  /// </summary>
  public User()
  {
    email = string.Empty;
    passwordHash = string.Empty;
    displayName = string.Empty;
  }

  /// <summary>
  /// Sets the password hash for the user.
  /// </summary>
  /// <param name="hash">The password hash.</param>
  public void SetPasswordHash(string hash)
  {
    passwordHash = hash;
    UpdatedAt = DateTime.UtcNow;
  }

  /// <summary>
  /// Validates if the provided hash matches the stored password hash.
  /// </summary>
  /// <param name="hash">The hash to validate.</param>
  /// <returns>True if the hash matches; otherwise, false.</returns>
  public bool ValidatePasswordHash(string hash)
  {
    return !string.IsNullOrEmpty(passwordHash) &&
           string.Equals(passwordHash, hash, StringComparison.Ordinal);
  }

  /// <summary>
  /// Gets or sets the display name.
  /// </summary>
  public string DisplayName
  {
    get => string.IsNullOrEmpty(displayName) ? FullName : displayName;
    set => displayName = value;
  }
}
