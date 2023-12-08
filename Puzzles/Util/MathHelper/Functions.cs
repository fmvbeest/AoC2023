namespace AoC2023.Util.MathHelper;

public class Functions
{
    public static long GreatestCommonDivisor(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static long LeastCommonMultiple(long a, long b)
    {
        return a / GreatestCommonDivisor(a, b) * b;
    }
}