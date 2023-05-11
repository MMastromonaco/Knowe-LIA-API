using System.ComponentModel.DataAnnotations;

namespace KnoweLia.Models
{
	public class Group
	{
		[Key]
		public Guid GroupId { get; set; }
		// Guid generated id is in format 8-4-4-4-12
		public string Department{ get; set; }
		public int Floor { get; set; }
		// Navigation properties
		public List<UserGroup> UserGroups { get; set; }
	}
}
