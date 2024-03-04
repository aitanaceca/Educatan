namespace Scripts.RandomCardText
{
    public class Text
    {
        public int Id { get; set; }
        public string CardText { get; set; }

        public Text()
        {

        }

        public Text(int id, string cardText)
        {
            Id = id;
            CardText = cardText;
        }
    }
}