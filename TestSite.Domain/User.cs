using Microsoft.AspNetCore.Identity;

namespace TestSite.Domain;

public class User: IdentityUser
{
	public string Name { get; private set; }

	public User(string name)
	{
		Name = name;
	}
}