namespace KnoweLia.Models
{
	public class UpdateUserRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public virtual Role Role { get; set; }
	}
}
