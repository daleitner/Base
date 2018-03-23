using System;

namespace Base
{
	public abstract class ModelBase
	{
		protected ModelBase()
		{
			Id = IdGenerator.GetNewId();
		}
		
		public virtual string Id { get; protected set; }
		public virtual string DisplayName { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null)
				return base.Equals(obj);

			if (obj.GetType().BaseType != typeof (ModelBase))
				return base.Equals(obj);

			var check = (ModelBase)obj;
			return check.Id == Id;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return DisplayName;
		}
	}
}
