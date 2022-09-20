using System.ComponentModel.DataAnnotations;

namespace DAL.Entity {
	public class TodoItem {
		[Key]
		public virtual long Id { get; protected set; }
		public virtual string Name { get; set; }
		public virtual bool IsComplete { get; set; }
		public virtual string Secret { get; set; }

		public TodoItem(string name, bool isComplete, string secret = null) {
			Name = name;
			IsComplete = isComplete;
			Secret = secret;
		}
	}
}
