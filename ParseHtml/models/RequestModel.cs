namespace ParseHtml.models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ENERGY
    {
        public string STAT_STATE { get; set; }
        public string STAT_STATE_DECODE { get; set; }
        public string GUI_BAT_DATA_POWER { get; set; }
        public string GUI_INVERTER_POWER { get; set; }
        public string GUI_HOUSE_POW { get; set; }
        public string GUI_GRID_POW { get; set; }
        public string GUI_BAT_DATA_FUEL_CHARGE { get; set; }
        public string GUI_CHARGING_INFO { get; set; }
        public string GUI_BOOSTING_INFO { get; set; }
    }

    public class WIZARD
    {
        public string CONFIG_LOADED { get; set; }
        public string SETUP_NUMBER_WALLBOXES { get; set; }
    }

    public class SYSUPDATE
    {
        public string UPDATE_AVAILABLE { get; set; }
    }

    public class RequestModel

    {
        public ENERGY ENERGY { get; set; }
        public WIZARD WIZARD { get; set; }
        public SYSUPDATE SYS_UPDATE { get; set; }
    }
}