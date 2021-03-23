using System;
using Xamarin.Forms;

namespace XamarinException
{
	
	public static class Declarations
	{
	

		// https://localhost:5001/api/sync/synctodevice
		private static bool bUseEmulator = true;

		// this app is only developed for android
		public static string BaseAdr = bUseEmulator ? "10.0.2.2" : "localhost";
		public static string BasePort = bUseEmulator ? "5000" : "44325";

		public static string BaseAddress = $"http://{BaseAdr}:{BasePort}";


		// URL of REST service
		public static string SyncToDeviceUrl = $"{BaseAddress}" + "/api/sync/SyncToDevice";
		public static string SyncFromDeviceUrl = $"{BaseAddress}" + "/api/sync/SyncFromDevice";
		public static string ApiTest = $"{BaseAddress}" + "/api/sync/ApiTest";

	}



	[Flags]
	public enum DataType : short
	{
		Lookup = 0,
		From_Device = 1,
		Impfgut_From_Device = 2,
		ImpfgutMenge_From_Device = 4,
		VarDos_From_Device = 8,
		Cmd_From_Deivce = 16,
		Anstz_From_Device = 32,
		SyncDateTime = 64,
		DataFiles = From_Device | ImpfgutMenge_From_Device | Impfgut_From_Device | VarDos_From_Device | Cmd_From_Deivce | Anstz_From_Device
	}

	public enum SaveState
	{
		Success,
		Cancel,
		Error
	}

}
