using System;
using LuaParser.Extensions;

namespace LuaParser.Syntax
{
    public class Variable : IEquatable<Variable>
    {
        public Variable(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public bool Equals(Variable other)
        {
            return other?.Name == Name;
        }

        public override bool Equals(object obj)
        {
            return this.CheckEquality(obj);
        }
    }
}