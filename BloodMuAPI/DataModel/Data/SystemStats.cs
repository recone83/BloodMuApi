namespace BloodMuAPI.DataModel.Data
{
    public class SystemStats
    {
        /// <summary>
        /// server status
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// number of players online
        /// </summary>
        public int players { get; set; }
        /// <summary>
        /// list characters online
        /// </summary>
        public List<string> playersList { get; set; }
        /// <summary>
        /// number of accounts on server
        /// </summary>
        public int Accounts { get; set; }
        /// <summary>
        /// number of characters on server
        /// </summary>
        public int Characters { get; set; }
    }
}
