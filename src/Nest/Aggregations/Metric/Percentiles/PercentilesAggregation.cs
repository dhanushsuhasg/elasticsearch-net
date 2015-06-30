using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<PercentilesAggregator>))]
	public interface IPercentilesAggregator : IMetricAggregator
	{
		[JsonProperty("percents")]
		IEnumerable<double> Percentages { get; set; }

		[JsonProperty("compression")]
		int? Compression { get; set; }
	}

	public class PercentilesAggregator : MetricAggregator, IPercentilesAggregator
	{
		public IEnumerable<double> Percentages { get; set; }
		public int? Compression { get; set; }
	}

	public class PercentilesAggregationDescriptor<T> 
		: MetricAggregationBaseDescriptor<PercentilesAggregationDescriptor<T>, IPercentilesAggregator, T>
			, IPercentilesAggregator 
		where T : class
	{
		IEnumerable<double> IPercentilesAggregator.Percentages { get; set; }

		int? IPercentilesAggregator.Compression { get; set; }

		public PercentilesAggregationDescriptor<T> Percentages(params double[] percentages) => Assign(a => a.Percentages = percentages);

		public PercentilesAggregationDescriptor<T> Compression(int compression) => Assign(a => a.Compression = compression);

	}
}