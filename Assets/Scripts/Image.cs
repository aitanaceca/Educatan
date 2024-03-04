namespace Scripts.CardImage
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageBase64 { get; set; }

        public Image()
        {

        }

        public Image(int id, string imageBase64)
        {
            Id = id;
            ImageBase64 = imageBase64;
        }
    }
}