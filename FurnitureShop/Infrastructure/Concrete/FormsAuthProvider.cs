using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FurnitureShop.Infrastructure.Abstract;
using System.Web.Security;

namespace FurnitureShop.Infrastructure.Concrete
{
	public class FormsAuthProvider : IAuthProvider
	{
		public bool Authenticate(string username, string password)
		{
			/*bool result = FormsAuthentication.Authenticate(username, password);

			if (result)
			{
				FormsAuthentication.SetAuthCookie(username, false);
			}*/

			bool result = Membership.ValidateUser(username, password);

			if (result)
			{
				FormsAuthentication.SetAuthCookie(username, false);
			}

			return result;
		}

		public void logout() {
			FormsAuthentication.SignOut();
		}
	}
}