﻿using FurnitureShop.Models;
using FurnitureShop.Repository;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace FurnitureShop.Infrastructure.Concrete
{
	public class CustomMembershipProvider : MembershipProvider
	{
		[Inject]
		public IUserRepository UserRepo { get; set; }
		/*public CustomMembershipProvider(IUserRepository AccountRepository) {
			userRepo = AccountRepository;
		}*/

		private static List<User> _users = new List<User>();

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

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new NotImplementedException();
		}

		public override bool EnablePasswordReset
		{
			get { throw new NotImplementedException(); }
		}

		public override bool EnablePasswordRetrieval
		{
			get { throw new NotImplementedException(); }
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override int GetNumberOfUsersOnline()
		{
			throw new NotImplementedException();
		}

		public override string GetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new NotImplementedException();
		}

		public override string GetUserNameByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public override int MaxInvalidPasswordAttempts
		{
			get { throw new NotImplementedException(); }
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get { throw new NotImplementedException(); }
		}

		public override int MinRequiredPasswordLength
		{
			get { throw new NotImplementedException(); }
		}

		public override int PasswordAttemptWindow
		{
			get { throw new NotImplementedException(); }
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get { throw new NotImplementedException(); }
		}

		public override string PasswordStrengthRegularExpression
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresQuestionAndAnswer
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresUniqueEmail
		{
			get { throw new NotImplementedException(); }
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override bool UnlockUser(string userName)
		{
			throw new NotImplementedException();
		}

		public override void UpdateUser(MembershipUser user)
		{
			throw new NotImplementedException();
		}

		public override bool ValidateUser(string username, string password)
		{
			//Users = context.Users.;
			_users = UserRepo.All.ToList();

			return _users.Exists(u => u.Name == username && u.Password == password);
		}
	}
}