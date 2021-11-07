namespace Model
{
    public class SectionData
    {
        // Driver aan de linkerkant van de track
        public IParticipant Left { get; set; }
        // De afstand die de linkerdriver heeft afgelegd over de sectie
        public int DistanceLeft { get; set; }
        // Driver aan de rechterkant van de track
        public IParticipant Right { get; set; }
        // De afstand die de rechterdriver heeft afgelegd over de sectie
        public int DistanceRight { get; set; }
    }
}
