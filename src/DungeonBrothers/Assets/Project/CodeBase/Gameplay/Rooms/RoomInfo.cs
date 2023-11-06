namespace Project.CodeBase.Gameplay.Rooms
{
    public class RoomInfo
    {
        public readonly bool IsHaveTopExit;
        public readonly bool IsHaveDownExit;
        public readonly bool IsHaveLeftExit;
        public readonly bool IsHaveRightExit;

        public bool IsCleaned;

        public RoomInfo(bool isHaveTopExit, 
            bool isHaveDownExit,
            bool isHaveLeftExit,
            bool isHaveRightExit)
        {
            IsHaveTopExit = isHaveTopExit;
            IsHaveDownExit = isHaveDownExit;
            IsHaveLeftExit = isHaveLeftExit;
            IsHaveRightExit = isHaveRightExit;
        }

        public bool IsHaveAnyExit() => 
            IsHaveDownExit || IsHaveTopExit || IsHaveLeftExit || IsHaveRightExit;
    }
}