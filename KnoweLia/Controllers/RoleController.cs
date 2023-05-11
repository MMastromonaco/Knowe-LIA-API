﻿using System;
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
	[Route("/api")]
	public class RoleController : Controller
	{
		private readonly KnoweLiaDbContext database;

		public RoleController(KnoweLiaDbContext database)
		{
			this.database = database;
		}
	}
}