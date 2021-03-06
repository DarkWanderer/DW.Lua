using DW.Lua.Extensions;
using DW.Lua.Language;
using DW.Lua.Lexer;
using DW.Lua.Misc;
using DW.Lua.Syntax;
using DW.Lua.Syntax.Control;

namespace DW.Lua.Parser.Statement
{
    internal sealed class WhileStatementParser : IStatementParser
    {
        public LuaStatement Parse(INextAwareEnumerator<Token> reader, IParserContext context)
        {
            reader.VerifyExpectedTokenAndMoveNext(Keywords.While);
            var conditionExpression = SyntaxParser.ReadExpression(reader, context);
            reader.VerifyExpectedTokenAndMoveNext(Keywords.Do);
            var blockParser = new StatementBlockParser(Keywords.End);
            var bodyBlock = blockParser.ParseBlock(reader, context);
            return new WhileStatement(conditionExpression, bodyBlock);
        }
    }
}