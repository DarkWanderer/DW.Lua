﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace DW.Lua.UnitTests.Code
{
    [TestFixture]
    public class ReferenceCodeTests
    {
        private static class CodeTestCaseFactory
        {
            private const string FixturesTag = ".Code.Fixtures.";
            private static readonly Assembly Assembly = typeof (CodeTestCaseFactory).Assembly;

            public static IEnumerable<TestCaseData> TestCases
            {
                get
                {
                    var fixtureResourceNames = Assembly.GetManifestResourceNames().Where(n => n.Contains(FixturesTag));
                    return from fixtureName in fixtureResourceNames
                        let code = GetFixtureCode(fixtureName)
                        select
                            new TestCaseData(code).SetName("Should parse: " + GetFixtureDisplayName(fixtureName));
                }
            }

            private static string GetFixtureDisplayName(string fixtureName)
            {
                return fixtureName.Replace(Assembly.GetName().Name, "")
                    .Replace(FixturesTag, "");
            }

            [ExcludeFromCodeCoverage]
            private static string GetFixtureCode(string resourceName)
            {
                string result;

                using (var stream = Assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                        using (var reader = new StreamReader(stream))
                            result = reader.ReadToEnd();
                    else
                    {
                        throw new InvalidOperationException("Could not load resource stream for " + resourceName);
                    }
                }
                return result;
            }
        }

        [Test]
        [TestCaseSource(typeof (CodeTestCaseFactory), nameof(CodeTestCaseFactory.TestCases))]
        public void Parse(string code)
        {
            SyntaxParser.Parse(code);
        }
    }
}