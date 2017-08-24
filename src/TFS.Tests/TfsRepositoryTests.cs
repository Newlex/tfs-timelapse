using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Timelase.TFS.Tests.Properties;

namespace Timelase.TFS.Tests
{
	[TestClass]
	public class TfsRepositoryTests
	{
		public TfsRepositoryTests()
		{
			AutoMapper.Mapper.Initialize(cfg =>
			{
				cfg.AddProfile<MapperProfile>();
			});
		}

		[TestMethod]
		public void GetItemChangesetsTest()
		{
			const string pathToTest = "$/Timelapse/Test";
			using (var repository = new TfsRepository(new Uri(Settings.Default.CollectionUri), Settings.Default.User, Settings.Default.Password))
			{
				var changesets = repository.GetItemChangesets(pathToTest).Result;
				Assert.AreNotEqual(changesets.Length, 0);
			}
		}
	}
}
