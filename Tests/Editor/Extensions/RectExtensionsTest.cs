using NUnit.Framework;
using StansAssets.Foundation.Editor.Extensions;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace StansAssets.Foundation.Tests.Extensions
{
    class RectExtensionsTest
    {
        [Test]
        public void WithWidthTest()
        {
            // Arrange.
            var targetRect = new Rect(45f, 10f, 20f, 10f);
            var width = 20f;
            var height = targetRect.height;
            var x = targetRect.x;
            var y = targetRect.y;

            // Act.
            var result = targetRect.WithWidth(width);

            // Assert.
            Assert.AreEqual(width, result.width, "Width is incorrect");
            Assert.AreEqual(height, result.height, "Height is incorrect");
            Assert.AreEqual(x, result.x, "X position is incorrect");
            Assert.AreEqual(y, result.y, "Y position is incorrect");
        }

        [Test]
        public void WithHeightTest()
        {
            // Arrange.
            var targetRect = new Rect(45f, 10f, 20f, 10f);
            var height = 20.3f;
            var width = targetRect.width;
            var x = targetRect.x;
            var y = targetRect.y;

            // Act.
            var result = targetRect.WithHeight(height);

            // Assert.
            Assert.AreEqual(height, result.height, "Height is incorrect");
            Assert.AreEqual(width, result.width, "Width is incorrect");
            Assert.AreEqual(x, result.x, "X position is incorrect");
            Assert.AreEqual(y, result.y, "Y position is incorrect");
        }

        [Test]
        public void WithSizeTest()
        {
            // Arrange.
            var targetRect = new Rect(45f, 10f, 20f, 10f);
            var expectedSize = new Vector2(15f, 20f);
            var x = targetRect.x;
            var y = targetRect.y;

            // Act.
            var result = targetRect.WithSize(expectedSize);

            // Assert.
            Assert.AreEqual(expectedSize, result.size, "Size is incorrect");
            Assert.AreEqual(x, result.x, "X position is incorrect");
            Assert.AreEqual(y, result.y, "Y position is incorrect");
        }

        [Test]
        public void ShiftHorizontallyTest()
        {
            // Arrange.
            var targetRect = new Rect(45f, 10f, 20f, 10f);
            var offset = 14f;
            var expectedXPosition = targetRect.x + offset;
            var y = targetRect.y;
            var size = targetRect.size;

            // Act.
            var result = targetRect.ShiftHorizontally(offset);

            // Assert.
            Assert.AreEqual(expectedXPosition, result.x, "X position is incorrect");
            Assert.AreEqual(y, result.y, "Y position is incorrect");
            Assert.AreEqual(size, result.size, "Size is incorrect");
        }

        [Test]
        public void ShiftVerticallyTest()
        {
            // Arrange.
            var targetRect = new Rect(45f, 10f, 20f, 10f);
            var offset = 17.5f;
            var expectedYPosition = targetRect.y + offset;
            var x = targetRect.x;
            var size = targetRect.size;

            // Act.
            var result = targetRect.ShiftVertically(offset);

            // Assert.
            Assert.AreEqual(expectedYPosition, result.y, "Y position is incorrect");
            Assert.AreEqual(x, result.x, "X position is incorrect");
            Assert.AreEqual(size, result.size, "Size is incorrect");
        }

        [Test]
        public void AddWidthTest()
        {
            // Arrange.
            var targetRect = new Rect(45f, 10f, 20f, 10f);
            var width = 34.2f;
            var expectedWidth = targetRect.width + width;
            var height = targetRect.height;
            var x = targetRect.x;
            var y = targetRect.y;

            // Act.
            var result = targetRect.AddWidth(width);

            // Assert.
            Assert.AreEqual(expectedWidth, result.width, "Width is incorrect");
            Assert.AreEqual(height, result.height, "Height is incorrect");
            Assert.AreEqual(x, result.x, "X position is incorrect");
            Assert.AreEqual(y, result.y, "Y position is incorrect");
        }

        [Test]
        public void AddHeightTest()
        {
            // Arrange.
            var targetRect = new Rect(45f, 10f, 20f, 10f);
            var height = 31f;
            var expectedHeight = targetRect.height + height;
            var width = targetRect.width;
            var x = targetRect.x;
            var y = targetRect.y;

            // Act.
            var result = targetRect.AddHeight(height);

            // Assert.
            Assert.AreEqual(expectedHeight, result.height, "Height is incorrect");
            Assert.AreEqual(width, result.width, "Width is incorrect");
            Assert.AreEqual(x, result.x, "X position is incorrect");
            Assert.AreEqual(y, result.y, "Y position is incorrect");
        }

        [Test]
        public void AddSizeTest()
        {
            // Arrange.
            var targetRect = new Rect(45f, 10f, 20f, 10f);
            var size = new Vector2(12f, 5f);
            var expectedSize = new Vector2(targetRect.size.x + size.x, targetRect.size.y + size.y);
            var x = targetRect.x;
            var y = targetRect.y;

            // Act.
            var result = targetRect.AddSize(size);

            // Assert.
            Assert.AreEqual(expectedSize, result.size, "Size is incorrect");
            Assert.AreEqual(x, result.x, "X position is incorrect");
            Assert.AreEqual(y, result.y, "Y position is incorrect");
        }

        [Test]
        public void TranslateTest()
        {
            // Arrange.
            var targetRect = new Rect(45f, 10f, 20f, 10f);
            var xOffset = 5f;
            var yOffset = 7f;

            var expectedXPosition = targetRect.x + xOffset;
            var expectedYPosition = targetRect.y + yOffset;
            var size = targetRect.size;

            // Act.
            var result= targetRect.Translate(xOffset, yOffset);

            // Assert.
            Assert.AreEqual(expectedXPosition, result.x, "X position is incorrect");
            Assert.AreEqual(expectedYPosition, result.y, "Y position is incorrect");
            Assert.AreEqual(size, result.size, "Size is incorrect");
        }

        [Test]
        public void PadWithZeroPaddingTest()
        {
            // Arrange.
            var targetRect = new Rect(100.0f, 100.0f, 100.0f, 50.0f);

            // Act.
            var result = targetRect.Pad(0.0f, 0.0f, 0.0f, 0.0f);

            // Assert.
            Assert.AreEqual(targetRect, result, "Incorrect expected result");
        }

        [Test]
        public void PadWithPositivePaddingTest()
        {
            // Arrange.
            // For a visual representation of this test-case on a single-unit grid, please refer to the screenshot
            // https://drive.google.com/file/d/1HYq1Q-oXD4pc89tm4Yqjass9XfE4b_vH/view?usp=sharing
            var targetRect = new Rect(2f, 1f, 4f, 6f);
            var expectedRect = new Rect(3f, 2f, 2f, 2f);

            // Act.
            var result = targetRect.Pad(1f, 1f, 1f, 3f);

            // Assert.
            Assert.AreEqual(expectedRect, result, "Incorrect expected result");
        }

        [Test]
        public void PadWithNegativePaddingTest()
        {
            // Arrange.
            // For a visual representation of this test-case on a single-unit grid, please refer to the screenshot
            // https://drive.google.com/file/d/1RYSfgcdF290W5JglGcha1dg0swcA1Leo/view?usp=sharing
            var targetRect = new Rect(2f, 1f, 4f, 3f);
            var expectedRect = new Rect(1f, 2f, 4f, 5f);

            // Act.
            var result = targetRect.Pad(-1f, 1f, 1f, -3f);

            // Assert.
            Assert.AreEqual(expectedRect, result, "Incorrect expected result");
        }

        [Test]
        public void PadWithAllNegativePaddingTest()
        {
            // Arrange.
            // For a visual representation of this test-case on a single-unit grid, please refer to the screenshot
            // https://drive.google.com/file/d/1aW1EwfOS1_vANm1ZdvxcX0HXN9gYVvxf/view?usp=sharing
            var targetRect = new Rect(2f, 2f, 3f, 4f);
            var expectedRect = new Rect(1f, 1f, 5f, 7f);

            // Act.
            var result = targetRect.Pad(-1f, -1f, -1f, -2f);

            // Assert.
            Assert.AreEqual(expectedRect, result, "Incorrect expected result");
        }

        [Test]
        public void PadSidesWithZeroPaddingTest()
        {
            // Arrange.
            var targetRect = new Rect(45f, 10f, 20f, 10f);
            var padding = 0.0f;

            // Act.
            var result = targetRect.PadSides(padding);

            // Assert.
            Assert.AreEqual(targetRect, result, "Incorrect expected result");
        }

        [Test]
        public void PadSidesWithPositivePaddingTest()
        {
            // Arrange.
            // For a visual representation of this test-case on a single-unit grid, please refer to the screenshot
            // https://drive.google.com/file/d/1Ok2bJL4WrqVlq6cL0188dXUzLY0yJZrE/view?usp=sharing
            var targetRect = new Rect(1f, 2f, 5f, 6f);
            var padding = 1f;
            var expectedRect = new Rect(2f, 3f, 3f, 4f);

            // Act.
            var result = targetRect.PadSides(padding);

            // Assert.
            Assert.AreEqual(expectedRect, result, "Incorrect expected result");
        }

        [Test]
        public void PadSidesWithNegativePaddingTest()
        {
            // Arrange.
            // For a visual representation of this test-case on a single-unit grid, please refer to the screenshot
            // https://drive.google.com/file/d/1S3QxXQQ-uzLuGl0Ny3aaKLukI-MuTkGk/view?usp=sharing
            var targetRect = new Rect(2f, 2f, 3f, 4f);
            var negativePadding = -1f;
            var expectedRect = new Rect(1f, 1f, 5f, 6f);

            // Act.
            var result = targetRect.PadSides(negativePadding);

            // Assert.
            Assert.AreEqual(expectedRect, result, "Incorrect expected result");
        }

        [Test]
        public void RightOfTest()
        {
            // Arrange.
            var targetRect = new Rect(45f, 10f, 20f, 10f);
            var otherRect = new Rect(10f, 5f, 10f, 10f);

            // Act.
            var result = targetRect.RightOf(otherRect);

            // Assert.
            Assert.AreEqual(otherRect.width + otherRect.x, result.x, "X position is incorrect");
            Assert.AreEqual(targetRect.y, result.y, "Y position is incorrect");
            Assert.AreEqual(targetRect.width, result.width, "Width is incorrect");
            Assert.AreEqual(targetRect.height, result.height, "Height is incorrect");
        }

        [Test]
        public void LeftOfTest()
        {
            // Arrange.
            var targetRect = new Rect(45f, 10f, 20f, 10f);
            var otherRect = new Rect(5f, 10f, 10f, 10f);

            // Act.
            var result = targetRect.LeftOf(otherRect);

            // Assert.
            Assert.AreEqual(otherRect.x - targetRect.width, result.x, "X position is incorrect");
            Assert.AreEqual(targetRect.y, result.y, "Y position is incorrect");
            Assert.AreEqual(targetRect.width, result.width, "Width is incorrect");
            Assert.AreEqual(targetRect.height, result.height, "Height is incorrect");
        }

        [Test]
        public void AboveTest()
        {
            // Arrange.
            var targetRect = new Rect(45f, 10f, 20f, 10f);
            var otherRect = new Rect(0f, 5f, 20f, 20f);
            var width = targetRect.width;
            var height = targetRect.height;
            var x = targetRect.x;
            var y = otherRect.y - targetRect.height;

            // Act.
            var result = targetRect.Above(otherRect);

            // Assert.
            Assert.AreEqual(height, result.height, "Height is incorrect");
            Assert.AreEqual(width, result.width, "Width is incorrect");
            Assert.AreEqual(x, result.x, "X position is incorrect");
            Assert.AreEqual(y, result.y, "Y position is incorrect");
        }

        [Test]
        public void BelowTest()
        {
            // Arrange.
            var targetRect = new Rect(45f, 10f, 20f, 10f);
            var otherRect = new Rect(10f, 5f, 20f, 20f);
            var width = targetRect.width;
            var height = targetRect.height;
            var x = targetRect.x;
            var y = otherRect.y + otherRect.height;

            // Act.
            var result = targetRect.Below(otherRect);

            // Assert.
            Assert.AreEqual(height, result.height, "Height is incorrect");
            Assert.AreEqual(width, result.width, "Width is incorrect");
            Assert.AreEqual(x, result.x, "X position is incorrect");
            Assert.AreEqual(y, result.y, "Y position is incorrect");
        }
    }
}
