using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
                        ? ignoreAttrData.ToList().FirstOrDefault().ConstructorArguments.Where(val => val.ArgumentType == typeof(String))
                            .FirstOrDefault().Value.ToString().Split(',')
                            .Select(val => val.Trim()).ToArray()
                        : default;
            TestCasesCount = attrDataList.Any(val => val.AttributeType == typeof(TestCaseAttribute))
                ? attrDataList.Count(val => val.AttributeType == typeof(TestCaseAttribute))
                : 1;
        }








        public string TestMethodName { get; set; }
        public string Author { get; set; }
        public int TestCasesCount { get; set; }
        public string[] IgnoreValues { get; set; }
    }
}

