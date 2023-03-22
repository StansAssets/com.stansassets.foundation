﻿using NUnit.Framework;
using StansAssets.Foundation.Extensions;
using UnityEngine;

namespace Tests.Editor.Extensions
{
    public class TransformExtenstionsTest
    {
        [Test]
        [TestCase(0, false, false,ExpectedResult = 0)]
        [TestCase(10, false, false, ExpectedResult = 0)]
        [TestCase(10, true, false, ExpectedResult = 0)]
        [TestCase(10, true, true, ExpectedResult = 10)]
        public int ClearTest(int amount, bool disactivateAtSpawn, bool deleteActiveOnly)
        {
            //Arrange
            Transform parentTransform = new GameObject().transform;
            
            for (int i = 0; i < amount; i++)
            {
                Transform childTransform = new GameObject().transform;
                childTransform.parent = parentTransform;
                
                if (disactivateAtSpawn)
                    childTransform.gameObject.SetActive(false);
            }
            
            //Act
            parentTransform.Clear(deleteActiveOnly);
            
            //Result
            return parentTransform.childCount;
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void FindOrCreateTest(bool createChild)
        {
            //Arrange
            string objectName = "object";
            
            Transform parentTransform = new GameObject().transform;
            parentTransform.localScale = new Vector3(5, 1, 5);
            if (createChild)
            {
                Transform childTransform = new GameObject(objectName).transform;
                childTransform.parent = parentTransform;
                childTransform.Reset();
            }
            
            //Act
            Transform foundedTransform = parentTransform.FindOrCreateChild(objectName);
            
            //Assert
            Assert.IsNotNull(foundedTransform, "Founded Transform is null");
            Assert.AreEqual(objectName, foundedTransform.gameObject.name, "Name of found object isn't correct");
            Assert.AreEqual(Vector3.zero, foundedTransform.localPosition, "Position has not been reset");
            Assert.AreEqual(Vector3.one, foundedTransform.localScale, "Scale has not been reset");
        }
    }
}
