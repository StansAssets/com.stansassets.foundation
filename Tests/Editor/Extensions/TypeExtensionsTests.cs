using System;
using JetBrains.Annotations;
using NUnit.Framework;
using StansAssets.Foundation.Extensions;

namespace StansAssets.Foundation.Tests.Extensions
{
    class TypeExtensionsTests
    {
        class ClassWithPublicConstructor
        {
            [UsedImplicitly]
            public ClassWithPublicConstructor() {}
        }
        
        class ClassWithPrivateConstructor
        {
            [UsedImplicitly]
            protected ClassWithPrivateConstructor() {}
        }
        
        interface IInterfaceA {}
        
        struct CustomStructure {}
        
        [TestCase(typeof(int))]
        [TestCase(typeof(CustomStructure))]
        [TestCase(typeof(ClassWithPublicConstructor))]
        public void HasDefaultConstructorTrue(Type type)
        {
            Assert.IsTrue(type.HasDefaultConstructor(), $"Default constructor NOT detected for:{type.FullName}");
        }
        
        [TestCase(typeof(IInterfaceA))]
        [TestCase(typeof(ClassWithPrivateConstructor))]
        public void HasDefaultConstructorFalse(Type type)
        {
            Assert.IsFalse(type.HasDefaultConstructor(), $"Default constructor IS detected for:{type.FullName}");
        }
    }
}
