using Microsoft.AspNetCore.Identity;

namespace TestSite.Domain;

/// <summary>
/// Пользователь.
/// </summary>
public class User: IdentityUser
{
	/// <summary>
	/// Никнейм.
	/// </summary>
	public string Name { get; private set; }

	public User(string name)
	{
		Name = name;
	}
}