using Loyc.Syntax;
using System.Collections.Generic;

namespace Furesoft.Proxy.Language
{
    public static class SyntaxNode
    {
        public static LNodeFactory F = new LNodeFactory(new EmptySourceFile("blocklang"));

        public static LNode Combine(LNode f, LNode s)
        {
            if (s.IsCall && s.Name == CodeSymbols.AltList)
            {
                var res = new List<LNode>();
                res.Add(f);
                res.AddRange(s.Args);

                return F.List(res.ToArray());
            }

            return F.List(f, s);
        }

        public static LNode Combine(LNode f, IEnumerable<LNode> s)
        {
            var r = new List<LNode>();
            r.Add(f);
            r.AddRange(s);

            return F.List(r);
        }

        public static IEnumerable<LNode> Combine(LNode v)
        {
            return F.List(v);
        }

        public static LNode CreateBlock(IEnumerable<LNode> body)
        {
            return F.Braces(F.List(body)).SetStyle(NodeStyle.Statement);
        }

        public static LNode CreateBlock(LNode body)
        {
            return F.Braces(F.List(body)).SetStyle(NodeStyle.Statement);
        }

        public static LNode CreateContentCommand(LNode cond, LNode type, LNode arg)
        {
            return F.Call("Content", cond, type, arg);
        }

        public static LNode CreateID(string src)
        {
            return F.Id(src);
        }

        public static LNode CreateOption(bool value)
        {
            return F.Literal(value);
        }

        public static LNode CreateScriptCmd(LNode option, LNode name, string content)
        {
            return F.Fn(option, name, null, F.Literal(content));
        }

        public static LNode CreateString(string value)
        {
            return F.String.WithArgs(F.Literal(value));
        }
    }
}