namespace Model
{
    public class Section
    {
        public SectionTypes SectionType { get; set; }

        public enum SectionTypes
        {
            Straight,
            LeftCorner,
            RightCorner,
            StartGrid,
            Finish
        }
    }
}
