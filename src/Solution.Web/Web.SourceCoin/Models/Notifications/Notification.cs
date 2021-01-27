namespace Web.SourceCoin.Models.Notifications
{
    public class Notification
    {
        public Notification()
        {
            EnableICO = false;
        }
        public int Id { get; set; }
        public bool EnableICO { get; set; }
        public string Meg { get; set; }
    }
}