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

    public class PlayerAnimatorTest
    {
        [Category("Lab PlayerAnimator")]
        [UnityTest]
        public IEnumerator PlayerAnimatorConstructorTest()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            var players = ControllerAssigner.current.gameObject;
            PlayerScript playerScript = players.GetComponentInChildren<PlayerScript>();
            yield return null;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.AddComponent<Animator>();
            GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            PlayerAnimator playerAnimator = new PlayerAnimator(Camera.main, playerScript, cube, cube2);
            Assert.IsNotNull(playerAnimator);
            Assert.AreEqual(playerAnimator.Animator,cube.GetComponent<Animator>());
            Assert.AreEqual(playerAnimator.Camera,Camera.main);
            Assert.AreEqual(playerAnimator.Controls,playerScript.controls);
            Assert.AreEqual(playerAnimator.Rb,playerScript.RigidBody);
            Assert.AreEqual(playerAnimator.RightArm, cube2.transform);
        }

        [UnityTest]
        public IEnumerator PlayerAnimatorSelectAimAnimatorConstructorTest()
        {
            SceneManager.LoadScene("LabTest");
            yield return null;
            var players = ControllerAssigner.current.gameObject;
            PlayerScript playerScript = players.GetComponentInChildren<PlayerScript>();
            yield return null;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.AddComponent<Animator>();
            GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            PlayerAnimator playerAnimator = new PlayerAnimator(Camera.main, playerScript, cube, cube2);
            playerAnimator.SelectAimAnimator(true);
            Assert.IsTrue(playerAnimator.AimAnimator.GetType()==typeof(MouseAimAnimator));
            Assert.IsFalse(playerAnimator.AimAnimator.GetType() == typeof(GamepadAimAnimator));
            playerAnimator.SelectAimAnimator(false);
            Assert.IsFalse(playerAnimator.AimAnimator.GetType() == typeof(MouseAimAnimator));
            Assert.IsTrue(playerAnimator.AimAnimator.GetType() == typeof(GamepadAimAnimator));
        }
    }
}
