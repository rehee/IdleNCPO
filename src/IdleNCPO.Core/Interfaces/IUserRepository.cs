using IdleNCPO.Core.Entities;

namespace IdleNCPO.Core.Interfaces;

/// <summary>
/// Interface for user repository operations.
/// </summary>
public interface IUserRepository
{
  /// <summary>
  /// Gets a user by their unique identifier.
  /// </summary>
  /// <param name="id">The user's unique identifier.</param>
  /// <param name="cancellationToken">Cancellation token.</param>
  /// <returns>The user if found; otherwise, null.</returns>
  Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

  /// <summary>
  /// Gets a user by their username.
  /// </summary>
  /// <param name="username">The username.</param>
  /// <param name="cancellationToken">Cancellation token.</param>
  /// <returns>The user if found; otherwise, null.</returns>
  Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

  /// <summary>
  /// Gets a user by their email address.
  /// </summary>
  /// <param name="email">The email address.</param>
  /// <param name="cancellationToken">Cancellation token.</param>
  /// <returns>The user if found; otherwise, null.</returns>
  Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

  /// <summary>
  /// Creates a new user.
  /// </summary>
  /// <param name="user">The user to create.</param>
  /// <param name="cancellationToken">Cancellation token.</param>
  /// <returns>The created user.</returns>
  Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);

  /// <summary>
  /// Updates an existing user.
  /// </summary>
  /// <param name="user">The user to update.</param>
  /// <param name="cancellationToken">Cancellation token.</param>
  /// <returns>The updated user.</returns>
  Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default);

  /// <summary>
  /// Deletes a user by their unique identifier.
  /// </summary>
  /// <param name="id">The user's unique identifier.</param>
  /// <param name="cancellationToken">Cancellation token.</param>
  /// <returns>True if deleted; otherwise, false.</returns>
  Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
