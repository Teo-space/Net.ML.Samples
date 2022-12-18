global using static ExtensionsCreate;

public static class ExtensionsCreate
{
	public static T Create<T>() where T : class, new() => new T();

	public static void Run<T>() where T : class, iRunnable, new()
	{
		DateTime start = DateTime.Now;
		var o = Create<T>();
		print($"{o.GetType().Name}.Run()", ConsoleColor.Yellow);
		start = DateTime.Now;
		o.Run();
		print($"{o.GetType().Name}.Run() End. [{(DateTime.Now-start).TotalMilliseconds} m/s]", ConsoleColor.Green);
	}


	public interface iRunnable
	{
		public void Run();
	}
}