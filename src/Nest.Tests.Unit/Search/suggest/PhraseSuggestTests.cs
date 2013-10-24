﻿using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Suggest
{
	[TestFixture]
	public class PhraseSuggestTests : BaseJsonTests
	{
		[Test]
        public void PhraseSuggestDescriptorTest()
		{
		    var phraseSuggestDescriptor = new PhraseSuggestDescriptor<ElasticSearchProject>()
		        .Analyzer("body")
		        .OnField("bigram")
		        .Size(1)
		        .MaxErrors(0.5m)
		        .GramSize(2);

            var json = TestElasticClient.Serialize(phraseSuggestDescriptor);

            var expected = @"{
                              ""gram_size"": 2,
                              ""max_errors"": 0.5,
                              ""field"": ""bigram"",
                              ""analyzer"": ""body"",
                              ""size"": 1
                            }";

            Assert.True(json.JsonEquals(expected), json);
        }

        [Test]
        public void PhraseSuggestDescriptorDirectGeneratorTest()
        {
            var phraseSuggestDescriptor = new PhraseSuggestDescriptor<ElasticSearchProject>()
                .Analyzer("body")
                .DirectGenerator(m => m.OnField("body").SuggestMode(SuggestMode.Always).MinWordLength(3));

            var json = TestElasticClient.Serialize(phraseSuggestDescriptor);

            var expected = @"{
                              ""direct_generator"": [
                                {
                                  ""field"": ""body"",
                                  ""suggest_mode"": ""always"",
                                  ""min_word_len"": 3
                                }
                              ],
                              ""analyzer"": ""body""
                            }";

            Assert.True(json.JsonEquals(expected), json);
        }


	} 
}
