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
    public class PlayerTest
    {
        GameObject player;

        [Category("Lab Player")]
        [UnityTest]
        public IEnumerator PlayerTestCreated()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            var players = ControllerAssigner.current.gameObject;
            player = players.GetComponentInChildren<PlayerScript>().gameObject;
            Assert.IsNotNull(player);
            yield return null;
        }
        [UnityTest]
        public IEnumerator PlayerScriptExistsTest()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            var players = ControllerAssigner.current.gameObject;
            player = players.GetComponentInChildren<PlayerScript>().gameObject;
            var playerScript = player.GetComponent<PlayerScript>();
            Assert.IsNotNull(playerScript);
            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerScriptTestController()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            var players = ControllerAssigner.current.gameObject;
            player = players.GetComponentInChildren<PlayerScript>().gameObject;
            var playerScript = player.GetComponent<PlayerScript>();
            Assert.IsNotNull(playerScript.controls.devices);
            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerScriptTestSelectController()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            var players = ControllerAssigner.current.gameObject;
            player = players.GetComponentInChildren<PlayerScript>().gameObject;
            var playerScript = player.GetComponent<PlayerScript>();
            yield return null;
            playerScript.SelectController(-1);
            Assert.AreEqual(playerScript.controls.devices, new InputDevice[] { Keyboard.current, Mouse.current });
            if (Gamepad.all.Count != 0)
            {
                playerScript.SelectController(0);
                Assert.AreEqual(playerScript.controls.devices, new InputDevice[] { Gamepad.all[0] });
            }
            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerScriptDieTest()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            var players = ControllerAssigner.current.gameObject;
            player = players.GetComponentInChildren<PlayerScript>().gameObject;
            var playerScript = player.GetComponent<PlayerScript>();
            yield return null;
            Assert.IsFalse(playerScript.Dead);
            playerScript.Die(false);
            Assert.IsTrue(playerScript.Dead);
            yield return null;
        }
    }
}
