using DevExpress.XamarinForms.DataForm;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace XamarinException.Dtos
{
	
	public abstract class DtoBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string name = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		

		
		public DataFormView DataForm { get => dataform; set { dataform = value; OnPropertyChanged(); } }

		#region private fields common
		string fermenter;
		string duration;
		dynamic container; // aka Spritzgefäss
		string batch;
		//string chargenid;
		string barcode;
		DataFormView dataform;
		#endregion

		#region common
		
		public string Fermenter { get => fermenter; set { fermenter = value; OnPropertyChanged(); } }
		public string Duration { get => duration ; set { duration = value; OnPropertyChanged(); } }
		public string Batch { get => batch; set { batch = value; OnPropertyChanged(); } }
		
		public string BarCode { get => barcode; set { barcode = value; OnPropertyChanged(); } }

		#endregion

		
		public abstract Task<SaveState> Save();
		public abstract void ClearData();
		
		protected void Delete(object obj)
		{
			var props = obj.GetType().GetProperties().Where(x => Attribute.IsDefined(x, typeof(ClearDataAttribute)));
			foreach (var prop in props)
			{
				prop.SetValue(obj, null);
			}
		}

	}
}
