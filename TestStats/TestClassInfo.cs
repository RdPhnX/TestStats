using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
                        ? ignoreAttrData.ToList().FirstOrDefault().ConstructorArguments
                            .FirstOrDefault(val => val.ArgumentType == typeof(String)).Value.ToString()
                            .Split(',').Select(val => val.Trim()).ToList()
                        : new List<string>();
            IEnumerable<MethodInfo> testMethods = typeInfo.GetMethods()
                .Where(method => method.CustomAttributes.Any(attr => attr.AttributeType == typeof(TestAttribute)));
            TestInfos = new List<TestMethodInfo>();
            foreach (MethodInfo testMethod in testMethods)
            {
                TestInfos.Add(new TestMethodInfo(testMethod));
            }
        }
        public string TestClassName { get; set; }
        public List<string> IgnoreValues { get; set; }
        public List<TestMethodInfo> TestInfos { get; set; }

        public override string ToString()
        {
            StringBuilder classInformation = new StringBuilder();
            classInformation.AppendLine($"Test class name: {TestClassName}");
            classInformation.AppendLine($"Test methods q-ty: {TestInfos.Count}");
            classInformation.AppendLine($"Ignored by: {String.Join(", ", IgnoreValues)}");
            return classInformation.ToString();
        }

        public string PrintIgnoredStats()
        {
            StringBuilder ignoreStats = new StringBuilder();
            IEnumerable<string> allIgnores = new List<string>();
            foreach (var testInfo in TestInfos)
            {
                allIgnores = allIgnores.Union(testInfo.IgnoreValues).Distinct();
            }
            ignoreStats.AppendLine($"Tests are ignored by these tickets: {String.Join(", ", allIgnores)}");
            return ignoreStats.ToString();
        }
    }
}

