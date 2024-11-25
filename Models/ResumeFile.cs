namespace Models
{
    public class ResumeFile
    {
        public int Id { get; set; }

        public string Filename { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.MinValue;

        public override bool Equals(object? obj)
        {
            return obj is ResumeFile file &&
                   Id == file.Id &&
                   Filename == file.Filename &&
                   Path == file.Path &&
                   Date == file.Date;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
