using System;

namespace XamarinException.Dtos
{
	public class Converter
	{
		public static From_Device ConvertToFromDevice(Charge charge,  DateTime dt, decimal value, 
														string testid, int occurance)
		{
			var obj = new From_Device()
			{
				EQUIP_ID = charge.EQUIP_ID,
				A_VERSION = charge.A_VERSION,
				CH_ID = charge.CH_ID,
				KLASSE = "klasse",
				ZEIT = dt,
				ISTWERT = value,
				TESTS_ID = testid,
				OCCURANCE = occurance,
				RESOURCE_ID = "test"
			};
			return obj;
		}


	}

}
