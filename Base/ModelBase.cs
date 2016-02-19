using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base
{
	public abstract class ModelBase
	{
		#region members
		#endregion

		#region ctors
		public ModelBase()
		{
			Id = Guid.NewGuid().ToString();
		}
		#endregion

		#region properties
		protected string Id { get; set; }
		#endregion

		#region private methods
		#endregion

		#region public methods
		public string GetId()
		{
			return Id;
		}

		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				if (obj.GetType().BaseType == typeof(ModelBase))
				{
					ModelBase check = (ModelBase)obj;
					if (check.Id == this.Id)
						return true;
					return false;
				}
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}
}
