using DevExpress.XamarinForms.DataForm;
using DevExpress.XamarinForms.Editors;
using Xamarin.Forms;
using XamarinException.Dtos;
using XamarinException.ViewModels;

namespace XamarinException.Views
{
	public class GenericModelPage<T> : ContentPage where T : DtoBase, new() 
	{

		public DataFormView MasterForm { get; set; }
		private GenericViewModel<T> _viewModel { get; set; }
		private string Formtitle { get; set; }
		
		public GenericModelPage(string title)
		{

			//setup data form resource dictionary
			var dfStyes = new ResourceDictionary
			{
				new Style(typeof(ComboBoxEdit))
				{
					Setters =
					{
						new Setter { Property = ComboBoxEdit.DropDownItemPaddingProperty, Value = new Thickness(1.0) }
					}
				}
			};

			Formtitle = title;
			Grid maingrid = new Grid()
			{
				RowDefinitions = new RowDefinitionCollection()
				{
					{ new RowDefinition() { Height =  new GridLength(0.9, GridUnitType.Star)} },
					{ new RowDefinition() { Height =  new GridLength(0.1, GridUnitType.Star)} }
				}
			};


			Button saveBtn = new Button
			{
				Text = "Speichern"
				//Margin = new Thickness(0.0, 10.0, 10.0, 10.0)
			};
			saveBtn.SetBinding(Button.CommandProperty, new Binding() { Path = "SaveDataCommand" });

			Button resetBtn = new Button
			{
				Text = "Reset"
			};
			resetBtn.SetBinding(Button.CommandProperty, new Binding() { Path = "ResetDataCommand" });

			ActivityIndicator busyIndicator = new ActivityIndicator()
			{ 
				IsRunning = false
			};
			busyIndicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding() { Path = "IsBusy" });

			StackLayout btnStack = new StackLayout()
			{
				VerticalOptions = LayoutOptions.End,
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Center,
				Children = { busyIndicator, saveBtn, resetBtn  }
			};

			MasterForm = new DataFormView()
			{
				CommitMode = CommitMode.PropertyChanged,
				ValidationMode = CommitMode.LostFocus,
				IsEditorLabelVisible = true,
				IsGroupHeaderVisible = true,
				GroupHeaderTextColor = Color.FromHex("#bcd640"),
				GroupHeaderBackgroundColor = Color.FromHex("#4c4d4f"),
				RowSeparatorThickness = 1,
				IsLastRowSeparatorVisible = false,
				Resources = dfStyes
			};
			
			MasterForm.Resources.Add("DXEditBaseBorderThickness", 1);
			MasterForm.Resources.Add("DXEditBaseBoxMinHeight", 20);
			MasterForm.Resources.Add("DXEditBaseFocusedBorderThickness", 1);
			MasterForm.Resources.Add("DXEditBaseCornerRadius", 0);
			MasterForm.Resources.Add("DXEditBaseBoxPadding", 5);
			// https://docs.devexpress.com/MobileControls/401766/xamarin-forms/data-form/examples/data-form-custom-appearance#resource-keys
			MasterForm.Resources["DXDataFormContentPadding"] = 0;
			MasterForm.Resources["DXDataFormGroupHeaderPadding"] = 0;
			MasterForm.Resources["DXEditBaseTextFontSize"] = 10;
			MasterForm.Resources["DXEditBaseLabelFontSize"] = 10;
			MasterForm.Resources["DXEditBaseBottomTextFontSize"] = 10;
			MasterForm.EditorLabelColor = Color.Black;
			

			StackLayout formStack = new StackLayout()
			{
				Orientation = StackOrientation.Vertical,
				Margin = new Thickness(1.0, 0.0, 1.0, 0.0),
				//HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = { MasterForm }
			};

			ScrollView dataScroll = new ScrollView()
			{
				Content = formStack
			};

			maingrid.Children.Add(dataScroll, 0, 0);
			maingrid.Children.Add(btnStack, 0, 1);

			
			var dto = new T();
			BindingContext = _viewModel = new GenericViewModel<T>(Navigation, /*MasterForm,*/ this)
			{
				DtoObject = dto
			};
			MasterForm.DataObject = dto;
			MasterForm.PickerSourceProvider = new ComboBoxDataProvider(dto);
			// assignment required to change item visibility
			dto.DataForm = MasterForm;
			Title = Formtitle;
			
			Content = maingrid;

		}

		
		/// <summary>
		/// create new, empty form instance on appearing
		/// </summary>
		protected override void OnAppearing()
		{
			base.OnAppearing();
			_viewModel.DtoObject.ClearData();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}

	}
}