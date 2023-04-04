using NUnit.Framework;
using UnityEngine;

namespace StansAssets.Foundation.Utilities.Tests
{
    class EnumUtilityTests
    {
        const string k_fullyMatchString = "First";
        const string k_caseMismatchString = "seconD";
        const string k_multipleMatchCommaString = "First,Second,Third";
        const string k_multipleMatchSpaceString = "First Second Third";
        const string k_multipleMatchDotString = "First.Second.Third";
        
        [Test]
        [TestCase(k_fullyMatchString, ExpectedResult = true, TestName = "Fully match string can be parsed")]
        [TestCase(k_caseMismatchString, ExpectedResult = true, TestName = "Match string but with wrong case can be parsed")]
        [TestCase(k_multipleMatchCommaString, ExpectedResult = true, TestName = "Multiple match string with comma separator can be parsed")]
        [TestCase(k_multipleMatchSpaceString, ExpectedResult = false, TestName = "Multiple match string with space separator can't be parsed")]
        [TestCase(k_multipleMatchDotString, ExpectedResult = false, TestName = "Multiple match string with dot separator can't be parsed")]
        public bool CanBeParsedTest(string inputString)
        {
            return EnumUtility.CanBeParsed<TestEnum>(inputString);
        }
        
        [Test]
        [TestCase(k_fullyMatchString, ExpectedResult = TestEnum.First, TestName = "Fully match string input returns right result")]
        [TestCase(k_caseMismatchString, ExpectedResult = TestEnum.Second, TestName = "Match string but with wrong case returns right result")]
        [TestCase(k_multipleMatchCommaString, ExpectedResult = TestEnum.Third, TestName = "Multiple match string with comma separator returns right result")]
        [TestCase(k_multipleMatchSpaceString, ExpectedResult = TestEnum.Default, TestName = "Multiple match string with space separator returns default result")]
        [TestCase(k_multipleMatchDotString, ExpectedResult = TestEnum.Default, TestName = "Multiple match string with dot separator returns default result")]
        public TestEnum TryParseTest(string inputString)
        {
            TestEnum result;
            bool parsedWithoutExceptions = EnumUtility.TryParse(inputString, out result);

            Debug.Log($"String: \"{inputString}\". Parsed without exceptions: {parsedWithoutExceptions}");
            return result;
        }
        
        [Test]
        [TestCase(k_fullyMatchString, ExpectedResult = TestEnum.First, TestName = "Fully match string input returns right result")]
        [TestCase(k_caseMismatchString, ExpectedResult = TestEnum.Second, TestName = "Match string but with wrong case returns right result")]
        [TestCase(k_multipleMatchCommaString, ExpectedResult = TestEnum.Third, TestName = "Multiple match string with comma separator returns right result")]
        [TestCase(k_multipleMatchSpaceString, ExpectedResult = TestEnum.Default, TestName = "Multiple match string with space separator returns default result")]
        [TestCase(k_multipleMatchDotString, ExpectedResult = TestEnum.Default, TestName = "Multiple match string with dot separator returns default result")]
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