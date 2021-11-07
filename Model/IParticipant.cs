using System;

namespace Model
{
    public interface IParticipant
    {
        public String Name { get; set; }
        public int Points { get; set; }
        public Car Car { get; set; }
        public int Laps { get; set; }
        public TeamColors TeamColor { get; set; }

        public enum TeamColors
        {
            Red,
            Green,
            Yellow,
            Grey,
            Blue
        }
    }
}
