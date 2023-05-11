using System.ComponentModel.DataAnnotations.Schema;

namespace KnoweLia.Models
{
	public class UserGroup
	{
		[ForeignKey("User")]
		public Guid UserId { get; set; }
		public User User { get; set; }
		[ForeignKey("Group")]
		public Guid GroupId { get; set; }
		public Group Group { get; set; }
	}
}
