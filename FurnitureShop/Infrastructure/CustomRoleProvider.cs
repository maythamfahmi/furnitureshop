using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Ninject;
using FurnitureShop.Repository;
using FurnitureShop.Models;

namespace FurnitureShop.Infrastructure
{
	public class CustomRoleProvider : RoleProvider
	{
		private static List<User> AccountRoles = new List<User>();
		[Inject]
		public IUserRepository UserRepository { get; set; }

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			throw new NotImplementedException();
		}

		public override string[] GetRolesForUser(string username)
		{
			AccountRoles = UserRepository.All.ToList();

			string[] roles = AccountRoles.FindAll(u => u.Name == username).Select(u => u.UserRole.Name).ToArray();

			return roles;
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			AccountRoles = UserRepository.All.ToList();

			bool UserIsInrole = AccountRoles.FindAll(u => u.Name == username).FirstOrDefault(u => u.UserRole.Name == roleName).UserRole.Name == roleName;

			return UserIsInrole;
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			throw new NotImplementedException();
		}
	}
}