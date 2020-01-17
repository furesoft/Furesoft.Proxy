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

        public static LNode CreateID(string src)
        {
            return F.Id(src);
        }
    }
}