using System;

namespace Project.CodeBase.Gameplay.Rooms.Doors
{
    public interface IDoorSelector
    {
        IObservable<Door> IsOpened { get; }
        void Initialize();
        void ClearUp();
    }
}