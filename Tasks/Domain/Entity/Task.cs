namespace Tasks.Domain.Entity
{
	public class Task
	{
		public long Id { get; set; }

		public string Description { get; set; }

		public bool Done { get; set; }
	}
}