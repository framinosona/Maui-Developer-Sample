namespace Maui_Developer_Sample.Helpers;

public static class MathHelper
{
    public static float ToDegrees(float radians)
    {
        return radians * (180f / MathF.PI);
    }

    public static float ToRadians(float degrees)
    {
        return degrees * (MathF.PI / 180f);
    }
}
