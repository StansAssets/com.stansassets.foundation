using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using NUnit.Framework;

namespace StansAssets.Foundation.Tests.Utilities
{
    class ReflectionUtilityTests
    {
        [AttributeUsage(validOn: AttributeTargets.Method)]
        class FancyAttribute : Attribute { }

        interface IInterfaceA { }

        interface IInterfaceB : IInterfaceA { }

        class ClassA : IInterfaceA
        {
            public int IntValue
            {
                get;
                [UsedImplicitly]
                set;
            }
            public string Name
            {
                get;
                [UsedImplicitly]
                set;
            }

            int PrivateValue
            {
                [UsedImplicitly]
                get;
                set;
            }

            public ClassA()
            {
                IntValue = -1;
                PrivateValue = -1;

                Name = string.Empty;
            }

            public ClassA(int intValue, string name)
            {
                IntValue = intValue;
                Name = name;
            }

            public void SetPrivateIntValue(int value)
            {
                PrivateValue = value;
            }

            public int GetPrivateIntValue()
            {
                return PrivateValue;
            }

            [Fancy, UsedImplicitly] public virtual void MethodWithAttribute() { }
        }

        [UsedImplicitly]
        class ClassAChild : ClassA
        {
            public override void MethodWithAttribute() { }
        }

        class ClassWithPublicConstructor : IInterfaceB
        {
            [UsedImplicitly] public ClassWithPublicConstructor() { }
        }

        class ClassWithPrivateConstructor
        {
            [UsedImplicitly] protected ClassWithPrivateConstructor() { }
        }

        struct CustomStructure { }

        [TestCase(typeof(int))]
        [TestCase(typeof(CustomStructure))]
        [TestCase(typeof(ClassWithPublicConstructor))]
        public void CreateInstanceValid(Type type)
        {
            Assert.IsNotNull(ReflectionUtility.CreateInstance(type.FullName), $"Failed to create instance for:{type.FullName}");
        }

        [TestCase(typeof(IInterfaceA))]
        [TestCase(typeof(ClassWithPrivateConstructor))]
        public void CreateInstanceInvalid(Type type)
        {
            Assert.IsNull(ReflectionUtility.CreateInstance(type.FullName), $"Failed to create instance for:{type.FullName}");
        }

        [TestCase("System.Int32")]
        [TestCase("UnityEngine.GameObject")]
        public void FindTypeValid(string fullTypeName)
        {
            Assert.IsNotNull(ReflectionUtility.FindType(fullTypeName), $"Type {fullTypeName} not found");
        }

        [TestCase("System.ClassDoesNotExist")]
        public void FindTypeInvalid(string fullTypeName)
        {
            Assert.IsNull(ReflectionUtility.FindType(fullTypeName), $"Type {fullTypeName} not found");
        }

        class TestAssembly
        {
            public string Name { get; }
            public bool IsBuiltIn { get; }

            public TestAssembly(string name, bool isBuiltIn)
            {
                Name = name;
                IsBuiltIn = isBuiltIn;
            }
        }

        static readonly IReadOnlyCollection<TestAssembly> s_TestAssemblies = new List<TestAssembly>
        {
            new TestAssembly("mscorlib", true),
            new TestAssembly("StansAssets.Foundation", false),
            new TestAssembly("UnityEngine", true)
        };

        [Test]
        public void GetAllAssemblies()
        {
            var assemblies = ReflectionUtility.GetAssemblies().ToList();
            Assert.True(assemblies.Any(), "Assemblies collection is empty");

            var assembliesSearchMap = new Dictionary<TestAssembly, bool>();
            foreach (var assembly in s_TestAssemblies)
            {
                assembliesSearchMap[assembly] = assemblies.Any(a => a.GetName().Name.Equals(assembly.Name));

                Assert.True(assembliesSearchMap[assembly], $"{assembly.Name} assembly is not found");
            }
        }

        [Test]
        public void GetAllAssembliesWithoutBuiltIn()
        {
            var assemblies = ReflectionUtility.GetAssemblies(true).ToList();
            Assert.True(assemblies.Any(), "Assemblies collection is empty");

            var assembliesSearchMap = new Dictionary<TestAssembly, bool>();
            foreach (var assembly in s_TestAssemblies)
            {
                assembliesSearchMap[assembly] = assemblies.Any(a => a.GetName().Name.Equals(assembly.Name));

                if (assembly.IsBuiltIn)
                {
                    Assert.False(assembliesSearchMap[assembly], $"Assembly {assembly.Name} should be ignored in this test-case");
                }
                else
                {
                    Assert.True(assembliesSearchMap[assembly], $"Assembly {assembly.Name} NOT found");
                }
            }
        }

        [Test]
        public void FindImplementationsOfInterface()
        {
            var type = typeof(IInterfaceB);
            var implementations = ReflectionUtility.FindImplementationsOf(type).ToList();

            Assert.True(implementations.Count == 1, $"Incorrect amount of implementations found for type:{type.FullName}");
            Assert.True(implementations[0] == typeof(ClassWithPublicConstructor), $"{implementations[0].FullName} is incorrect implementation of type:{type.FullName}");
        }

