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
	[Route("/api/user")]
	public class UserController : Controller
	{
		private readonly KnoweLiaDbContext dbContext;
		public UserController(KnoweLiaDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			return Ok(await dbContext.Users.ToListAsync());
		}

		//[HttpGet]
		//public async Task<IActionResult> GetUsers([FromQuery] Guid? groupId)
		//{
		//	if (groupId.HasValue)
		//	{
		//		var users = await dbContext.Users
		//			.Where(u => u.UserGroups.Any(ug => ug.GroupId == groupId.Value))
		//			.ToListAsync();

		//		return Ok(users);
		//	}

		//	return Ok(await dbContext.Users.ToListAsync());
		//}

		[HttpGet]
		[Route("{userId:guid}")]
		public async Task<IActionResult> GetUser([FromRoute] Guid userId)
		{
			var user = await dbContext.Users.FindAsync(userId);

			if(user == null)
			{
				return NotFound();
			}

			return Ok(user);
		}

		[HttpPost]
		public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
		{
			var user = new User()
			{
				UserId = Guid.NewGuid(),
				FirstName = addUserRequest.FirstName,
				LastName = addUserRequest.LastName,
				Role = addUserRequest.Role
			};

			await dbContext.Users.AddAsync(user);
			await dbContext.SaveChangesAsync();

			return Ok(user);
		}

		[HttpPut]
		[Route("{userId:guid}")]
		public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, UpdateUserRequest updateUserRequest)
		{
			var user = await dbContext.Users.FindAsync(userId);

			if (user != null)
			{
				user.FirstName = updateUserRequest.FirstName;
				user.LastName = updateUserRequest.LastName;
				user.Role = updateUserRequest.Role;

				await dbContext.SaveChangesAsync();
				return Ok(user);
			}

			return NotFound();
		}

		[HttpDelete]
		[Route("{userId:Guid}")]
		public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
		{
			var user = await dbContext.Users.FindAsync(userId);

			if (user == null)
			{
				return NotFound();
			}

			// Explicitly load the associated Role entity
			await dbContext.Entry(user).Reference(u => u.Role).LoadAsync();
			var role = user.Role;

			dbContext.Remove(user);
			dbContext.Remove(role);
			await dbContext.SaveChangesAsync();

			return Ok(user);
		}

		//[HttpDelete]
		//[Route("{userId:Guid}")]
		//public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
		//{
		//	var user = await dbContext.Users.FindAsync(userId);

		//	if (user != null)
		//	{
		//		dbContext.Remove(user);
		//		await dbContext.SaveChangesAsync();
		//		return Ok(user);
		//	}

		//	return NotFound();
		//}
	}
}