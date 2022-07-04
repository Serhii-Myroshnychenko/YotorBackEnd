using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Utils
{
    public static class CarSelection
    {
        public static double GetDistanceOfSomeCar(string carsLocation, string usersLocation)
        {
            var result = "";
            string url = $"https://maps.googleapis.com/maps/api/distancematrix/xml?origins={usersLocation}&destinations={carsLocation}&key=";
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);
                    result = dsResult.Tables["distance"].Rows[0]["text"].ToString();
                    result = result.Replace(" km", "").Replace(" m", "");
                    if (result.Contains('.'))
                    {
                        result = result.Replace(".", ",");
                    }
                    return double.Parse(result);
                }
            }
            return -1;
        }
        public static string GetUsersLocationFromLatitudeAndLongitude(string latitude, string longitude)
        {
            using (WebClient wc = new())
            {
                var json = wc.DownloadString($"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key=");
                var data = JObject.Parse(json);
                var response = data.ToString();
                var start = response.IndexOf("\"formatted_address\": \"") + 22;
                var endIndex = response.IndexOf("\",", start);
                var res = response.Substring(start, endIndex - start);
                return res;
            }

        }

        public static CarModel GetTheNearestCar(IEnumerable<CarModel> cars, string usersLocation)
        {
            List<CarModel> result = null;
            double distance = double.MaxValue;
            foreach(var car in cars)
            {
                var currentDistance = GetDistanceOfSomeCar(car.Address, usersLocation);
                if (car.City == usersLocation)
                {
                    currentDistance = 1;
                }

                if(currentDistance < distance)
                {
                    distance = currentDistance;
                    result = new List<CarModel> { car };
                }
                else if(currentDistance == distance)
                {
                    result.Add(car);
                }
                else
                {
                    continue;
                }
            }

            return result.MinBy(k => k.Price);
        }
    }
}
