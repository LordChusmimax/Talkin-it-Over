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
    public class PlayerMenuTest
    {
        [Category("Lab PlayerCollision")]
        [UnityTest]
        public IEnumerator BulletsKillPlayerTest()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            var players = ControllerAssigner.current.gameObject;
            PlayerScript playerScript = players.GetComponentInChildren<PlayerScript>();
            yield return null;
            Assert.IsFalse(playerScript.Dead);
            GameObject block = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs\\Solid Block"));
            block.transform.position = playerScript.transform.position;
            block.tag = "Ammunition";
            yield return null;
            Assert.IsTrue(playerScript.Dead);
        }

        [UnityTest]
        public IEnumerator OwnBulletsDontKillPlayerTest()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            var players = ControllerAssigner.current.gameObject;
            PlayerScript playerScript = players.GetComponentInChildren<PlayerScript>();
            yield return null;
            Assert.IsFalse(playerScript.Dead);
            GameObject block = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs\\Solid Block"));
            block.transform.position = playerScript.transform.position;
            block.tag = "Ammunition";
            block.layer = playerScript.gameObject.layer;
            yield return null;
            Assert.IsFalse(playerScript.Dead);
        }

        [UnityTest]
        public IEnumerator NonBulletsDontKillPlayerTest()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            var players = ControllerAssigner.current.gameObject;
            PlayerScript playerScript = players.GetComponentInChildren<PlayerScript>();
            yield return null;
            Assert.IsFalse(playerScript.Dead);
            GameObject block = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs\\Solid Block"));
            block.transform.position = playerScript.transform.position;
            yield return null;
            Assert.IsFalse(playerScript.Dead);
        }

    }
}

