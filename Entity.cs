using System;

public interface Entity
{
    public int Value { get; }// unique
}

public interface EntityCollection
{
    public void Add(Entity entity);
    public Entity RemoveMaxValue();
}