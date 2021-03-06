using System.Collections.Generic;
using DW.Lua.Extensions;
using DW.Lua.Language;
using DW.Lua.Lexer;
using DW.Lua.Misc;
using DW.Lua.Syntax;
using DW.Lua.Syntax.Statement;

namespace DW.Lua.Parser.Statement
{
    internal sealed class DoEndBlockStatementParser : IStatementParser
    {
        public LuaStatement Parse(INextAwareEnumerator<Token> reader, IParserContext context)
        {
            var scope = context.AcquireScope();
            reader.VerifyExpectedToken(Keywords.Do);
            reader.MoveNext();
            var statements = new List<LuaStatement>();
            while (reader.Current.Value != Keywords.End)
                statements.Add(SyntaxParser.ReadStatement(reader, context));
            reader.VerifyExpectedTokenAndMoveNext(Keywords.End);
            context.ReleaseScope(scope);
            return new DoEndBlock(new StatementBlock(statements));
        }
    }
}