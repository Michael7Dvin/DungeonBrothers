using UnityEngine;

namespace CodeBase
{
    public class SOME_SERVICE_FOR_UNIT_TEST_DEBUG : I_SOME_SERVICE_FOR_UNIT_TEST_DEBUG 
    {
        public void DoSomething() => 
            Debug.Log("something");
    }
}