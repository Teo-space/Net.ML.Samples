public static class ExtentionsPipe
{
	public static TSource PipeTo<TSource>(this TSource source, Action<TSource> func)
	{
		func(source);
		return source;
	}

	public static TResult PipeTo<TSource, TResult>

		(this TSource source, Func<TSource, TResult> func) => func(source);



	public static IEnumerable<TSource> PipeFirst<TSource, TResult>

		(this IEnumerable<TSource> collection, Func<TSource, TResult> func)
	{
		func(collection.First());
		return collection;
	}
	public static IEnumerable<TSource> PipeFirstOrDefault<TSource, TResult>

		(this IEnumerable<TSource> collection, Func<TSource, TResult> func)
	{
		func(collection.FirstOrDefault());
		return collection;
	}




	public static IEnumerable<TSource> PipeSingle<TSource, TResult>

		(this IEnumerable<TSource> collection, Func<TSource, TResult> func)
	{
		func(collection.Single());
		return collection;
	}

	public static IEnumerable<TSource> PipeSingleOrDefault<TSource, TResult>

		(this IEnumerable<TSource> collection, Func<TSource, TResult> func)
	{
		func(collection.SingleOrDefault());
		return collection;
	}





	public static T Do<T>(this T o, Action<T> action)
	{
		if (o != null)
		{
			action(o);
		}

		return o;
	}



}
