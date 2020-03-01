namespace HabrPostApi.Settings
{
    public class HabrSelector : IHabrSelector
    {
        public string Post { get; set; }
        public string Title { get; set; }
        public string Footer { get; set; }
        public string Hub { get; set; }
        public string Mark { get; set; }
        public string Bookmark { get; set; }
        public string View { get; set; }
        public string Comment { get; set; }

        public HabrSelector()
        { }
    }
}