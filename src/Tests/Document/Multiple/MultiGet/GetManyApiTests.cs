﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Document.Multiple.MultiGet
{
	[Collection(IntegrationContext.ReadOnly)]
	public class GetManyApiTests
	{
		private readonly ReadOnlyCluster _cluster;
		private readonly IEnumerable<long> _ids = Developer.Developers.Select(d => (long)d.Id).Take(10);
		private readonly IElasticClient _client;

		public GetManyApiTests(ReadOnlyCluster cluster)
		{
			_cluster = cluster;
			_client = _cluster.Client();
		}

		[I]
		public void UsesDefaultIndexAndInferredType()
		{
			var response = _client.GetMany<Developer>(_ids);
			response.Count().Should().Be(10);
		}

		[I]
		public async Task UsesDefaultIndexAndInferredTypeAsync()
		{
			var response = await _client.GetManyAsync<Developer>(_ids);
			response.Count().Should().Be(10);
		}
	}
}
