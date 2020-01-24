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

        [Test]
        public void ScriptParseTest()
        {
            var result = new Furesoft.Proxy.Language.BlockQueryGrammar().Parse("enable script \"blockImages\" start function onRequest() {} end");

            Assert.Pass();
        }

        [SetUp]
        public void Setup()
        {
        }
    }
}