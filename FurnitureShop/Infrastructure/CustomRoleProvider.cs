using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Ninject;
using FurnitureShop.Repository;
using FurnitureShop.Models;

namespace FurnitureShop.Infrastructure
{
	public class CustomRoleProvider : RoleProvider
	{
		private static List<User> _accountRoles = new List<User>();
		private static List<UserRole> _roles = new List<UserRole>();

		[Inject]
		public IUserRepository UserRepository { get; set; }
		public IUserRoleRepository RoleRepository { get; set; }

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
			RoleRepository.InsertOrUpdate(new UserRole(){Name = roleName});
			RoleRepository.Save();
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
			return RoleRepository.All.ToList().Select(r => r.Name).ToArray();
		}

		public override string[] GetRolesForUser(string username)
		{
			_accountRoles = UserRepository.All.ToList();

			string[] roles = _accountRoles.FindAll(u => u.Name == username).Select(u => u.UserRole.Name).ToArray();

			return roles;
		}

		public override string[] GetUsersInRole(string roleName)
		{
			_accountRoles = UserRepository.All.ToList();

			string[] users = _accountRoles.FindAll(u => u.UserRole.Name == roleName).Select(u => u.Name).ToArray();

			return users;
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			_accountRoles = UserRepository.All.ToList();

			bool userIsInrole = _accountRoles.FindAll(u => u.Name == username).FirstOrDefault(u => u.UserRole.Name == roleName).UserRole.Name == roleName;

			return userIsInrole;
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			_roles = RoleRepository.All.ToList();

			bool roleExist = _roles.Find(r => r.Name == roleName) != null;

			return roleExist;
		}
	}
}