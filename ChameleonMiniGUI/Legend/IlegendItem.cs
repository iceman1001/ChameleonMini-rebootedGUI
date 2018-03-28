namespace ChameleonMiniGUI
{
    public interface IlegendItem
    {
        string ForeGroundColor { get; set; }
        string BackGroundColor { get; set; }
        string Description { get; set; }
    }

    public class LegendItem : IlegendItem
    {
        public string ForeGroundColor { get; set; }
        public string BackGroundColor { get; set; }
        public string Description { get; set; }
    }
}