using DevExpress.XamarinForms.DataForm;
using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using XamarinException.Dtos;

namespace XamarinException
{

	public class ComboBoxDataProvider : IPickerSourceProvider
	{

		private DtoBase dtoObject;

		public ComboBoxDataProvider(DtoBase dto)
		{
			dtoObject = dto;
		}


		// returns the available spgs, chs, impfguts...
		public IEnumerable GetSource(string propertyName)
		{
			return LocalDataStore.Instance?.Lookups.ChargenList.Select(x => x.FNAME);
		}	
	}

}
