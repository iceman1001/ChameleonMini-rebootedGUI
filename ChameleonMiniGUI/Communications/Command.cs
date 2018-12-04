namespace ChameleonMiniGUI
{
    /**
     * Class to parse and decode messages from device
     */
    public class Command
    {
        public string CmdText { get; set; }

        public Command()
        {
        }

        public Command TryParse(string cmdtext)
        {
            return new Command();
        }
    }
}