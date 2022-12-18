global using static ExtensionsStackTrace;
using System.Diagnostics;

class ExtensionsStackTrace
{
	public static IEnumerable<MethodInfo> TraceMethods() =>
		new StackTrace()
			.GetFrames()
			.Where(f => f != null)
			.Select(f => f.GetMethod())
			.Where(m => m != null)
			.OfType<MethodInfo>()
			.Skip(1);

	//print($"[Test] {MethodBase.GetCurrentMethod().DeclaringType.Namespace.Split('.').Last()}.{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name}", ConsoleColor.Magenta);

	public static void PrintMethodInfo(string message = "", ConsoleColor color = ConsoleColor.DarkGray)
	{
		try
		{
			var m = TraceMethods().Skip(1).FirstOrDefault();

			print($"[{(string.IsNullOrEmpty(message) ? "Method" : message)}] {m.DeclaringType.Namespace.Split('.').Last()}.{m.DeclaringType.Name}.{m.Name}", ConsoleColor.Magenta);
		}
		catch { }
	}
}