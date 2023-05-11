using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnoweLia.Models
{
	public class User
	{
		[Key]
		public Guid UserId { get; set; }
		// Guid generated id is in format 8-4-4-4-12
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public virtual Role Role { get; set; }
		// Navigation properties
		public List<UserGroup> UserGroups { get; set; }
	}
}