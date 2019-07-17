// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class TestClass
    {
        [Test, Author("aaa")]
        [Ignore("qaa-1, qaa-2")]
        public void TestMethod()
        {
            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }
        [Test, Author("aaa")]
        [TestCase(22)]
        [TestCase(2)]
        public void TestMethod11(int answer)
        {
            // TODO: Add your test code here
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }

        [Test]
        [Ignore("qaa-3, qaa-2")]
        public void TestMethod1()
        {
            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }

        [Test, Author("bbb")]
        [Ignore("qaa-3")]
        public void TestMethod2()
        {
            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }

        [Test, Author("ccc")]
        public void TestMethod3()
        {
            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }

        [Test, Author("ccc")]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(13)]
        public void TestMethod4(int answer)
        {
            // TODO: Add your test code here
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }
    }
}
