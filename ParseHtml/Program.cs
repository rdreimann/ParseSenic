using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ParseHtml.models;

namespace ParseHtml
{
    public static class Program
    {
        private static readonly HttpClient MyClient = new HttpClient();

        public static async Task Main(string[] args)
        {
            while (true)
            {
                var requestModel = await GetRequestModel();
                var erg = Hex2Float(requestModel.ENERGY.GUI_GRID_POW);
                Console.WriteLine("KW = {0}", Math.Round(erg, 2));
                Thread.Sleep(1000);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static async Task<RequestModel> GetRequestModel()
        {
            var requestPoco = new RequestModel
            {
                ENERGY = new ENERGY
                {
                    STAT_STATE = "",
                    STAT_STATE_DECODE = "",
                    GUI_BAT_DATA_POWER = "",
                    GUI_INVERTER_POWER = "",
                    GUI_HOUSE_POW = "",
                    GUI_GRID_POW = "",
                    GUI_BAT_DATA_FUEL_CHARGE = "",
                    GUI_CHARGING_INFO = "",
                    GUI_BOOSTING_INFO = ""
                },
                WIZARD = new WIZARD
                {
                    CONFIG_LOADED = "",
                    SETUP_NUMBER_WALLBOXES = ""
                },
                SYS_UPDATE = new SYSUPDATE
                {
                    UPDATE_AVAILABLE = ""
                }
            };
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(requestPoco, serializeOptions);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await MyClient.PostAsync("http://192.168.2.82/lala.cgi", content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var responseBody = JsonSerializer.Deserialize<RequestModel>(responseString);
                return responseBody;
            }

            return null;
        }

        private static double Hex2Float(string hexString)
        {
            var cuttedHexString = hexString.Substring(3, hexString.Length - 3);
            var num = uint.Parse(cuttedHexString, NumberStyles.AllowHexSpecifier);
            var floatVals = BitConverter.GetBytes(num);
            var f = BitConverter.ToSingle(floatVals, 0);

            var erg = Math.Round(f * 100) / 100000;
            return erg;
        }
    }
}