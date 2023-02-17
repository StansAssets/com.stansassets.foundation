using NUnit.Framework;
using StansAssets.Foundation.Editor.Extensions;
using UnityEngine;

namespace StansAssets.Foundation.Tests.Extensions
{
    public class RectExtensionsTest
    {
        const double k_Delta = 0.001;

        Rect m_Rect;

        [Test]
        public void WithWidthTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var expectedValue = 20f;
            var height = m_Rect.height;
            var xPosition = m_Rect.x;
            var yPosition = m_Rect.y;
            var center = new Vector2(xPosition + expectedValue / 2, m_Rect.center.y);

            // Act.
            m_Rect = m_Rect.WithWidth(expectedValue);

            // Assert.
            Assert.AreEqual(expected: expectedValue, actual: m_Rect.width, delta: k_Delta);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: k_Delta);
            Assert.AreEqual(expected: xPosition, actual: m_Rect.x, delta: k_Delta);
            Assert.AreEqual(expected: yPosition, actual: m_Rect.y, delta: k_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void WithHeightTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var expectedValue = 20.3f;
            var width = m_Rect.width;
            var xPosition = m_Rect.x;
            var yPosition = m_Rect.y;
            var center = new Vector2(m_Rect.center.x, yPosition + expectedValue / 2);

            // Act.
            m_Rect = m_Rect.WithHeight(expectedValue);

            // Assert.
            Assert.AreEqual(expected: expectedValue, actual: m_Rect.height, k_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: k_Delta);
            Assert.AreEqual(expected: xPosition, actual: m_Rect.x, delta: k_Delta);
            Assert.AreEqual(expected: yPosition, actual: m_Rect.y, delta: k_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void WithSizeTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var expectedSize = new Vector2(15f,20f);
            var center = new Vector2(m_Rect.x + expectedSize.x / 2, m_Rect.y + expectedSize.y / 2);

            // Act.
            m_Rect = m_Rect.WithSize(expectedSize);

            // Assert.
            Assert.AreEqual(expected: expectedSize, actual: m_Rect.size);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void ShiftHorizontallyTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var offset = 14f;
            var expectedXPosition = m_Rect.x + offset;
            var yPosition = m_Rect.y;
            var width = m_Rect.width;
            var height = m_Rect.height;
            var center = new Vector2(expectedXPosition + width / 2, yPosition + height / 2);

            // Act.
            m_Rect = m_Rect.ShiftHorizontally(offset);

            // Assert.
            Assert.AreEqual(expected: expectedXPosition, actual: m_Rect.x, delta: k_Delta);
            Assert.AreEqual(expected: yPosition, actual: m_Rect.y, delta: k_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: k_Delta);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: k_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void ShiftVerticallyTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var offset = 17.5f;
            var expectedYPosition = m_Rect.y + offset;
            var xPosition = m_Rect.x;
            var width = m_Rect.width;
            var height = m_Rect.height;
            var center = new Vector2(xPosition + width / 2, expectedYPosition + height / 2);

            // Act.
            m_Rect = m_Rect.ShiftVertically(offset);

            // Assert.
            Assert.AreEqual(expected: expectedYPosition, actual: m_Rect.y, delta: k_Delta);
            Assert.AreEqual(expected: xPosition, actual: m_Rect.x, delta: k_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: k_Delta);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: k_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void AddWidthTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var width = 34.2f;
            var expectedWidth = m_Rect.width + width;
            var height = m_Rect.height;
            var xPosition = m_Rect.x;
            var yPosition = m_Rect.y;
            var center = new Vector2(xPosition + expectedWidth / 2, yPosition + height / 2);

            // Act.
            m_Rect = m_Rect.AddWidth(width);

            // Assert.
            Assert.AreEqual(expected: expectedWidth, actual: m_Rect.width, delta: k_Delta);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: k_Delta);
            Assert.AreEqual(expected: xPosition, actual: m_Rect.x, delta: k_Delta);
            Assert.AreEqual(expected: yPosition, actual: m_Rect.y, delta: k_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void AddHeightTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var height = 31f;
            var expectedHeight = m_Rect.height + height;
            var width = m_Rect.width;
            var xPosition = m_Rect.x;
            var yPosition = m_Rect.y;
            var center = new Vector2(xPosition + width / 2, yPosition + expectedHeight / 2);

            // Act.
            m_Rect = m_Rect.AddHeight(height);

            // Assert.
            Assert.AreEqual(expected: expectedHeight, actual: m_Rect.height, delta: k_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: k_Delta);
            Assert.AreEqual(expected: xPosition, actual: m_Rect.x, delta: k_Delta);
            Assert.AreEqual(expected: yPosition, actual: m_Rect.y, delta: k_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void AddSizeTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var size = new Vector2(12f, 5f);
            var expectedSize = new Vector2(m_Rect.size.x + size.x, m_Rect.size.y + size.y);
            var xPosition = m_Rect.x;
            var yPosition = m_Rect.y;
            var center = new Vector2(xPosition + expectedSize.x / 2, yPosition + expectedSize.y / 2);

            // Act.
            m_Rect = m_Rect.AddSize(size);

            // Assert.
            Assert.AreEqual(expected: expectedSize, actual: m_Rect.size);
            Assert.AreEqual(expected: xPosition, actual: m_Rect.x, delta: k_Delta);
            Assert.AreEqual(expected: yPosition, actual: m_Rect.y, delta: k_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsTranslateTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);

            var x = 12f;
            var y = -25f;

            var expectedXPosition = m_Rect.x + x;
            var expectedYPosition = m_Rect.y + y;
            var width = m_Rect.width;
            var height = m_Rect.height;
            var center = new Vector2(expectedXPosition + width / 2, expectedYPosition + height / 2);

            // Act.
            m_Rect = m_Rect.Translate(x, y);

            // Assert.
            Assert.AreEqual(expected: expectedXPosition, actual: m_Rect.x, delta: k_Delta);
            Assert.AreEqual(expected: expectedYPosition, actual: m_Rect.y, delta: k_Delta);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: k_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: k_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void PadWhenAllPaddingsValueIsPositiveTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);

            var leftPadding = 10f;
            var topPadding = 45.2f;
            var rightPadding = 33f;
            var bottomPadding = 8.5f;

            // Act.
            var result = m_Rect.Pad(leftPadding, topPadding, rightPadding, bottomPadding);

            // Assert.
            Assert.AreNotEqual(expected: result, actual: m_Rect);
        }

        [Test]
        public void PadWhenAllPaddingsValueIsZeroTest()
        {
            // Arrange.
            m_Rect = new Rect(100.0f, 100.0f, 100.0f, 50.0f);

            // Act.
            var result = m_Rect.Pad(0.0f, 0.0f, 0.0f, 0.0f);

            // Assert.
            Assert.AreEqual(expected: result, actual: m_Rect);
            Assert.AreEqual(expected: result.center, actual: m_Rect.center);
        }
        
        [Test]
        public void PadWhenSomePaddingsValueIsNegativeTest()
        {
            // Arrange.
            m_Rect = new Rect(100.0f, 100.0f, 100.0f, 50.0f);

            var leftPadding = 10f;
            var topPadding = -45.2f;
            var rightPadding = -33f;
            var bottomPadding = 8.5f;

            // Act.
            var result = m_Rect.Pad(leftPadding, topPadding, rightPadding, bottomPadding);

            // Assert.
            Assert.AreNotEqual(expected: result, actual: m_Rect);
        }

        [Test]
        public void PadSidesWhenPaddingIsPositiveTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var padding = 15.2f;

            // Act.
            var result = m_Rect.PadSides(padding);

            // Assert.
            Assert.AreNotEqual(expected: result, actual: m_Rect);
        }

