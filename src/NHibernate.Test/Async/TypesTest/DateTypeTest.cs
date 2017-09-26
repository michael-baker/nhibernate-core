﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Collections.Generic;
using NHibernate.Dialect;
using NHibernate.Type;
using NUnit.Framework;
using System;

namespace NHibernate.Test.TypesTest
{
	using System.Threading.Tasks;
	using System.Threading;

	[TestFixture]
	public class DateTypeFixtureAsync : TypeFixtureBase
	{
		protected override string TypeName
		{
			get { return "Date"; }
		}

		[Test]
		public Task ReadWriteNormalAsync()
		{
			try
			{
				var expected = DateTime.Today;

				return ReadWriteAsync(expected);
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		[Test]
		public Task ReadWriteMinAsync()
		{
			try
			{
				var expected = Sfi.ConnectionProvider.Driver.MinDate;

				return ReadWriteAsync(expected);
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		[Test]
		public Task ReadWriteYear750Async()
		{
			try
			{
				var expected = new DateTime(750, 5, 13);
				if (Sfi.ConnectionProvider.Driver.MinDate > expected)
				{
					Assert.Ignore($"The driver does not support dates below {Sfi.ConnectionProvider.Driver.MinDate:O}");
				}
				return ReadWriteAsync(expected);
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		private async Task ReadWriteAsync(DateTime expected, CancellationToken cancellationToken = default(CancellationToken))
		{
			// Add an hour to check it is correctly ignored once read back from db.
			var basic = new DateClass { DateValue = expected.AddHours(1) };
			object savedId;
			using (var s = OpenSession())
			{
				savedId = await (s.SaveAsync(basic, cancellationToken)); 
				await (s.FlushAsync(cancellationToken));
			}
			using (var s = OpenSession())
			{
				basic = await (s.GetAsync<DateClass>(savedId, cancellationToken));
				Assert.That(basic.DateValue, Is.EqualTo(expected));
				await (s.DeleteAsync(basic, cancellationToken));
				await (s.FlushAsync(cancellationToken));
			}
		}
	}
}