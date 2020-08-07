using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameEventsTest
    {
        [Category("Lab GameEvents")]
        [UnityTest]
        public IEnumerator PlayerTestCreated()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            Assert.IsFalse(RoundValues.paused);
            GameEvents.current.PressPause();
            Assert.IsTrue(RoundValues.paused);
            GameEvents.current.PressPause();
            Assert.IsFalse(RoundValues.paused);
        }
    }
}
