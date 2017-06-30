using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Base;

namespace TestBase
{
	public class WindowGenerator
	{
		/// <summary>
		/// Generates a Window with viewModel as Content and size 800x600.</summary>
		/// <param name="viewModel">Content of the window.</param>
		/// <param name="assemblyName">Name of Assembly which contains the /Resources/ResourceDictionary.xaml to bind the viewModel to its view.</param>
		public static Window GenerateWindow(ViewModelBase viewModel, string assemblyName)
		{
			return GenerateWindow(viewModel, 800, 600, assemblyName);
		}

		/// <summary>
		/// Generates a Window with viewModel as Content and given size.</summary>
		/// <param name="viewModel">Content of the window.</param>
		/// <param name="width">Width of the window.</param>
		/// <param name="height">Height of the window.</param>
		/// <param name="assemblyName">Name of Assembly which contains the /Resources/ResourceDictionary.xaml to bind the viewModel to its view.</param>
		public static Window GenerateWindow(ViewModelBase viewModel, int width, int height, string assemblyName)
		{
			var window = new Window
			{
				Content = viewModel,
				Width = width,
				Height = height,
				Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/" + assemblyName + ";component/Resources/ResourceDictionary.xaml") }
			};
			
			return window;
		}
	}
}
