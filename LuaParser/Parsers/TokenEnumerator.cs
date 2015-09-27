﻿using System;
using System.Collections.Generic;
using DW.Lua.Exceptions;

namespace DW.Lua.Parsers
{
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
            if (_index > _tokens.Count) 
                throw new EndOfFileException();

            Previous = Current;
            Current = _index < _tokens.Count ? _tokens[_index] : null;
            Next = _index < _tokens.Count - 1 ? _tokens[_index+1] : null;
        }

        public string GetAndAdvance()
        {
            string token = Current;
            Advance();
            return token;
        }

        public bool Finished => _index >= _tokens.Count;
    }
}