using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinException.Dtos
{
	public class GenericFormAttribute : Attribute
	{
		public int MenuOrder { get; set; }
		public string FormTitle { get; set; }
	}

	public class ClearDataAttribute : Attribute
	{

	}
}
