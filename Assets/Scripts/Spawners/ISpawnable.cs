using System;

public interface ISpawnable<out T>
{
    event Action <T> Destroyer;
}
