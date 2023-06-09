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

		[HttpGet]	//Get All Users
		public async Task<IActionResult> GetUsers()
		{
			return Ok(await dbContext.Users.ToListAsync());
		}

		[HttpGet]	//Get Specific User
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

		[HttpPost]	//Create A User
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

		[HttpPost]  // Add User to Group
		[Route("{userId:guid}/groups/{groupId:guid}")]
		public async Task<IActionResult> AddUserToGroup([FromRoute] Guid userId, [FromRoute] Guid groupId)
		{
			var user = await dbContext.Users.FindAsync(userId);
			var group = await dbContext.Groups.FindAsync(groupId);

			if (user == null || group == null)
			{
				return NotFound();
			}

			var userGroup = new UserGroup
			{
				UserId = userId,
				GroupId = groupId
			};

			dbContext.UserGroups.Add(userGroup);
			await dbContext.SaveChangesAsync();

			return Ok(userGroup);
		}

		[HttpPut]	//Update Specific User
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

		[HttpDelete]	//Delete Specific User
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
			// Explicitly load the associated UserGroup entity
			// userGroup might cause problem	13/5
			await dbContext.Entry(user).Collection(u => u.UserGroups).LoadAsync();
			var userGroup = user.UserGroups;
         
			dbContext.RemoveRange(userGroup);    // Remove all associated UserGroups
			dbContext.Remove(user);			// Remove the user itself
			await dbContext.SaveChangesAsync();

			return Ok(user);
		}

		[HttpDelete]    // Remove User from Group
		[Route("{userId:guid}/groups/{groupId:guid}")]
		public async Task<IActionResult> RemoveUserFromGroup([FromRoute] Guid userId, [FromRoute] Guid groupId)
		{
			var userGroup = await dbContext.UserGroups.FirstOrDefaultAsync(ug => ug.UserId == userId && ug.GroupId == groupId);

			if (userGroup == null)
			{
				return NotFound();
			}

			dbContext.UserGroups.Remove(userGroup);
			await dbContext.SaveChangesAsync();

			return Ok(userGroup);
		}
	}
}