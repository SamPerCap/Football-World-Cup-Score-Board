namespace ScoreBoardLibrary.Models
{
    public class Audit
    {
        public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
    }
}
