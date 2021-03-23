using DevExpress.XamarinForms.DataForm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace XamarinException.Dtos
{
	[GenericForm(FormTitle ="Abernte", MenuOrder = 3)]
	public class AbernteDto : DtoBase
	{
		Charge charge;
		decimal? abernte_Continuous, abernte_Sample;
		readonly string Tests_Id_Abernte_Continuous = "00007362";
		readonly int Occurance_Abernte = 0;

		//private readonly Fermenter _fermenter;
		public AbernteDto()
		{
			
		}

		#region Fermenterinfo

		[DataFormItemPosition(RowOrder = 1, ItemOrderInRow = 1)]
		[DataFormDisplayOptions(LabelText = "BarCode (Equip ID)", LabelPosition = DataFormLabelPosition.Top)]
		[ClearData]
		public new string BarCode
		{
			get => base.BarCode;
			set
			{
				base.BarCode = value;
				OnPropertyChanged();
				if(value != null)
				{
					var lks = LocalDataStore.Instance.Lookups;
					var ch_from_equipId = lks.ChargenList.FirstOrDefault(x => x.EQUIP_ID == Decimal.Parse(base.BarCode));
					Fermenter = ch_from_equipId.FNAME;
					OnPropertyChanged(nameof(Fermenter));
				}
				
			}
		}

		[DataFormDisplayOptions(LabelText = "Fermenter", LabelPosition = DataFormLabelPosition.Top)]
		[DataFormItemPosition(RowOrder = 1, ItemOrderInRow = 2)]
		[DataFormComboBoxEditor(InplaceLabelText = "Fermenter auswählen", IsFilterEnabled = true)]
		[ClearData]
		public new string Fermenter 
		{ 
			get => base.Fermenter; 
			set 
			{
				base.Fermenter = value;
				if(value != null)
				{
					var lks = LocalDataStore.Instance.Lookups;
					charge = lks.ChargenList.First(x => x.FNAME.Equals(value));
					this.Batch = charge.CH_BEZ;
					var range = DateTime.Now - charge.START_T;
					this.Duration = $"{range.Hours + range.Days * 24}:{range.Minutes}";
				}
				
			} 
		}

		[DataFormItemPosition(RowOrder = 2, ItemOrderInRow = 2)]
		[DataFormDisplayOptions(LabelText = "Laufzeit", LabelPosition = DataFormLabelPosition.Top, /*GroupName = Declarations.FERMENTER_INFO,*/ IsReadOnly = true, EditorWidth = "2*")]
		[ClearData]
		public new string Duration { get => base.Duration; set { base.Duration = value; } }

		[DataFormItemPosition(RowOrder = 2, ItemOrderInRow = 1)]
		[DataFormDisplayOptions(LabelText = "Charge", /*GroupName = Declarations.FERMENTER_INFO,*/ IsReadOnly = true, LabelPosition = DataFormLabelPosition.Top, EditorWidth = "3*")]
		[ClearData]
		public new string Batch { get => base.Batch; set { base.Batch = value; } }
		#endregion


		[DataFormDisplayOptions(LabelText = "kontin. [g]")]
		[ClearData]
		public decimal? Abernte_Continuous { get => abernte_Continuous; set { abernte_Continuous = value; OnPropertyChanged(); } }

		[DataFormDisplayOptions(LabelText = "Probe [g]")]
		[DataFormNumericEditor]
		[ClearData]
		public decimal? Abernte_Sample { get => abernte_Sample; set { abernte_Sample = value; OnPropertyChanged(); } }

		
		public override void ClearData()
		{
			base.Delete(this);
		}

		public override async Task<SaveState> Save()
		{
			
			List<string> data = new List<string>();
			if(Abernte_Continuous.HasValue)
			{
				var obj1 = Converter.ConvertToFromDevice(charge, DateTime.Now, Abernte_Continuous.Value, 
															Tests_Id_Abernte_Continuous, Occurance_Abernte);
				data.Add(JsonConvert.SerializeObject(obj1));
			}
			if(Abernte_Sample.HasValue)
			{
				// this is copied from origninal implementation
				var obj2 = Converter.ConvertToFromDevice(charge, DateTime.Now.AddSeconds(10), 0,
															Tests_Id_Abernte_Continuous, Occurance_Abernte);
				data.Add(JsonConvert.SerializeObject(obj2));
				var obj3 = Converter.ConvertToFromDevice(charge, DateTime.Now.AddSeconds(40), Abernte_Sample.Value,
															Tests_Id_Abernte_Continuous, Occurance_Abernte);
				data.Add(JsonConvert.SerializeObject(obj3));
			}
			if (data.Count > 0)
				return LocalDataStore.Instance.Save(data.ToArray());
			else
				return SaveState.Cancel;
		}
	}
}
