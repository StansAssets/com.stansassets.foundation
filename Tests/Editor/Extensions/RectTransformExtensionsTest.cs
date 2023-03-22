using NUnit.Framework;
using StansAssets.Foundation.Extensions;
using UnityEngine;

namespace StansAssets.Foundation.Tests.Extensions
{
    public class RectTransformExtensionsTest
    {
        [Test]
        public void SetLeftTest()
        {
            // Arrange
            RectTransform rectTransform = new GameObject().AddComponent<RectTransform>();
            Vector2 initOffsetMin = rectTransform.offsetMin;
            Vector2 initOffsetMax = rectTransform.offsetMax;
            float left = 100f;

            // Act
            rectTransform.SetLeft(left);

            // Assert
            Assert.AreEqual(left, rectTransform.offsetMin.x, "X Min Offset if wrong");
            Assert.AreEqual(initOffsetMin.y, rectTransform.offsetMin.y, "Y Min Offset if wrong");
            Assert.AreEqual(initOffsetMax.x, rectTransform.offsetMax.x, "X Max Offset if wrong");
            Assert.AreEqual(initOffsetMax.y, rectTransform.offsetMax.y, "Y Max Offset if wrong");
        }

        [Test]
        public void SetRightTest()
        {
            // Arrange
            RectTransform rectTransform = new GameObject().AddComponent<RectTransform>();
            Vector2 initOffsetMin = rectTransform.offsetMin;
            Vector2 initOffsetMax = rectTransform.offsetMax;
            float right = -100f;

            // Act
            rectTransform.SetRight(right);

            // Assert
            Assert.AreEqual(initOffsetMin.y, rectTransform.offsetMin.x, "X Min Offset if wrong");
            Assert.AreEqual(initOffsetMin.y, rectTransform.offsetMin.y, "Y Min Offset if wrong");
            Assert.AreEqual(-right, rectTransform.offsetMax.x, "X Max Offset if wrong");
            Assert.AreEqual(initOffsetMax.y, rectTransform.offsetMax.y, "Y Max Offset if wrong");
        }

        [Test]
        public void SetTopTest()
        {
            // Arrange
            RectTransform rectTransform = new GameObject().AddComponent<RectTransform>();
            Vector2 initOffsetMin = rectTransform.offsetMin;
            Vector2 initOffsetMax = rectTransform.offsetMax;
            float top = 100f;

            // Act
            rectTransform.SetTop(top);

            // Assert
            Assert.AreEqual(initOffsetMin.x, rectTransform.offsetMin.x, "X Min Offset if wrong");
            Assert.AreEqual(initOffsetMin.y, rectTransform.offsetMin.y, "Y Min Offset if wrong");
            Assert.AreEqual(initOffsetMax.x, rectTransform.offsetMax.x, "X Max Offset if wrong");
            Assert.AreEqual(-top, rectTransform.offsetMax.y, "Y Max Offset if wrong");
        }

        [Test]
        public void SetBottomTest()
        {
            // Arrange
            RectTransform rectTransform = new GameObject().AddComponent<RectTransform>();
            Vector2 initOffsetMin = rectTransform.offsetMin;
            Vector2 initOffsetMax = rectTransform.offsetMax;
            float bottom = -100f;

            // Act
            rectTransform.SetBottom(bottom);

            // Assert
            Assert.AreEqual(initOffsetMin.x, rectTransform.offsetMin.x, "X Min Offset if wrong");
            Assert.AreEqual(bottom, rectTransform.offsetMin.y, "Y Min Offset if wrong");
            Assert.AreEqual(initOffsetMax.x, rectTransform.offsetMax.x, "X Max Offset if wrong");
            Assert.AreEqual(initOffsetMax.y, rectTransform.offsetMax.y, "Y Max Offset if wrong");

        }
        
        [Test]
        public void ResetTest()
        {
            // Arrange
            RectTransform rectTransform = new GameObject().AddComponent<RectTransform>();

            // Act
            rectTransform.Reset();

            // Assert
            Assert.AreEqual(rectTransform.anchorMin, Vector2.zero, "X Min Offset if wrong");
            Assert.AreEqual(rectTransform.anchorMax, Vector2.zero, "X Min Offset if wrong");
            Assert.AreEqual(rectTransform.offsetMin, Vector2.zero, "X Min Offset if wrong");
            Assert.AreEqual(rectTransform.offsetMax, Vector2.zero, "X Min Offset if wrong");
        }
    }
}
