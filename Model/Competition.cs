using System.Collections.Generic;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        public Competition()
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
        }

        // returnt de volgende track in de queue (en verwijderd deze gelijk uit de queue)
        // als er geen items meer in de queue zijn, returnt hij null
        public Track NextTrack()
        {
            if (Tracks.Count <= 0)
            {
                return null;
            } else
            {
                return Tracks.Dequeue();
            }
        }
    }
}
