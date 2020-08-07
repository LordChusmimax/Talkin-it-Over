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
    public class PauseMenuTest
    {
        [Category("Lab PauseMenu")]
        [UnityTest]
        public IEnumerator PauseButtonPauses()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            GameEvents.current.PressPause();
            Assert.IsTrue(RoundValues.paused);
            yield return null;
            PauseMenuScript.current.Resume();
            Assert.IsFalse(RoundValues.paused);
            PauseMenuScript.current.Resume();
            Assert.IsFalse(RoundValues.paused);
        }
        [UnityTest]
        public IEnumerator LeaveButtonsLeaves()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            Assert.IsTrue(SceneManager.GetActiveScene().name.Equals("LabTest"));
            PauseMenuScript.current.Leave();
            yield return null;
            Assert.IsTrue(SceneManager.GetActiveScene().buildIndex == 0);
        }

    }
}
