using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinException
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
			LocalDataStore.Instance.Sync();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
