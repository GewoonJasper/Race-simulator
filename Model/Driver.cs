namespace Model
{
    public class Driver : IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public Car Car { get; set; }
        public int Laps { get; set; }
        public IParticipant.TeamColors TeamColor { get; set; }

        public Driver(string name, int points, IParticipant.TeamColors teamcolor)
        {
            Name = name;
            Points = points;
            TeamColor = teamcolor;
            Laps = 0;

            Car = new Car(0,0,0,false);
        }
    }
}
