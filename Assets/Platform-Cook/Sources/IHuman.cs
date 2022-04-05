using System;

public interface IHuman : IMovement
{
    event Action Dead; 
}
