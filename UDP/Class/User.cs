namespace UDP.Class
{
    public class User
    {
        public User()
        {
            Alive = true;
            Retry = 0;
            Leader = false;
        }

        public int Priority { get; set; }
        public string Ip { get; set; }
        public bool Alive { get; set; }
        public bool Leader { get; set; }
        public int Retry { get; set; }
    }
}