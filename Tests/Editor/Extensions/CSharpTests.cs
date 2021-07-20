using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StansAssets.Foundation.Extensions;
using UnityEngine;

namespace StansAssets.Foundation.Patterns.EditorTests 
{
    public class CSharpTests {

	    readonly List<int> m_byIndexTestList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
	    readonly List<float> m_byItemTestList = new List<float>() { 0f, 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f };
	    readonly List<int> m_byPredicateTestList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
	    readonly Predicate<int> m_testPredicate = e => (e % 5) == 0;
	    
	    [Test]
	    [TestCase(4)]
	    public void ListRemoveBySwapTest(int index) {
		    var checkList = new HashSet<int>();
		    foreach (var item in m_byIndexTestList) {
			    checkList.Add(item);
		    }
		    m_byIndexTestList.RemoveBySwap(index);
		    checkList.Remove(index);
		    foreach (var item in checkList) {
			    Assert.Contains(item, m_byIndexTestList);
		    }
		    
		    Assert.IsFalse(m_byIndexTestList.Contains(index));
	    }

	    [Test]
	    [TestCase(3f)]
	    public void ListRemoveBySwapTest(float element) {
		    var checkList = new HashSet<float>();

		    foreach (var item in m_byItemTestList) {
			    checkList.Add(item);
		    }

		    m_byItemTestList.RemoveBySwap(element);
		    checkList.Remove(element);
		    foreach (var item in checkList) {
			    Assert.Contains(item, m_byItemTestList);
		    }
		    Assert.IsFalse(m_byItemTestList.Contains(element));
	    }
	    
	    [Test]
	    public void ListRemoveBySwapTest() {
		    var checkList = new HashSet<int>();
		    foreach (var item in m_byPredicateTestList) {
			    checkList.Add(item);
		    }

		    var removableItem = m_byPredicateTestList.First(i => m_testPredicate(i));
		    m_byPredicateTestList.RemoveBySwap(m_testPredicate);
		    // HashSet<T>.RemoveWhere can't be used because it removes all elements evaluated by predicate, not the first only
		    checkList.Remove(removableItem);
		    foreach (var item in checkList) {
			    Assert.Contains(item, m_byPredicateTestList);
		    }
		    Assert.IsFalse(m_byPredicateTestList.Contains(removableItem));
	    }
    }
}