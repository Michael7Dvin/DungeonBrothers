using UnityEngine;

namespace CodeBase.Tests
{
    public class Create
    {
        public static HEALTH Health()
        {
            HEALTH health = new GameObject().AddComponent<HEALTH>();
            return health;
        }
    }
}