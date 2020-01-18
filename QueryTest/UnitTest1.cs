using Furesoft.Proxy.Query;
using NUnit.Framework;

namespace QueryTest
{
    public class Tests
    {
        [Test]
        public void ContentParseTest()
        {
            var result = QueryEvaluator.ParseQuery("content on \"mysite.domain\" display \"<h1> my own site</h1>\"");
            QueryEvaluator.DoBlock(result, null);

            Assert.Pass();
        }

        [SetUp]
        public void Setup()
        {
        }
    }
}