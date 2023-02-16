using NUnit.Framework;
using StansAssets.Foundation.Editor.Extensions;
using UnityEngine;

namespace StansAssets.Foundation.Tests.Extensions
{
    public class RectExtensionsTest
    {
        readonly Rect m_OtherRect = new Rect(5, 10, 20, 10);
        readonly double m_Delta = 0.001;

        Rect m_Rect;

        [Test]
        public void RectExtensionsWithWidthTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var expectedValue = 20f;
            var height = m_Rect.height;
            var xPosition = m_Rect.x;
            var yPosition = m_Rect.y;
            var center = new Vector2(xPosition + expectedValue / 2, m_Rect.center.y);

            // Act.
            m_Rect = m_Rect.WithWidth(expectedValue);

            // Assert.
            Assert.AreEqual(expected: expectedValue, actual: m_Rect.width, delta: m_Delta);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: xPosition, actual: m_Rect.x, delta: m_Delta);
            Assert.AreEqual(expected: yPosition, actual: m_Rect.y, delta: m_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsWithHeightTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var expectedValue = 20.3f;
            var width = m_Rect.width;
            var xPosition = m_Rect.x;
            var yPosition = m_Rect.y;
            var center = new Vector2(m_Rect.center.y, yPosition + expectedValue / 2);

            // Act.
            m_Rect = m_Rect.WithHeight(expectedValue);

            // Assert.
            Assert.AreEqual(expected: expectedValue, actual: m_Rect.height, m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
            Assert.AreEqual(expected: xPosition, actual: m_Rect.x, delta: m_Delta);
            Assert.AreEqual(expected: yPosition, actual: m_Rect.y, delta: m_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsWithSizeTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var expectedSize = new Vector2(15f,20f);
            var center = new Vector2(m_Rect.x + expectedSize.x / 2, m_Rect.y + expectedSize.y / 2);

            // Act.
            m_Rect = m_Rect.WithSize(expectedSize);

            // Assert.
            Assert.AreEqual(expected: expectedSize, actual: m_Rect.size);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsShiftHorizontallyTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var offset = 14f;
            var expectedXPosition = m_Rect.x + offset;
            var yPosition = m_Rect.y;
            var width = m_Rect.width;
            var height = m_Rect.height;
            var center = new Vector2(expectedXPosition + width / 2, yPosition + height / 2);

            // Act.
            m_Rect = m_Rect.ShiftHorizontally(offset);

            // Assert.
            Assert.AreEqual(expected: expectedXPosition, actual: m_Rect.x, delta: m_Delta);
            Assert.AreEqual(expected: yPosition, actual: m_Rect.y, delta: m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsShiftVerticallyTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var offset = 17.5f;
            var expectedYPosition = m_Rect.y + offset;
            var xPosition = m_Rect.x;
            var width = m_Rect.width;
            var height = m_Rect.height;
            var center = new Vector2(xPosition + width / 2, expectedYPosition + height / 2);

            // Act.
            m_Rect = m_Rect.ShiftVertically(offset);

            // Assert.
            Assert.AreEqual(expected: expectedYPosition, actual: m_Rect.y, delta: m_Delta);
            Assert.AreEqual(expected: xPosition, actual: m_Rect.x, delta: m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsAddWidthTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var width = 34.2f;
            var expectedWidth = m_Rect.width + width;
            var height = m_Rect.height;
            var xPosition = m_Rect.x;
            var yPosition = m_Rect.y;
            var center = new Vector2(xPosition + expectedWidth / 2, yPosition + height / 2);

            // Act.
            m_Rect = m_Rect.AddWidth(width);

            // Assert.
            Assert.AreEqual(expected: expectedWidth, actual: m_Rect.width, delta: m_Delta);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: xPosition, actual: m_Rect.x, delta: m_Delta);
            Assert.AreEqual(expected: yPosition, actual: m_Rect.y, delta: m_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsAddHeightTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var height = 31f;
            var expectedHeight = m_Rect.height + height;
            var width = m_Rect.width;
            var xPosition = m_Rect.x;
            var yPosition = m_Rect.y;
            var center = new Vector2(xPosition + width / 2, yPosition + expectedHeight / 2);

            // Act.
            m_Rect = m_Rect.AddHeight(height);

            // Assert.
            Assert.AreEqual(expected: expectedHeight, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
            Assert.AreEqual(expected: xPosition, actual: m_Rect.x, delta: m_Delta);
            Assert.AreEqual(expected: yPosition, actual: m_Rect.y, delta: m_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsAddSizeTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var size = new Vector2(12f, 5f);
            var expectedSize = new Vector2(m_Rect.size.x + size.x, m_Rect.size.y + size.y);
            var xPosition = m_Rect.x;
            var yPosition = m_Rect.y;
            var center = new Vector2(xPosition + expectedSize.x / 2, yPosition + expectedSize.y / 2);

            // Act.
            m_Rect = m_Rect.AddSize(size);

            // Assert.
            Assert.AreEqual(expected: expectedSize, actual: m_Rect.size);
            Assert.AreEqual(expected: xPosition, actual: m_Rect.x, delta: m_Delta);
            Assert.AreEqual(expected: yPosition, actual: m_Rect.y, delta: m_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsTranslateTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);

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
            Assert.AreEqual(expected: expectedXPosition, actual: m_Rect.x, delta: m_Delta);
            Assert.AreEqual(expected: expectedYPosition, actual: m_Rect.y, delta: m_Delta);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsPadWhenAllPaddingsValueIsPositiveTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            
            var leftPadding = 10f;
            var topPadding = 45.2f;
            var rightPadding = 33f;
            var bottomPadding = 8.5f;
            
            var width = m_Rect.width -  (leftPadding + rightPadding);
            var height = m_Rect.height - (topPadding + bottomPadding);
            var xPosition = m_Rect.x + (leftPadding + rightPadding) / 2;
            var yPosition = m_Rect.y + (topPadding + bottomPadding) / 2;
            var center = new Vector2(xPosition + width / 2, yPosition + height / 2);

            // Act.
            m_Rect = m_OtherRect.Pad(leftPadding, topPadding, rightPadding, bottomPadding);

            // Assert.
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsPadWhenAllPaddingsValueIsZeroTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);

            var leftPadding = 0f;
            var topPadding = 0f;
            var rightPadding = 0f;
            var bottomPadding = 0f;
            
            var width = m_Rect.width -  (leftPadding + rightPadding);
            var height = m_Rect.height - (topPadding + bottomPadding);
            var xPosition = m_Rect.x + (leftPadding + rightPadding) / 2;
            var yPosition = m_Rect.y + (topPadding + bottomPadding) / 2;
            var center = new Vector2(xPosition + width / 2, yPosition + height / 2);

            // Act.
            m_Rect = m_OtherRect.Pad(leftPadding, topPadding, rightPadding, bottomPadding);

            // Assert.
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }
        
        [Test]
        public void RectExtensionsPadWhenSomePaddingsValueIsNegativeTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);

            var leftPadding = 10f;
            var topPadding = -45.2f;
            var rightPadding = -33f;
            var bottomPadding = 8.5f;
            
            var width = m_Rect.width - (leftPadding + rightPadding);
            var height = m_Rect.height - (topPadding + bottomPadding);
            var xPosition = m_Rect.x + (leftPadding + rightPadding) / 2;
            var yPosition = m_Rect.y + (topPadding + bottomPadding) / 2;
            var center = new Vector2(xPosition + width / 2, yPosition + height / 2);

            // Act.
            m_Rect = m_OtherRect.Pad(leftPadding, topPadding, rightPadding, bottomPadding);

            // Assert.
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsPadSidesWhenPaddingIsPositiveTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var padding = 15.2f;
            
            var xPosition = m_Rect.x + padding;
            var yPosition = m_Rect.y + padding;
            var width = m_Rect.width - padding * 2f;
            var height = m_Rect.height - padding * 2f;
            var center = new Vector2(xPosition + width / 2, yPosition + height / 2);

            // Act.
            m_Rect = m_OtherRect.PadSides(padding);

            // Assert.
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsPadSidesWhenPaddingIsZeroTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var padding = 0.0f;
            
            var xPosition = m_Rect.x + padding;
            var yPosition = m_Rect.y + padding;
            var width = m_Rect.width - padding * 2f;
            var height = m_Rect.height - padding * 2f;
            var center = new Vector2(xPosition + width / 2, yPosition + height / 2);

            // Act.
            m_Rect = m_OtherRect.PadSides(padding);

            // Assert.
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsPadSidesWhenPaddingIsNegativeTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var padding = -15.2f;
            
            var xPosition = m_Rect.x + padding;
            var yPosition = m_Rect.y + padding;
            var width = m_Rect.width - padding * 2f;
            var height = m_Rect.height - padding * 2f;
            var center = new Vector2(xPosition + width / 2, yPosition + height / 2);

            // Act.
            m_Rect = m_OtherRect.PadSides(padding);

            // Assert.
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
            Assert.AreEqual(expected: center, actual: m_Rect.center);
        }

        [Test]
        public void RectExtensionsRightOfTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var otherRect = new Rect(10f, 5f, 10f, 10f);
            var width = m_Rect.width;
            var height = m_Rect.height;
            var center = new Vector2(otherRect.width + otherRect.x + width / 2, otherRect.center.y);
            
            // Act.
            m_Rect = m_Rect.RightOf(otherRect);

            // Assert.
            Assert.IsFalse(condition: m_Rect.Overlaps(otherRect));
            Assert.AreEqual(expected: center, actual: m_Rect.center);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
        }

        [Test]
        public void RectExtensionsLeftOfTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var otherRect = new Rect(5f, 10f, 10f, 10f);
            var width = m_Rect.width;
            var height = m_Rect.height;
            var center = new Vector2(otherRect.x - width / 2, otherRect.center.y);

            // Act.
            m_Rect = m_Rect.LeftOf(otherRect);

            // Assert.
            Assert.IsFalse(condition: m_Rect.Overlaps(otherRect));
            Assert.AreEqual(expected: center, actual: m_Rect.center);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
        }

        [Test]
        public void RectExtensionsAboveTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var otherRect = new Rect(0f, 5f, 20f, 20f);
            var width = m_Rect.width;
            var height = m_Rect.height;
            var center = new Vector2(otherRect.center.x, otherRect.y - height / 2);

            // Act.
            m_Rect = m_Rect.Above(otherRect);

            // Assert.
            Assert.IsFalse(condition: m_Rect.Overlaps(otherRect));
            Assert.AreEqual(expected: center, actual: m_Rect.center);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
        }

        [Test]
        public void RectExtensionsBelowTest()
        {
            // Arrange.
            m_Rect = new Rect(m_OtherRect);
            var otherRect = new Rect(0f, 5f, 20f, -20f);
            var width = m_Rect.width;
            var height = m_Rect.height;
            var center = new Vector2(otherRect.center.x, otherRect.height + otherRect.y + height / 2);

            // Act.
            m_Rect = m_Rect.Below(otherRect);

            // Assert.
            Assert.IsFalse(condition: m_Rect.Overlaps(otherRect));
            Assert.AreEqual(expected: center, actual: m_Rect.center);
            Assert.AreEqual(expected: height, actual: m_Rect.height, delta: m_Delta);
            Assert.AreEqual(expected: width, actual: m_Rect.width, delta: m_Delta);
        }
    }
}
