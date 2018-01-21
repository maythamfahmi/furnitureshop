using FurnitureShop.Controllers;
using FurnitureShop.Repository;
using FurnitureShop.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FurnitureShop.Infrastructure.Abstract;

namespace FurnitureShop.Tests
{
	[TestClass]
	public class UserTest
	{
		[TestMethod]
		public void Contains_all_Users()
		{
			//Arrange
			Mock<IUserRepository> mock = new Mock<IUserRepository>();
			//Mock<IUserRoleRepository> mockRoles = new Mock<IUserRoleRepository>();

			mock.Setup(m => m.AllIncluding(user => user.Address)).Returns(new List<User>() { 
			//mock.Setup(m => m.AllIncluding(It.IsAny<params>).Returns(new List<User>() { 
				new User() { UserId=1, Name="u1"},
				new User() { UserId=2, Name="u2"}
			}.AsQueryable());
			UsersController target = new UsersController(null, mock.Object, null);
			
			//Action
			User[] result = ((IQueryable<User>)target.Index().Model).ToArray();
			
			//Assert
			Assert.AreEqual(2, result.Length);
		}

		[TestMethod]
		public void Cannot_update_user_uncomplete_data()
		{
			//Arrange
			Mock<IUserRepository> mock = new Mock<IUserRepository>();
			Mock<IUserRoleRepository> mockRoles = new Mock<IUserRoleRepository>();

			UsersController target = new UsersController(mockRoles.Object, mock.Object, null);

			User unValidUser = new User() { FirstName="firstName"};
			// Arrange - add an error to the model state
			target.ModelState.AddModelError("error", "error");

			//Action
			ActionResult result = target.Edit(unValidUser);

			// Assert - check that the repository was not called
			mock.Verify(m => m.InsertOrUpdate(It.IsAny<User>()), Times.Never());

			// Assert - check the method result type
			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public void Can_Login_With_Valid_Credentials()
		{
			//Arrange - create a mock authentication provider
			Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
			mock.Setup(m => m.Authenticate("user", "secret")).Returns(true);

			//Arrange - create the view model
			LoginOnViewModel model = new LoginOnViewModel { UserName = "user", Password = "secret" };

			//Arrange - create the controller
			AccountController target = new AccountController(mock.Object, null, null);

			//Act - authenticate using valid credentials
			ActionResult result = target.Login(model, "/MyUrl");

			//Assert
			Assert.IsInstanceOfType(result, typeof(RedirectResult));
			Assert.AreEqual("/MyUrl", ((RedirectResult)result).Url);
		}

		[TestMethod]
		public void Cannot_Login_With_Invalid_Credentials()
		{
			//arrange - create a mock authentication provider
			Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
			mock.Setup(m => m.Authenticate("BadUser", "BadPass")).Returns(false);

			//Arrange - create the view model
			LoginOnViewModel model = new LoginOnViewModel { UserName = "BadUser", Password = "BadPass" };

			//Arrange - create the controller
			AccountController target = new AccountController(mock.Object, null,null);

			//Act - authenticate using valid credentials
			ActionResult result = target.Login(model, "/MyUrl");

			//Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
		}
	}
}
