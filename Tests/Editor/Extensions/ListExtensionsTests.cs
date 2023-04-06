using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StansAssets.Foundation.Extensions;

namespace StansAssets.Foundation.Tests.Patterns
{
    class ListExtensionsTests  {

	    readonly List<int> m_ByIndexTestList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
	    readonly List<float> m_ByItemTestList = new List<float> { 0f, 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f };
	    readonly List<int> m_ByPredicateTestList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
	    readonly Predicate<int> m_TestPredicate = e => (e % 5) == 0;
	    
	    [Test]
	    [TestCase(4)]
	    public void ListRemoveBySwapTest(int index) {
		    var checkList = new HashSet<int>();
		    foreach (var item in m_ByIndexTestList) {
			    checkList.Add(item);
		    }
		    m_ByIndexTestList.RemoveBySwap(index);
		    checkList.Remove(index);
		    foreach (var item in checkList) {
			    Assert.Contains(item, m_ByIndexTestList);
		    }
		    
		    Assert.IsFalse(m_ByIndexTestList.Contains(index));
	    }

	    [Test]
	    [TestCase(3f)]
	    public void ListRemoveBySwapTest(float element) {
		    var checkList = new HashSet<float>();

		    foreach (var item in m_ByItemTestList) {
			    checkList.Add(item);
		    }

		    m_ByItemTestList.RemoveBySwap(element);
		    checkList.Remove(element);
		    foreach (var item in checkList) {
			    Assert.Contains(item, m_ByItemTestList);
		    }
		    Assert.IsFalse(m_ByItemTestList.Contains(element));
	    }
	    
	    [Test]
	    public void ListRemoveBySwapTest() {
		    var checkList = new HashSet<int>();
		    foreach (var item in m_ByPredicateTestList) {
			    checkList.Add(item);
		    }

		    var removableItem = m_ByPredicateTestList.First(i => m_TestPredicate(i));
		    m_ByPredicateTestList.RemoveBySwap(m_TestPredicate);
		    // HashSet<T>.RemoveWhere can't be used because it removes all elements evaluated by predicate, not the first only
		    checkList.Remove(removableItem);
		    foreach (var item in checkList) {
			    Assert.Contains(item, m_ByPredicateTestList);
		    }
		    Assert.IsFalse(m_ByPredicateTestList.Contains(removableItem));
	    }
    }
}