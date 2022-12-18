public static class ExtentionsPrint
{
	public static T Print<T>(this T o, Func<T, string> selector)
	{
		print(selector(o));
		return o;
	}
	public static T Print<T>(this T o, string message)
	{
		print(message);
		return o;
	}

	public static T Print<T>(this T o) where T : class
	{
		//print(o?.ToStringMembers());
		print(o?.ToStringProperties());
		print(o?.ToStringFields());
		return o;
	}


	public static List<TSource> Print<TSource>(this List<TSource> collection)
	{
		print($"Print IEnumerable<{typeof(TSource).Name}>  Count: {collection.Count()}");
		foreach (var x in collection)
		{
			try
			{
				//print(x?.ToStringMembers());
				print(x?.ToStringProperties());
				print(x?.ToStringFields());
			}
			catch { }
		}
		return collection;
	}

	public static IEnumerable<TSource> Print<TSource>(this IEnumerable<TSource> collection)
	{
		print($"Print IEnumerable<{typeof(TSource).Name}>  Count: {collection.Count()}");
		foreach (var x in collection)
		{
			try
			{
				//print(x?.ToStringMembers());
				print(x?.ToStringProperties());
				print(x?.ToStringFields());
			}
			catch { }
		}
		return collection;
	}


}
