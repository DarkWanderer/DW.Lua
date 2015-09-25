﻿using System;
using System.Collections;
using System.Collections.Generic;
using LuaParser.Exceptions;

namespace LuaParser.Parsers
{
    public interface ITokenEnumerator : IEnumerable<string>
    {
        string Previous { get; }
        string Next { get; }
        string Current { get; }
        bool Finished { get; }
        void Advance();
    }

    public class TokenEnumerator : ITokenEnumerator
    {
        private readonly IList<string> _tokens;
        private int _index = -1;

        public string Previous { get; private set; }
        public string Next { get; private set; }
        public string Current { get; private set; }

        public TokenEnumerator(IList<string> tokens)
        {
            if (tokens == null) throw new ArgumentNullException(nameof(tokens));
            _tokens = tokens;
            Advance();
        }

        public void Advance()
        {
            _index++;
            if (_index >= _tokens.Count)
                throw new EndOfFileException();

            if (_index > 0)
                Previous = _tokens[_index - 1];
                Current = _tokens[_index];

            Next = _index < _tokens.Count - 1 ? _tokens[_index+1] : null;
        }

        public bool Finished => Next == null;

        public IEnumerator<string> GetEnumerator()
        {
            return _tokens.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}