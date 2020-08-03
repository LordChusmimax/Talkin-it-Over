using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerTest
    {
        // A Test behaves as an ordinary method
        //[Test]
        public void PlayerTestSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PlayerTestCreated()
        {
            var player = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/StickHolder.prefab", typeof(GameObject));
            Assert.IsNotNull(player);
            yield return null;
        }

    }
}
