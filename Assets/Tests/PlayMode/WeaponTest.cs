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

    public class WeaponTest
    {
        [Category("Lab Weapon")]
        [UnityTest]
        public IEnumerator WeaponExistTest()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            var players = ControllerAssigner.current.gameObject;
            GameObject player = players.GetComponentInChildren<PlayerScript>().gameObject;
            var playerScript = player.GetComponent<PlayerScript>();
            var gun = (GameObject)Resources.Load("Prefabs\\Gun");
            playerScript.weapon = GameObject.Instantiate(gun).GetComponent<Gun>();
            yield return null;
            Assert.NotNull(playerScript.weapon);
            yield return null;
        }


        [UnityTest]
        public IEnumerator ShootMethodShootsTest()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            var players = ControllerAssigner.current.gameObject;
            var playerScript = players.GetComponentInChildren<PlayerScript>();
            var bullets = GameObject.FindObjectsOfType<BulletScript>();
            Assert.IsEmpty(bullets);
            playerScript.weapon.Shoot();
            yield return null;
            bullets = GameObject.FindObjectsOfType<BulletScript>();
            Assert.IsNotEmpty(bullets);
        }
    }
}
