namespace QuanLyPhongTro.Models.Domain
{
    public class PosterRecently
    {
        public string PosterId { get; set; }
        public string TimeRecently { get; set;}

        public List<PosterRecently> Posters { get; set;} = new List<PosterRecently>();
    }
}