        [Test]
        public void PadSidesWhenPaddingIsZeroTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var padding = 0.0f;

            // Act.
            var result = m_Rect.PadSides(padding);

            // Assert.
            Assert.AreEqual(expected: result, actual: m_Rect);
            Assert.AreEqual(expected: result.center, actual: m_Rect.center);
        }

        [Test]
        public void PadSidesWhenPaddingIsNegativeTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var padding = -15.2f;
          
            // Act.
            var result = m_Rect.PadSides(padding);

            // Assert.
            Assert.AreNotEqual(expected: result, actual: m_Rect); 
        }

        [Test]
        public void RightOfTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var otherRect = new Rect(10f, 5f, 10f, 10f);
            var width = m_Rect.width;
            var height = m_Rect.height;
            var center = new Vector2((otherRect.x + otherRect.width) + width / 2, m_Rect.center.y);

            // Act.
            var result = m_Rect.RightOf(otherRect);

            // Assert.
            Assert.AreNotEqual(expected: result, actual: m_Rect);
            Assert.AreEqual(expected: center, actual: result.center);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: k_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: k_Delta);
        }

        [Test]
        public void LeftOfTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var otherRect = new Rect(5f, 10f, 10f, 10f);
            var width = m_Rect.width;
            var height = m_Rect.height;
            var center = new Vector2((otherRect.x - width) + width / 2, m_Rect.center.y);

            // Act.
            var result = m_Rect.LeftOf(otherRect);

            // Assert.
            Assert.AreNotEqual(expected: result, actual: m_Rect);
            Assert.AreEqual(expected: center, actual: result.center);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: k_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: k_Delta);
        }

        [Test]
        public void AboveTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var otherRect = new Rect(0f, 5f, 20f, 20f);
            var width = m_Rect.width;
            var height = m_Rect.height;
            var center = new Vector2(m_Rect.center.x, otherRect.y - height + height / 2);

            // Act.
            var result = m_Rect.Above(otherRect);

            // Assert.
            Assert.AreNotEqual(expected: result, actual: m_Rect);
            Assert.AreEqual(expected: center, actual: result.center);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: k_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: k_Delta);
        }

        [Test]
        public void BelowTest()
        {
            // Arrange.
            m_Rect = new Rect(45, 10, 20, 10);
            var otherRect = new Rect(10f, 5f, 20f, 20f);
            var width = m_Rect.width;
            var height = m_Rect.height;
            var center = new Vector2(m_Rect.center.x, otherRect.y + otherRect.height + height / 2);

            // Act.
            var result = m_Rect.Below(otherRect);

            // Assert.
            Assert.AreNotEqual(expected: result, actual: m_Rect);
            Assert.AreEqual(expected: center, actual: result.center);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: k_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: k_Delta);
        }
    }
}
