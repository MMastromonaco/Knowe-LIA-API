using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnoweLia.Models
{
	public class Role
	{
		[Key]
		public int RoleId { get; set; }
		public string Title { get; set; }
	}
}
