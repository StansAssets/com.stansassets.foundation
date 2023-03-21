using NUnit.Framework;
using StansAssets.Foundation.Extensions;
using UnityEngine;

namespace StansAssets.Foundation.Tests.Extensions
{
    public class RectTransformExtensionsTest
    {
        [Test]
        public void SetLeft_SetsCorrectOffset()
        {
            // Arrange
            RectTransform rectTransform = new GameObject().AddComponent<RectTransform>();
            Vector2 initOffsetMin = rectTransform.offsetMin;
            Vector2 initOffsetMax = rectTransform.offsetMax;
            float left = 100f;

            // Act
            rectTransform.SetLeft(left);

            // Assert
            Assert.AreEqual(new Vector2(-left, initOffsetMin.y), rectTransform.offsetMin);
            Assert.AreEqual(initOffsetMax, rectTransform.offsetMax);
        }

        [Test]
        public void SetRight_SetsCorrectOffset()
        {
            // Arrange
            RectTransform rectTransform = new GameObject().AddComponent<RectTransform>();
            Vector2 initOffsetMin = rectTransform.offsetMin;
            Vector2 initOffsetMax = rectTransform.offsetMax;
            float right = 100f;

            // Act
            rectTransform.SetRight(right);

            // Assert
            Assert.AreEqual(initOffsetMin, rectTransform.offsetMin);
            Assert.AreEqual(new Vector2(right, initOffsetMax.y), rectTransform.offsetMax);
        }

        [Test]
        public void SetTop_SetsCorrectOffset()
        {
            // Arrange
            RectTransform rectTransform = new GameObject().AddComponent<RectTransform>();
            Vector2 initOffsetMin = rectTransform.offsetMin;
            Vector2 initOffsetMax = rectTransform.offsetMax;
            float top = 100f;

            // Act
            rectTransform.SetTop(top);

            // Assert
            Assert.AreEqual(initOffsetMin, rectTransform.offsetMin);
            Assert.AreEqual(new Vector2(initOffsetMax.x, top), rectTransform.offsetMax);
        }

        [Test]
        public void SetBottom_SetsCorrectOffset()
        {
            // Arrange
            RectTransform rectTransform = new GameObject().AddComponent<RectTransform>();
            Vector2 initOffsetMin = rectTransform.offsetMin;
            Vector2 initOffsetMax = rectTransform.offsetMax;
            float bottom = 100f;

            // Act
            rectTransform.SetBottom(bottom);

            // Assert
            Assert.AreEqual(new Vector2(initOffsetMin.x, -bottom), rectTransform.offsetMin);
            Assert.AreEqual(initOffsetMax, rectTransform.offsetMax);

        }
    }
}
