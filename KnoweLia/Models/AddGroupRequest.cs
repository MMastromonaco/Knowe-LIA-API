namespace KnoweLia.Models
{
	public class AddGroupRequest
	{	
		public string Department { get; set; }
		public int Floor { get; set; }
		public List<User> Users { get; set; }
	}
}
