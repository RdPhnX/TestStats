using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestStats
{
    public class TestClassInfo
    {
        public TestClassInfo(TypeInfo typeInfo)
        {
            if (!typeInfo.IsClass)
            {
                return;
            }
            IEnumerable<CustomAttributeData> attrDataList = typeInfo.CustomAttributes;
            IEnumerable<CustomAttributeData> ignoreAttrData = attrDataList.Where(val => val.AttributeType == typeof(IgnoreAttribute)).Distinct();
            TestClassName = typeInfo.Name;
            IgnoreValues = ignoreAttrData.Any()
                        ? ignoreAttrData.ToList().FirstOrDefault().ConstructorArguments.Where(val => val.ArgumentType == typeof(String))
                            .FirstOrDefault().Value.ToString().Split(',')
                            .Select(val => val.Trim()).ToArray()
                        : default;
            IEnumerable<MethodInfo> testMethods = typeInfo.GetMethods()
                .Where(method => method.CustomAttributes.Any(attr => attr.AttributeType == typeof(TestAttribute)));
            TestInfos = new List<TestMethodInfo>();
            foreach (MethodInfo testMethod in testMethods)
            {
                if (!testMethod.CustomAttributes.Any(val => val.AttributeType == typeof(TestAttribute)))
                {
                    return;
                }
                TestInfos.Add(new TestMethodInfo(testMethod));
            }
        }
        public string TestClassName { get; set; }
        public string[] IgnoreValues { get; set; }
        public List<TestMethodInfo> TestInfos { get; set; }
    }
}

