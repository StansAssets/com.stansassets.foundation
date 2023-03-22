using NUnit.Framework;
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
    }
}
