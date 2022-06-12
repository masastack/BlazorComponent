namespace BlazorComponent;

public class AbstractComponentActivator : IComponentActivator
{
    private readonly IAbstractComponentTypeMapper _typeMapper;

    public AbstractComponentActivator(IAbstractComponentTypeMapper typeMapper)
    {
        _typeMapper = typeMapper;
    }

    public IComponent CreateInstance([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type componentType)
    {
        if (!typeof(IComponent).IsAssignableFrom(componentType))
        {
            throw new ArgumentException($"The type {componentType.FullName} does not implement {nameof(IComponent)}.", nameof(componentType));
        }

        var type = _typeMapper.Map(componentType);
        return (IComponent)Activator.CreateInstance(type)!;
    }
}