using NUnit.Framework;
using StansAssets.Foundation.Editor.Extensions;
using UnityEngine;

namespace StansAssets.Foundation.Tests.Extensions
{
    public class RectExtensionsTest
    {
        Rect m_Rect = new Rect();

        [Test]
        public void WhenChangeRectWidth_AndRectIsDefaultOrNot_ThenWidthShouldBe20()
        {
            // Arrange.
            float expectedValue = 20f;

            // Act.
            m_Rect = m_Rect.WithWidth(expectedValue);

            // Assert.
            Assert.AreEqual(expected: 20f, actual: m_Rect.width);
        }

        [Test]
        public void WhenChangeRectHeight_AndRectIsDefaultOrNot_ThenHeightShouldBe20()
        {
            // Arrange.
            float expectedValue = 20f;

            // Act.
            m_Rect = m_Rect.WithHeight(expectedValue);

            // Assert.
            Assert.AreEqual(expected: 20f, actual: m_Rect.height);
        }

        [Test]
        public void WhenChangeRectSize_AndRectIsDefaultOrNot_ThenSizeShouldBeExpected()
        {
            // Arrange.
            Vector2 expectedSize = new Vector2(15f,20f);

            // Act.
            m_Rect = m_Rect.WithSize(expectedSize);

            // Assert.
            Assert.AreEqual(expected: expectedSize, actual: m_Rect.size);
        }

        [Test]
        public void WhenShiftRectHorizontally_AndRectIsDefaultOrNot_ThenPositionShouldBeExpected()
        {
            // Arrange.
            float offset = 14f;
            float expected = m_Rect.x + offset;

            // Act.
            m_Rect = m_Rect.ShiftHorizontally(offset);

            // Assert.
            Assert.AreEqual(expected: expected, actual: m_Rect.x);
        }

        [Test]
        public void WhenShiftRectVertically_AndRectIsDefaultOrNot_ThenPositionShouldBeExpected()
        {
            // Arrange.
            float offset = 17f;
            float expected = m_Rect.y + offset;

            // Act.
            m_Rect = m_Rect.ShiftVertically(offset);

            // Assert.
            Assert.AreEqual(expected: expected, actual: m_Rect.y);
        }

        [Test]
        public void WhenAddWidthToRect_AndRectIsDefaultOrNot_ThenRectWidthShouldBeExpected()
        {
            // Arrange.
            float width = 34f;
            float expectedWidth = m_Rect.width + width;

            // Act.
            m_Rect = m_Rect.AddWidth(width);

            // Assert.
            Assert.AreEqual(expected: expectedWidth, actual: m_Rect.width);
        }

        [Test]
        public void WhenAddHeightToRect_AndRectIsDefaultOrNot_ThenRectHeightShouldBeExpected()
        {
            // Arrange.
            float height = 31f;
            float expectedHeight = m_Rect.height + height;

            // Act.
            m_Rect = m_Rect.AddHeight(height);

            // Assert.
            Assert.AreEqual(expected: expectedHeight, actual: m_Rect.height);
        }

        [Test]
        public void WhenAddSizeToRect_AndRectIsDefaultOrNot_ThenRectSizeShouldBeExpected()
        {
            // Arrange.
            Vector2 size = new Vector2(12f,5f);
            Vector2 expectedSize = new Vector2(m_Rect.size.x + size.x, m_Rect.size.y + size.y);

            // Act.
            m_Rect = m_Rect.AddSize(size);

            // Assert.
            Assert.AreEqual(expected: expectedSize, actual: m_Rect.size);
        }

        [Test]
        public void WhenTranslateRect_AndRectIsDefaultOrNot_ThenRectPositionShouldBeExpected()
        {
            // Arrange.
            float x = 12f;
            float y = -25f;

            float expectedXPosition = m_Rect.x + x;
            float expectedYPosition = m_Rect.y + y;

            // Act.
            m_Rect = m_Rect.Translate(x, y);

            // Assert.
            Assert.AreEqual(expected: expectedXPosition, actual: m_Rect.x);
            Assert.AreEqual(expected: expectedYPosition, actual: m_Rect.y);
        }

        [Test]
        public void WhenChangeRectPaddingByEachSide_AndRectIsDefaultOrNot_ThenRectPaddingShouldBeExpected()
        {
            // Arrange.
            float leftPadding = 10f;
            float topPadding = -45.2f;
            float rightPadding = -33f;
            float bottomPadding = 8.5f;
            Rect expectedRect = new Rect(m_Rect.x + (leftPadding + rightPadding) / 2,
                m_Rect.y + (topPadding + bottomPadding) / 2,
                m_Rect.width - (leftPadding + rightPadding),
                m_Rect.height - (topPadding + bottomPadding));

            // Act.
            m_Rect = m_Rect.Pad(leftPadding, topPadding, rightPadding, bottomPadding);

            // Assert.
            Assert.AreEqual(expected: expectedRect, actual: m_Rect);
        }

        [Test]
        public void WhenChangeRectPaddingOnEachSide_AndRectIsDefaultOrNot_ThenRectPaddingShouldBeExpected()
        {
            // Arrange.
            float padding = 15.2f;
            Rect expectedRect = new Rect(m_Rect.x + padding, m_Rect.y + padding,
                m_Rect.width - padding * 2f, m_Rect.height - padding * 2f);

            // Act.
            m_Rect = m_Rect.PadSides(padding);

            // Assert.
            Assert.AreEqual(expected: expectedRect, actual: m_Rect);
        }

        [Test]
        public void WhenPlacingRectRightOfOtherRect_AndRectIsDefaultOrNot_ThenRectShouldBePlacedRightOfOtherRect()
        {
            // Arrange.
            Rect otherRect = new Rect(10f,5f,10f,10f);

            // Act.
            m_Rect = m_Rect.RightOf(otherRect);

            // Assert.
            Assert.IsTrue(condition: m_Rect.x > otherRect.x);
        }

        [Test]
        public void WhenPlacingRectLeftOfOtherRect_AndRectIsDefaultOrNot_ThenRectShouldBePlacedLeftOfOtherRect()
        {
            // Arrange.
            Rect otherRect = new Rect(5f, 10f, 10f, 10f);

            // Act.
            m_Rect = m_Rect.LeftOf(otherRect);

            // Assert.
            Assert.IsFalse(condition: m_Rect.x > otherRect.x);
        }

        [Test]
        public void WhenPlacingRectAboveOtherRect_AndRectIsDefaultOrNot_ThenRectShouldBePlacedAboveOtherRect()
        {
            // Arrange.
            Rect otherRect = new Rect(0f, 5f, 20f, 20f);

            // Act.
            m_Rect = m_Rect.Above(otherRect);

            // Assert.
            Assert.IsFalse(condition: m_Rect.y > otherRect.y);
        }

        [Test]
        public void WhenPlacingRectBelowOtherRect_AndRectIsDefaultOrNot_ThenRectShouldBePlacedBelowOtherRect()
        {
            // Arrange.
            Rect otherRect = new Rect(0f, 5f, 20f, 20f);

            // Act.
            m_Rect = m_Rect.Below(otherRect);

            // Assert.
            Assert.IsTrue(condition: m_Rect.y > otherRect.y);
        }
    }
}
