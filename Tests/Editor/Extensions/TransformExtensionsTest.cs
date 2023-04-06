using NUnit.Framework;
using StansAssets.Foundation.Extensions;
using UnityEngine;

namespace StansAssets.Foundation.Tests.Extensions
{
    public class TransformExtensionsTest
    {
        [Test]
        [TestCase(0, false, false, ExpectedResult = 0, TestName = "Empty Hierarchy clears without exceptions")]
        [TestCase(10, false, false, ExpectedResult = 0, TestName = "Hierarchy with active objects cleared correctly")]
        [TestCase(10, true, false, ExpectedResult = 0, TestName = "Hierarchy with inactive objects cleared correctly")]
        [TestCase(10, true, true, ExpectedResult = 10, TestName = "Hierarchy with inactive objects is not cleared")]
        public int ClearHierarchyTest(int amount, bool deactivateChildrenAtSpawn, bool deleteActiveOnly)
        {
            //Arrange
            Transform parentTransform = new GameObject().transform;
            
            for (int i = 0; i < amount; i++)
            {
                Transform childTransform = new GameObject().transform;
                childTransform.parent = parentTransform;
                
                if (deactivateChildrenAtSpawn)
                    childTransform.gameObject.SetActive(false);
            }
            
            //Act
            parentTransform.ClearHierarchy(deleteActiveOnly);
            
            //Result
            return parentTransform.childCount;
        }

        [Test]
        [TestCase(true, TestName = "Existing object has been found") ]
        [TestCase(false, TestName = "The object hasn't been found and has been created")]
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
            Transform foundTransform = parentTransform.FindOrCreateChild(objectName);
            
            //Assert
            Assert.IsNotNull(foundTransform, "Found Transform is null");
            Assert.AreEqual(objectName, foundTransform.gameObject.name, "Name of found object isn't correct");
            Assert.AreEqual(Vector3.zero, foundTransform.localPosition, "Position has not been reset");
            Assert.AreEqual(Vector3.one, foundTransform.localScale, "Scale has not been reset");
        }
    }
}
