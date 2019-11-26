using System.Collections.Generic;

namespace HabrPostApi.Models
{
    public class HabrPost
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public HabrPostDetails Details { get; set; }
        public List<HabrHub> Hubs { get; set; }

        public HabrPost()
        { }
    }
}