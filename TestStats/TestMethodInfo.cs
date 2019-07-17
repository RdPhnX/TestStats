using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestStats
{
    public class TestMethodInfo
    {
        public TestMethodInfo(MethodInfo methodInfo)
        {
            if (!methodInfo.CustomAttributes.Any(val => val.AttributeType == typeof(TestAttribute)))
            {
                return;
            }
            IEnumerable<CustomAttributeData> attrDataList = methodInfo.CustomAttributes;
            IEnumerable<CustomAttributeData> authorAttrData = attrDataList.Where(val => val.AttributeType == typeof(AuthorAttribute)).Distinct();
            IEnumerable<CustomAttributeData> ignoreAttrData = attrDataList.Where(val => val.AttributeType == typeof(IgnoreAttribute)).Distinct();
            TestMethodName = methodInfo.Name;
            Author = authorAttrData.Any()
                        ? authorAttrData.ToList().FirstOrDefault().ConstructorArguments.FirstOrDefault().Value.ToString()
                        : "NoName"; ;
            IgnoreValues = ignoreAttrData.Any()
                        ? ignoreAttrData.ToList().FirstOrDefault().ConstructorArguments
                            .FirstOrDefault(val => val.ArgumentType == typeof(String)).Value.ToString().Split(',')
                            .Select(val => val.Trim()).ToList()
                        : new List<string>();
            TestCasesCount = attrDataList.Any(val => val.AttributeType == typeof(TestCaseAttribute))
                ? attrDataList.Count(val => val.AttributeType == typeof(TestCaseAttribute))
                : 1;
        }

        public string TestMethodName { get; set; }
        public string Author { get; set; }
        public int TestCasesCount { get; set; }
        public List<string> IgnoreValues { get; set; }

        public override string ToString()
        {
            StringBuilder printTestMethodInfo = new StringBuilder();
            printTestMethodInfo.AppendLine($"Test method name: {TestMethodName}");
            printTestMethodInfo.AppendLine($"Author of the test: {Author}");
            printTestMethodInfo.AppendLine($"Q-ty of test cases: {TestCasesCount}");
            if (IgnoreValues.Any())
            {
                printTestMethodInfo.AppendLine($"Ignored by: {String.Join(", ", IgnoreValues)}");
            }
            return printTestMethodInfo.ToString();
        }
    }
}

