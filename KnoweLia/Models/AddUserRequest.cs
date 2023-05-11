namespace KnoweLia.Models
{
	public class AddUserRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public virtual Role Role { get; set; }
		public List<Group> Groups { get; set; }
	}
}
