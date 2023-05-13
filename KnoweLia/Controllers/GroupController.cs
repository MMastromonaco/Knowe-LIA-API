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
    [Route("/api/group")]
    public class GroupController : Controller
    {
        private readonly KnoweLiaDbContext dbContext;

        public GroupController(KnoweLiaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

		[HttpGet]	// Get All Groups
		public async Task<IActionResult> GetGroups()
		{
			return Ok(await dbContext.Groups.ToListAsync());
		}

		[HttpGet]	//Get Specific Group
		[Route("{department}")]
		public async Task<IActionResult> GetGroup([FromRoute] string department)
		{
			var group = await dbContext.Groups.FirstOrDefaultAsync(g => g.Department == department);

			if (group == null)
			{
				return NotFound();
			}

			return Ok(group);
		}

		[HttpPost]	//Create A Group
		public async Task<IActionResult> AddGroup(AddGroupRequest addGroupRequest)
		{
			var group = new Group()
			{
				GroupId = Guid.NewGuid(),
				Department = addGroupRequest.Department,
				Floor = addGroupRequest.Floor
			};

			await dbContext.Groups.AddAsync(group);
			await dbContext.SaveChangesAsync();

			return Ok(group);
		}

		[HttpPut]   //Update Specific Group
		[Route("{floor}")]
		public async Task<IActionResult> UpdateGroup([FromRoute] int floor, UpdateGroupRequest updateGroupRequest)
		{
			var group = await dbContext.Groups.FirstOrDefaultAsync(g => g.Floor == floor);

			if (group != null)
			{
				group.Department = updateGroupRequest.Department;
				group.Floor = updateGroupRequest.Floor;

				await dbContext.SaveChangesAsync();
				return Ok(group);
			}

			return NotFound();
		}

		[HttpDelete]    //Delete Specific Group
		[Route("{department}")]
		public async Task<IActionResult> DeleteGroup([FromRoute] string department)
		{
			var group = await dbContext.Groups.FirstOrDefaultAsync(g => g.Department == department);

			if (group == null)
			{
				return NotFound();
			}

			// Explicitly load the associated UserGroup entity
			// userGroup might cause problem	13/5
			await dbContext.Entry(group).Collection(u => u.UserGroups).LoadAsync();
			var userGroups = group.UserGroups;

			dbContext.RemoveRange(userGroups);  // Remove all associated UserGroups
			dbContext.Remove(group);	// Remove the group itself
			await dbContext.SaveChangesAsync();

			return Ok(group);
		}
	}
}