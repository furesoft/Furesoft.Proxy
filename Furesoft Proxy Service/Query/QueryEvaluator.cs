﻿using Furesoft.Proxy.Language;
using Loyc.Syntax;
using System;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Http;

namespace Furesoft.Proxy.Query
{
    public static class QueryEvaluator
    {
        public static bool DoBlock(LNode result, SessionEventArgs e)
        {
            if (result.Name.Name == "Content")
            {
                var host = result.Args[0].Args[0].Value;
                var type = result.Args[1].Name;
                var content = result.Args[2].Args[0].Value;
                var uri = e?.HttpClient.Request.RequestUri;

                if (type.Name == "display")
                {
                    if (uri?.Host == host.ToString())
                    {
                        e?.Ok(content.ToString());

                        return true;
                    }
                }
            }

            return false;
        }

        public static LNode ParseQuery(string src)
        {
            var parser = new BlockQueryGrammar();
            var ast = parser.Parse(src);

            return ast;
        }
    }
}