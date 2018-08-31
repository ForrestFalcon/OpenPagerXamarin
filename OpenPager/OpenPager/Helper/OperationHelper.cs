using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.AppCenter.Crashes;
using OpenPager.Models;
using Xamarin.Forms.Internals;

namespace OpenPager.Helper
{
    public static class OperationHelper
    {
        public static Operation MapFirebaseToOperation(IDictionary<string, object> data)
        {
            if (!data.ContainsKey("type") || !data["type"].Equals("operation")) return null;
                
            Operation operation = new Operation();

            foreach (KeyValuePair<string, object> pair in data)
            {
                string value = pair.Value.ToString();
                switch (pair.Key)
                {
                    case "key":
                        operation.Id = value;
                        break;
                    case "title":
                        operation.Title = value;
                        break;
                    case "message":
                        operation.Message = value;
                        break;
                    case "destination":
                        operation.Destination = value;
                        break;
                    case "destination_loc":
                        try
                        {
                            var split = value.Split(';');
                            operation.DestinationLat = float.Parse(split[0], CultureInfo.InvariantCulture.NumberFormat);
                            operation.DestinationLng = float.Parse(split[1], CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            Crashes.TrackError(e);
                        }
                        break;
                    case "timestamp":
                        try
                        {
                            operation.Time = UnixTimeStampToDateTime(long.Parse(value));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            Crashes.TrackError(e);
                        }
                        break;
                }
            }

            return operation;
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
