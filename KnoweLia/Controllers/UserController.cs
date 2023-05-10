using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnoweLia.Data;
using KnoweLia.Models;

namespace KnoweLia.Controllers
{
	[ApiController]
	[Route("/api/users")]
	public class UserController : Controller
	{
		private readonly LiaContext database;

		public UserController(LiaContext database)
		{
			this.database = database;
		}

		private static List<User> users = new List<User>
	   {
		   new User
		   {
			   UserId = 1,
			   FirstName = "Martin",
			   LastName = "Larsson",
		   },
		   new User
		   {
			   UserId = 2,
			   FirstName = "Giovanni",
			   LastName = "Mastromonaco"
		   }
	   };

		[HttpGet]
		public User[] Get()
		{
			return users.ToArray();
		}

		[HttpGet("{UserId}")]
		//public User[] GetUsers()
		//{

		//}

		[HttpPost]
		public void AddUser(User user)
		{
			user = new User
			{
				UserId = 3,
				FirstName = "Eric",
				LastName = "Johansson"
			};
			users.Add(user);
		}
	}
}