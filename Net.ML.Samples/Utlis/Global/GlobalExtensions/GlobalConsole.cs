global using static GlobalConsole;
class GlobalConsole
{
	static DateTime start = DateTime.Now;

	public static string Time() => $"{DateTime.Now}:{DateTime.Now.Millisecond}";


	public static void print() => Console.WriteLine("null");

	public static void print(object o = null) => print(o, ConsoleColor.White);

	public static void print(object o = null, ConsoleColor color = ConsoleColor.White)
	{
		if (o == null)
		{
			Console.WriteLine("null");
			return;
		}
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.Write("[");
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.Write(Time());
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.Write("]");
		Console.Write($"({Math.Round((DateTime.Now - start).TotalMilliseconds, 2)})        ");
		start = DateTime.Now;
		Console.ForegroundColor = color;
		Console.Write("  ");
		Console.Write(o.ToString());
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.White;

	}


	public static void print(params object[] parameters)
	{
		if (parameters == null || parameters.Length == 0) return;

		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.Write("[");
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.Write(Time());
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.Write("]");
		Console.Write("  ");
		Console.ForegroundColor = ConsoleColor.White;
		for (int i = 0; i < parameters.Length; i++)
		{
			Console.Write($"  {parameters[i]}");
		}
		Console.WriteLine();
	}

	public static void print(Exception ex)
	{
		print(ex?.Message, ConsoleColor.DarkRed);
		print(ex, ConsoleColor.DarkRed);
	}





	public static void info(object? o)
	{
		print(o?.ToStringProperties());
		print(o?.ToStringFields());
	}
	//public static IEnumerable<TSource> Print<TSource>(this IEnumerable<TSource> collection)
	public static void info<TSource>(IEnumerable<TSource> collection)
	{
		print($"info IEnumerable<{typeof(TSource).Name}>  Count: {collection.Count()}");
		foreach (var x in collection)
		{
			try
			{
				print(x?.ToStringProperties());
				print(x?.ToStringFields());
			}
			catch { }
		}
	}
}
