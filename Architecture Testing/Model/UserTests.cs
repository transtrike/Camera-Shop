using System;
using Data.Models.Classes;
using NUnit.Framework;

namespace Camera_Shop.Tests.Model
{
	public class UserTests
	{
		[Test]
		[TestCase("DT")]
		public void UsernameShouldNotSetIfValueLengthIsLessThanThree(string username)
		{
			User user = new User();
               
			Assert.Throws<ArgumentException>(
				() =>  user.UserName = username);
		}
	}
}