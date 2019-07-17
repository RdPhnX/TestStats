using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestStats;

namespace TestProject
{
    class MyTests
    {
        [Test]
        public void TestMethodInfo_Constructor()
        {
            TestClass tc = new TestClass();
            MethodInfo methodInfo = tc.GetType().GetMethod("TestMethod");
            TestMethodInfo testMethodInfo = new TestMethodInfo(methodInfo);
            Console.WriteLine(testMethodInfo.ToString());
            TestClassInfo tci = new TestClassInfo(tc.GetType().GetTypeInfo());
            Console.WriteLine(tci.ToString());
            Console.WriteLine(tci.PrintIgnoredStats());
        }
    }
}
