internal static class ExtensionsLinq2Db
{
	public static BulkCopyRowsCopied CopyTo<T>(this ITable<T> table, params T[] source) where T : class
	{
		return table.BulkCopy(source);
	}

}