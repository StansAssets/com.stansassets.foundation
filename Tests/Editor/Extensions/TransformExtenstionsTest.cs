using NUnit.Framework;
using StansAssets.Foundation.Extensions;
using UnityEngine;

namespace Tests.Editor.Extensions
{
    public class TransformExtenstionsTest
    {
        [Test]
        [TestCase(0, false, ExpectedResult = 0)]
        [TestCase(10, false, ExpectedResult = 0)]
        [TestCase(10, true, ExpectedResult = 10)]
        public int ClearTest(int amount, bool activeOnly)
        {
            //Arrange
            Transform parentTransform = new GameObject().transform;

            for (int i = 0; i < amount; i++)
            {
                Transform childTransform = new GameObject().transform;
                childTransform.parent = parentTransform;
                
                if (activeOnly)
                    childTransform.gameObject.SetActive(false);
            }
            
            //Act
            parentTransform.Clear(activeOnly);
            
            //Return
            return parentTransform.childCount;
        }
    }
}
