using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinException.Properties;

namespace XamarinException
{
	
	

	public class LocalDataStore 
	{
		#region lookups to device
		public Lookups Lookups { get; private set; }
		#endregion
	
		
		public bool Sync()
		{
			Lookups = new Lookups();

			// read data
			var data = Resources.ResourceManager.GetString("TextFile1");
			var settings = new JsonSerializerSettings();
			settings.DateParseHandling = DateParseHandling.None;
			settings.Converters.Add(new MultiFormatDateConverter
			{
				DateTimeFormats = new List<string>() { "dd.MM.yyyy HH:mm:ss" }
			});
			var x = JsonConvert.DeserializeObject<List<Charge>>(data, settings);
			Lookups.ChargenList.AddRange(x);
			return true;
		}


		public SaveState Save(string[] data)
		{
			return SaveState.Success;
		}

		
		// Singleton implementation
		private static readonly LocalDataStore _instance;

		private LocalDataStore()
		{
		
		}

		static LocalDataStore()
		{
			_instance = new LocalDataStore();
		}
		public static LocalDataStore Instance => _instance;
	}

	class MultiFormatDateConverter : JsonConverter
	{
		public List<string> DateTimeFormats { get; set; }

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			string dateString = (string)reader.Value;
			DateTime date;
			foreach (string format in DateTimeFormats)
			{
				// adjust this as necessary to fit your needs
				if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
					return date;
			}
			throw new JsonException("Unable to parse \"" + dateString + "\" as a date.");
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
