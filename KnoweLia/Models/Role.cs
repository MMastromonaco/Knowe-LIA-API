using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnoweLia.Models
{
	public class Role
	{
		[Key]
		public Guid RoleId { get; set; }
		// Guid generated id is in format 8-4-4-4-12
		public string Title { get; set; }
	}
}
