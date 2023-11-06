using Project.CodeBase.Gameplay.Rooms;

namespace Project.CodeBase.Gameplay.Spawner.Rooms
{
    public interface IRoomSpawner
    {
        public Room CreateWithLeftExit();
        public Room CreateWithRightExit();
        public Room CreateWithUpExit();
        public Room CreateWithDownExit();
    }
}