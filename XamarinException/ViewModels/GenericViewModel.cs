using DevExpress.XamarinForms.DataForm;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinException.Dtos;
using XamarinException.Views;

namespace XamarinException.ViewModels
{
	public class GenericViewModel<T> : BaseViewModel where T : DtoBase, new ()
	{
		#region fields
		T dataObject;
		private DataFormView masterForm;
		#endregion

		public GenericModelPage<T> Page { get; set; }

		public INavigation Navigation { get; set; }

		public Command SaveDataCommand { get; }
		public Command ResetDataCommand { get; }

		#region properties
		public T DtoObject
		{
			get => dataObject;
			set { SetProperty(ref dataObject, value); }
		}
		#endregion
		private Dictionary<PropertyInfo, object> dictPropValues { get; set; } = new Dictionary<PropertyInfo, object>();

		#region ctor
		public GenericViewModel(INavigation navigation, GenericModelPage<T> page)
		{
			Navigation = navigation;
			SaveDataCommand = new Command(async () => await CmdSaveFormData(), () => !IsBusy);
			ResetDataCommand = new Command(
				execute: () =>
				{
					try
					{
						IsBusy = true;
						((T)Page.MasterForm.DataObject).ClearData();
					}
					finally
					{
						IsBusy = false;
					}
				},
				canExecute: () =>
				{
					return !IsBusy;
				});
			//this.masterForm = masterForm;
			Page = page;
		}
		#endregion

		#region UI methods
			
		private async Task CmdSaveFormData()
		{
			try
			{
				if (!Page.MasterForm.Validate())
				{
					// save not allowed > invalid entries
					await Application.Current.MainPage.DisplayAlert("Save", "Daten können nicht gespeichert werden. Ungültige Einträge!", "Ok");
					return;
				}
				IsBusy = true;
				var state = await ((T)Page.MasterForm.DataObject).Save();
				//await Task.Delay(2000);

				if (state == SaveState.Success)
				{
					await Application.Current.MainPage.DisplayAlert("Save", "Daten gespeichert", "Ok");

					//Page.RefreshPage();
					((T)Page.MasterForm.DataObject).ClearData();

				}

				if (state == SaveState.Error)
					await Application.Current.MainPage.DisplayAlert("Fehler", "Fehler beim Speichern der Daten", "Ok");
			}
			finally
			{
				IsBusy = false;
			}
			
		}
		#endregion

		
	}
}