        [Test]
        public void FindMultipleImplementationsOf()
        {
            var type = typeof(IInterfaceA);
            var implementations = ReflectionUtility.FindImplementationsOf(type).ToList();

            Assert.True(implementations.Count == 3, $"Incorrect amount of implementations found for type:{type.FullName}");

            var implementation1 = typeof(ClassA);
            var implementation2 = typeof(ClassWithPublicConstructor);
            Assert.True(implementations.First(i => i == implementation1) != null, $"Implementation {implementation1.FullName} of {type} not found");
            Assert.True(implementations.First(i => i == implementation2) != null, $"Implementation {implementation2.FullName} of {type} not found");
        }

        [Test]
        public void FindImplementationsOfClassWithoutImplementations()
        {
            var type = typeof(ClassWithPrivateConstructor);
            var implementations = ReflectionUtility.FindImplementationsOf(type).ToList();

            Assert.True(implementations.Count == 0, $"Incorrect amount of implementations found for type:{type.FullName}");
        }

        [Test]
        public void GenericFindImplementationsOfInterface()
        {
            var type = typeof(IInterfaceB);
            var implementations = ReflectionUtility.FindImplementationsOf<IInterfaceB>().ToList();

            Assert.True(implementations.Count == 1, $"Incorrect amount of implementations found for type:{type.FullName}");
            Assert.True(implementations[0] == typeof(ClassWithPublicConstructor), $"{implementations[0].FullName} is incorrect implementation of type:{type.FullName}");
        }

        [Test]
        public void GenericFindMultipleImplementationsOf()
        {
            var type = typeof(IInterfaceA);
            var implementations = ReflectionUtility.FindImplementationsOf<IInterfaceA>().ToList();

            Assert.True(implementations.Count == 3, $"Incorrect amount of implementations found for type:{type.FullName}");

            var implementation1 = typeof(ClassA);
            var implementation2 = typeof(ClassWithPublicConstructor);
            Assert.True(implementations.First(i => i == implementation1) != null, $"Implementation {implementation1.FullName} of {type} not found");
            Assert.True(implementations.First(i => i == implementation2) != null, $"Implementation {implementation2.FullName} of {type} not found");
        }

        [Test]
        public void GenericFindImplementationsOfClassWithoutImplementations()
        {
            var type = typeof(ClassWithPrivateConstructor);
            var implementations = ReflectionUtility.FindImplementationsOf<ClassWithPrivateConstructor>().ToList();

            Assert.True(implementations.Count == 0, $"Incorrect amount of implementations found for type:{type.FullName}");
        }

        const int k_PublicIntValue = 25;
        const int k_PrivateIntValue = 123;
        const string k_Name = "Example Name";

        [Test]
        public void GetPropertyValue()
        {
            var data = new ClassA(k_PublicIntValue, k_Name);
            data.SetPrivateIntValue(k_PrivateIntValue);

            var intData = (int)ReflectionUtility.GetPropertyValue(data, "IntValue");
            Assert.True(intData == k_PublicIntValue, "Incorrect public integer property value");

            var stringData = (string)ReflectionUtility.GetPropertyValue(data, "Name");
            Assert.True(stringData == k_Name, "Incorrect public string property value");

            var privateIntData = (int)ReflectionUtility.GetPropertyValue(data, "PrivateValue", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.True(privateIntData == k_PrivateIntValue, "Incorrect private integer property value");
        }

        [Test]
        public void SetPropertyValue()
        {
            var data = new ClassA();
            ReflectionUtility.SetPropertyValue(data, "IntValue", k_PublicIntValue);
            Assert.True(data.IntValue == k_PublicIntValue, "Incorrect public integer property value");

            ReflectionUtility.SetPropertyValue(data, "Name", k_Name);
            Assert.True(data.Name == k_Name, "Incorrect public string property value");

            ReflectionUtility.SetPropertyValue(data, "PrivateValue", k_PrivateIntValue, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.True(data.GetPrivateIntValue() == k_PrivateIntValue, "Incorrect private integer property value");
        }
        
        [Test]
        public void FindMethodsWithCustomAttributes()
        {
            var allMethodInfos = ReflectionUtility.FindMethodsWithCustomAttributes<FancyAttribute>(ignoreBuiltIn: true).ToList();
            var nonInheritedMethodInfos = ReflectionUtility.FindMethodsWithCustomAttributes<FancyAttribute>(inherit: false, ignoreBuiltIn: true).ToList();

            Assert.True(allMethodInfos.Count == 2, "Incorrect amount of methods with custom attribute (inheritances included)");
            Assert.True(nonInheritedMethodInfos.Count == 1, "Incorrect amount of methods with custom attribute (inheritances NOT included)");
            
            Assert.True(nonInheritedMethodInfos[0].DeclaringType == typeof(ClassA), "Incorrect declaring type of the method with custom attribute");
        }
    }
}
