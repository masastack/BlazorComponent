using Force.DeepCloner;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorComponent;

public static class DeepClonerExtensions
{
    public static T TryDeepClone<T>(this T obj)
    {
        try
        {
            if (obj is IBrowserFile or List<IBrowserFile>)
            {
                return obj;
            }

            return obj.DeepClone();
        }
        catch (InvalidCastException)
        {
            return obj;
        }
    }
}
