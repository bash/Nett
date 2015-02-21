﻿using System;
using Xunit;
using Xunit.Extensions;

namespace Nett.UnitTests
{
    public class StringTomlSerializerTests
    {
        [Theory]
        [InlineData("Key = 0", "Key", 0)]
        [InlineData("Key = +0", "Key", 0)]
        [InlineData("Key = -0", "Key", 0)]
        [InlineData("Key = 1", "Key", 1)]
        [InlineData("Key = +1", "Key", 1)]
        [InlineData("Key = -1", "Key", -1)]
        public void Deserialize_SimpleyKeyValuePair_ProducesCorrectObject(string toParse, string key, object expectedValue)
        {
            var parsed = StringTomlSerializer.Deserialize(toParse);

            Assert.NotNull(parsed);
            Assert.Equal(expectedValue, parsed.Get<int>(key));
            Assert.Equal((long)(int)expectedValue, (long)parsed[key]);
        }

        [Theory]
        [InlineData("Key = 1_000", "Key", 1000)]
        [InlineData("Key = 1_2_3_4", "Key", 1234)]
        public void Deserialize_IntWithUnderscoreSeperator_ProducesCorrectObject(string toParse, string key, object expectedValue)
        {
            var parsed = StringTomlSerializer.Deserialize(toParse);

            Assert.NotNull(parsed);
            Assert.Equal(expectedValue, parsed.Get<int>(key));
            Assert.Equal((long)(int)expectedValue, (long)parsed[key]);
        }

        [Fact]
        public void Deserilize_WithComments_ParserCanHandleCommentsCorrectly()
        {
            string toParse = "# I am a comment. Hear me roar. Roar." + Environment.NewLine +
                             "key = 0 # Yeah, you can do this. ";

            var parsed = StringTomlSerializer.Deserialize(toParse);

            Assert.NotNull(parsed);
            Assert.Equal(0, parsed.Get<int>("key"));
        }
    }
}