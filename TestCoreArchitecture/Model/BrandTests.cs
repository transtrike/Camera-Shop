using System;
using Camera_Shop.Models.Classes;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Camera_Shop.Tests.Model
{
	public class BrandTests
	{
		[Test]
		[TestCase("BD")]
		public void BrandShouldNotSetNameIfNameLengthIsLessThanThree(string name)
		{
			Brand brand = new Brand();

			Assert.Throws<ArgumentException>(
				() => brand.Name = name);
		}
	}
}