global using static ExtensionsRandom;

internal class ExtensionsRandom
{
	static Random random = new Random();
	public static int Random(int Start, int End) => random.Next(Start, End);
	public static long Random(long Start, long End) => random.NextInt64(Start, End);
}