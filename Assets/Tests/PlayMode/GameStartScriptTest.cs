
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameStartScriptTest
    {
        [Category("Lab StartingRoundValues")]
        [UnityTest]
        public IEnumerator PlayerTestCreated()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            Assert.AreEqual(Time.timeScale, 1);
            Time.timeScale = 0;
            Assert.AreEqual(Time.timeScale, 0);
            SceneManager.LoadScene("LabTest");
            yield return null;
            Assert.AreEqual(Time.timeScale, 1);
        }
    }
}
