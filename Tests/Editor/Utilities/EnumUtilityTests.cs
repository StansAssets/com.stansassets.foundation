using NUnit.Framework;

namespace StansAssets.Foundation.Tests.Utilities
{
    class EnumUtilityTests
    {
        const string k_FullyMatchString = "First";
        const string k_CaseMismatchString = "seconD";
        const string k_MultipleMatchCommaString = "First,Second,Third";
        const string k_MultipleMatchSpaceString = "First Second Third";
        const string k_MultipleMatchDotString = "First.Second.Third";

        [Test]
        [TestCase(k_FullyMatchString, ExpectedResult = true, TestName = "Fully matched string can be parsed")]
        [TestCase(k_CaseMismatchString, ExpectedResult = true, TestName = "Match string but with wrong case can be parsed")]
        [TestCase(k_MultipleMatchCommaString, ExpectedResult = true, TestName = "Multiple match string with comma separator can be parsed")]
        [TestCase(k_MultipleMatchSpaceString, ExpectedResult = false, TestName = "Multiple match string with space separator can't be parsed")]
        [TestCase(k_MultipleMatchDotString, ExpectedResult = false, TestName = "Multiple match string with dot separator can't be parsed")]
        public bool CanBeParsedTest(string inputString)
        {
            return EnumUtility.CanBeParsed<TestEnum>(inputString);
        }

        [Test]
        [TestCase(k_FullyMatchString, ExpectedResult = TestEnum.First, TestName = "Fully matched string input returns the correct result")]
        [TestCase(k_CaseMismatchString, ExpectedResult = TestEnum.Second, TestName = "Match string but with wrong case returns the correct result")]
        [TestCase(k_MultipleMatchCommaString, ExpectedResult = TestEnum.Third, TestName = "Multiple match string with comma separator returns the correct result")]
        [TestCase(k_MultipleMatchSpaceString, ExpectedResult = TestEnum.Default, TestName = "Multiple match string with space separator returns default result")]
        [TestCase(k_MultipleMatchDotString, ExpectedResult = TestEnum.Default, TestName = "Multiple match string with dot separator returns default result")]
        public TestEnum TryParseTest(string inputString)
        {
            _ = EnumUtility.TryParse(inputString, out TestEnum result);
            return result;
        }

        [Test]
        [TestCase(k_FullyMatchString, ExpectedResult = TestEnum.First, TestName = "Fully matched string input returns the correct result")]
        [TestCase(k_CaseMismatchString, ExpectedResult = TestEnum.Second, TestName = "Match string but with wrong case returns the correct result")]
        [TestCase(k_MultipleMatchCommaString, ExpectedResult = TestEnum.Third, TestName = "Multiple match string with comma separator returns the correct result")]
        [TestCase(k_MultipleMatchSpaceString, ExpectedResult = TestEnum.Default, TestName = "Multiple match string with space separator returns default result")]
        [TestCase(k_MultipleMatchDotString, ExpectedResult = TestEnum.Default, TestName = "Multiple match string with dot separator returns default result")]
        public TestEnum ParseOrDefaultTest(string inputString)
        {
            return EnumUtility.ParseOrDefault<TestEnum>(inputString);
        }

        public enum TestEnum
        {
            Default,
            First,
            Second,
            Third
        }
    }
}
