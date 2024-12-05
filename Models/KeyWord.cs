namespace Models
{
    public class KeyWord
    {
        public int Id { get; set; }

        string word = string.Empty;

        public string Word
        {
            get { return word; }
            set { word = value; }
        }
    }
}
