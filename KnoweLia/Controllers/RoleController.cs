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
	[Route("/api/role")]
	public class RoleController : Controller
	{
		private readonly KnoweLiaDbContext dbContext;

		public RoleController(KnoweLiaDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet]   //Get All Roles
		public async Task<IActionResult> GetRoles()
		{
			return Ok(await dbContext.Roles.ToListAsync());
		}

		[HttpGet]   //Get Specific Role
		[Route("{title}")]
		public async Task<IActionResult> GetRole([FromRoute] string title)
		{
			var role = await dbContext.Roles.FirstOrDefaultAsync(r => r.Title == title);

			if (role == null)
			{
				return NotFound();
			}

			return Ok(role);
		}

		[HttpPost]  //Create A Role
		public async Task<IActionResult> AddRole(AddRoleRequest addRoleRequest)
		{
			var role = new Role()
			{
				RoleId = Guid.NewGuid(),
				Title = addRoleRequest.Title
			};

			await dbContext.Roles.AddAsync(role);
			await dbContext.SaveChangesAsync();

			return Ok(role);
		}

		[HttpPut]   //Update Specific Role
		[Route("{title}")]
		public async Task<IActionResult> UpdateRole([FromRoute] string title, UpdateRoleRequest updateRoleRequest)
		{
			var role = await dbContext.Roles.FirstOrDefaultAsync(r => r.Title == title);

			if (title != null)
			{
				role.Title = updateRoleRequest.Title;

				await dbContext.SaveChangesAsync();
				return Ok(role);
			}

			return NotFound();
		}

		[HttpDelete]    //Delete Specific Role
		[Route("{roleId}")]
		public async Task<IActionResult> DeleteRole([FromRoute] Guid roleId)
		{
			var role = await dbContext.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);

			if (role == null)
			{
				return NotFound();
			}
			 
			dbContext.Remove(role);    // Remove the group itself
			await dbContext.SaveChangesAsync();

			return Ok(role);
		}
	}
}