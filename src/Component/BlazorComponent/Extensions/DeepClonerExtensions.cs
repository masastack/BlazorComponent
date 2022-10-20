using Force.DeepCloner;

namespace BlazorComponent;

public static class DeepClonerExtensions
{
    public static T TryDeepClone<T>(this T obj)
    {
        try
        {
            return obj.DeepClone();
        }
        catch (InvalidCastException)
        {
            return obj;
        }
    }
}
