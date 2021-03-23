using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using XamarinException.Dtos;
using XamarinException.Views;

namespace XamarinException
{

	public partial class AppShell : Xamarin.Forms.Shell
	{

		public static List<dynamic> views = new List<dynamic>();
		Dictionary<string, Type> routes = new Dictionary<string, Type>();
		
		public AppShell()
		{
			Console.WriteLine($"ODE (>) AppShell CTOR");
			DevExpress.XamarinForms.Editors.Initializer.Init();
			InitializeComponent();

			//FlyoutItems.Items.Clear();		
			// hide tabbar
			Shell.SetTabBarIsVisible(this, false);
			// add new views here
			var sharedTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.GetCustomAttributes(typeof(GenericFormAttribute), true).Length > 0);
			var orderedSharedTypes = sharedTypes.OrderBy(x => ((GenericFormAttribute)(x.GetCustomAttribute(typeof(GenericFormAttribute)))).MenuOrder);

			foreach (Type type in orderedSharedTypes)
			{
				// instantiate views from model objects which have "GenericFormAttribute" set
				// get form title
				var formTitle = ((GenericFormAttribute)(type.GetCustomAttribute(typeof(GenericFormAttribute)))).FormTitle;
				var menuOrder = ((GenericFormAttribute)(type.GetCustomAttribute(typeof(GenericFormAttribute)))).MenuOrder.ToString();

				// get type of generic model page
				var genericPage = (typeof(GenericModelPage<>));
				// strongly type genericPage
				var constructedType = genericPage.MakeGenericType(type);
				dynamic pageInstance = Activator.CreateInstance(constructedType, formTitle);
				FlyoutItems.Items.Add(new ShellContent
				{
					Content = pageInstance,
					Title = pageInstance.Title,
					Route = $"{type.Name}Page"
				});
				//pageInstance.RouteToPage = $"IMPL_{type.Name}Page";
				views.Add(pageInstance);

			}

		}
	}
}
