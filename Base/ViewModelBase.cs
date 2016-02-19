using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Base
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		protected void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(name));
			}
		}

		#region INotifyPropertyChanged Member
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
	}
}