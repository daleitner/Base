using System;

namespace Base
{
	public abstract class ModelBase
	{
		#region members

		#endregion

		#region ctors
		protected ModelBase()
		{
			this.Id = Guid.NewGuid().ToString();
		}
		#endregion

		#region properties
		protected string Id { get; set; }
		public string DisplayName { get; set; }
		#endregion

		#region public methods
		public string GetId()
		{
			return this.Id;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return base.Equals(obj);

			if (obj.GetType().BaseType != typeof (ModelBase))
				return base.Equals(obj);

			var check = (ModelBase)obj;
			return check.Id == this.Id;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return this.DisplayName;
		}

		#endregion
	}
}
