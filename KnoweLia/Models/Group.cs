using System.ComponentModel.DataAnnotations;

namespace KnoweLia.Models
{
	public class Group
	{
		[Key]
		public int GroupId { get; set; }
		public string Department{ get; set; }
		public int Floor { get; set; }
		// Navigation properties
		public List<UserGroup> UserGroups { get; set; }
	}
}
