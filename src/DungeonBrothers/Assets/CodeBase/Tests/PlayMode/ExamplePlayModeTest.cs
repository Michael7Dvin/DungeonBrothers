using System.Collections;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;

namespace CodeBase.Tests.PlayMode
{
    public class ExamplePlayModeTest
    {
        [UnityTest]
        public IEnumerator ExamplePlayModeTestWithEnumeratorPasses()
        {
            int i = 5;
        
            yield return new WaitForSeconds(2);

            i = 6;

            i.Should().Be(6);
        }
    }
}
