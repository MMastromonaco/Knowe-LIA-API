using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnoweLia.Models
{
	public class User
	{
		[Key]
		public Guid UserId { get; set; } 
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public virtual Role Role { get; set; }
		// Navigation properties
		public List<UserGroup> UserGroups { get; set; }
	}
}