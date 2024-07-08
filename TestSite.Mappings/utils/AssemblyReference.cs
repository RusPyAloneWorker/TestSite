using System.Reflection;

namespace TestSite.Mappings.utils;

public static class AssemblyReference
{
	public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}