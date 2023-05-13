using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnoweLia.Models
{
	public class Role
	{
		[Key]
		public Guid RoleId { get; set; }
		//Below might cause problem		13/5
		//[ForeignKey("User")]
		//public Guid UserId { get; set; }
		// Guid generated id is in format 8-4-4-4-12
		public string Title { get; set; }
	}
}
