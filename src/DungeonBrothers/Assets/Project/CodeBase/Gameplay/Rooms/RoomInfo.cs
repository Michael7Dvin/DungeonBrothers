namespace Project.CodeBase.Gameplay.Rooms
{
    public class RoomInfo
    {
        public bool IsHaveTopExit;
        public bool IsHaveDownExit;
        public bool IsHaveLeftExit;
        public bool IsHaveRightExit;

        public bool IsCleaned;

        public bool IsHaveAnyExit() => 
            IsHaveDownExit || IsHaveTopExit || IsHaveLeftExit || IsHaveRightExit;
    }
}