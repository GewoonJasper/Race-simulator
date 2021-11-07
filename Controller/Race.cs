using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Model;
using Timer = System.Timers.Timer;

namespace Controller
{
    public class Race
    {
        public event EventHandler<DriversChangedEventArgs> DriversChanged;
        private DriversChangedEventArgs dcArgs;

        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Timer _timer { get; set; }
        private Random _random;
        public bool IsGepauzeerd { get; private set; }
        
        private Dictionary<Section, SectionData> _positions;
        public Dictionary<Section, SectionData> Positions
        {
            get
            {
                return _positions;
            }
            private set
            {
                _positions = value;
            }
        }

        private int _sectionLength;
        private int _raceLenght;
        private int _maxLaps;
        private int _points { get; set; }

        // Constructor klasse Race
        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            StartTime = DateTime.Now;
            _random = new Random(DateTime.Now.Millisecond);

            _timer = new Timer(500);
            _timer.Elapsed += OnTimedEvent;
            
            dcArgs = new DriversChangedEventArgs() { Track = track };

            _positions = new Dictionary<Section, SectionData>();
            Positions = new Dictionary<Section, SectionData>();

            Positions = GetStartPositions(track, participants);
            RandomizeEquipment();
            SetRaceParams(track);

            StartTimer();
        }

        // Returnt sectiondata van de gevraagde sectie
        // Als deze niet bestaat, maakt het een nieuwe key aan in de _positions dictionary met de sectiondata
        public SectionData GetSectionData(Section section, Dictionary<Section, SectionData> position)
        {
            if (section != null)
            {
                if (!position.ContainsKey(section))
                {
                    SectionData data = new SectionData();
                    position.Add(section, data);
                }

                return position[section];
            }

            return null;
        }

        //Randomized de equipment van de car per driver voor verschillende races
        public void RandomizeEquipment()
        {
            foreach (Driver driver in Participants)
            {
                driver.Car.Performance = _random.Next(3, 5);
                driver.Car.Speed = _random.Next(10, 15);
                driver.Car.Quality = (int)(100 - Math.Pow(2, driver.Car.Speed) / 1000); // Hoe hoger de snelheid, hoe slechter de kwaliteit
            }
        }

        //Zet de startposities van elke driver, totdat de startgrids op zijn, of er geen drivers meer zijn om te tekenen
        //public void SetStartPositions(Track track, List<IParticipant> participants)
        public Dictionary<Section, SectionData> GetStartPositions(Track track, List<IParticipant> participants)
        {
            int Drivers = participants.Count(); //Het totale aantal drivers
            int Driver = 0; // De index van de variabele drivers, deze zet hij op de plek neer
            List<IParticipant> p = new List<IParticipant>(); // Het aantal spelers op de baan

            Dictionary<Section, SectionData> tempDictionary = new Dictionary<Section, SectionData>();

            // Hij gaat de baan achterstevoren langs omdat de drivers dan vooraan komen te staan
            for (int j = track.Sections.Count - 1; j >= 0; j--)
            {
                Section sectie = track.Sections.ElementAt(j);

                if (sectie.SectionType.Equals(Section.SectionTypes.StartGrid)) // Kijkt eerst of de sectie wel een startgrid is
                {
                    SectionData GridData = GetSectionData(sectie, _positions);
                    int i = 0; // tijdelijke variabele, hij mag maar 2 keer drivers per sectie neerzetten

                    while (i < 2)
                    {
                        if (Drivers > Driver) // Zolang er meer drivers zijn dan dat hij al gedaan heeft
                        {
                            if (GridData.Left == null)
                            {
                                GridData.Left = participants[Driver];
                                //_positions[sectie] = GridData;
                                tempDictionary[sectie] = GridData;
                            }
                            else if (GridData.Right == null)
                            {
                                GridData.Right = participants[Driver];
                                //_positions[sectie] = GridData;
                                tempDictionary[sectie] = GridData;
                            }
                            p.Add(participants[Driver]);
                            Driver++;
                        }
                        i++;
                    }

                }

                this.Participants = p;
            }

            return tempDictionary;

        }

        //Zet de lengte van elke sectie, de gehele raceafstand en het maximaal aantal laps dat gereden moet worden
        public void SetRaceParams(Track track)
        {
            foreach (IParticipant p in Participants)
            {
                p.Laps = 0;
            }
            _sectionLength = 80;
            _raceLenght = 5000;
            _maxLaps = 2;//_raceLenght / (track.Sections.Count * _sectionLength);
            if (_maxLaps < 2)
            {
                _maxLaps = 2;
            }

            _points = Participants.Count * 2;
        }

        // Start de timer, en dus de race
        public void StartTimer()
        {
            _timer.Start();
            IsGepauzeerd = false;
        }

        // Stopt de timer wanneer er een race is afgelopen
        public void StopTimer()
        {
            _timer.Stop();
            IsGepauzeerd = true;
        }

        public string StartPauzeer()
        {
            if (IsGepauzeerd)
            {
                return "Start race";
            }

            return "Pauzeer race";
        }

        // Wanneer de timer bij een bepaald limiet aangekomen is, wordt deze methode aangeroepen
        // De methode moet de drivers positions updaten gekeken naar de car equipment van de drivers
        // Wanneer de driver een nieuwe sectie binnenkomt, moet dit ook visueel te zien zijn
        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            for (int j = Track.Sections.Count - 1; j >= 0; j--)
            {
                Section currentSection = Track.Sections.ElementAt(j);
                Section nextSection = Track.Sections.ElementAt(0);

                if (j != Track.Sections.Count - 1)
                {
                    nextSection = Track.Sections.ElementAt(j + 1);
                }

                SectionData currentSectionData = GetSectionData(currentSection, _positions);
                SectionData nextSectionData = GetSectionData(nextSection, _positions);

                if (currentSectionData.Left != null)
                {
                    if (currentSectionData.Left.Laps <= _maxLaps)
                    {
                        checkIfCarBroken(currentSectionData.Left);
                        if (!currentSectionData.Left.Car.IsBroken)
                        {
                            
                            int distanceDriven = currentSectionData.Left.Car.Speed *
                                                 currentSectionData.Left.Car.Performance;

                            if (currentSectionData.DistanceLeft + distanceDriven > _sectionLength)
                            {
                                if (nextSectionData.Left == null)
                                {
                                    if (nextSection.SectionType.Equals(Section.SectionTypes.Finish))
                                    {
                                        currentSectionData.Left.Laps++;
                                    }

                                    nextSectionData.Left = currentSectionData.Left;
                                    nextSectionData.DistanceLeft =
                                        distanceDriven - (_sectionLength - currentSectionData.DistanceLeft);
                                    currentSectionData.Left = null;
                                    currentSectionData.DistanceLeft = 0;
                                }
                                else if (nextSectionData.Right == null)
                                {
                                    if (nextSection.SectionType.Equals(Section.SectionTypes.Finish))
                                    {
                                        currentSectionData.Left.Laps++;
                                    }

                                    nextSectionData.Right = currentSectionData.Left;
                                    nextSectionData.DistanceRight =
                                        distanceDriven - (_sectionLength - currentSectionData.DistanceLeft);
                                    currentSectionData.Left = null;
                                    currentSectionData.DistanceLeft = 0;
                                }
                            }
                            else
                            {
                                currentSectionData.DistanceLeft += distanceDriven;
                            }
                        }
                    }
                    else
                    {
                        currentSectionData.Left.Points += _points;
                        _points -= 2;
                        currentSectionData.Left = null;
                    }
                }

                if (currentSectionData.Right != null)
                {
                    if (currentSectionData.Right.Laps <= _maxLaps)
                    {
                        checkIfCarBroken(currentSectionData.Right);
                        if (!currentSectionData.Right.Car.IsBroken)
                        {

                            int distanceDriven = currentSectionData.Right.Car.Speed *
                                                 currentSectionData.Right.Car.Performance;

                            if (currentSectionData.DistanceRight + distanceDriven > _sectionLength)
                            {
                                if (nextSectionData.Right == null)
                                {
                                    if (nextSection.SectionType.Equals(Section.SectionTypes.Finish))
                                    {
                                        currentSectionData.Right.Laps++;
                                    }

                                    nextSectionData.Right = currentSectionData.Right;
                                    nextSectionData.DistanceRight =
                                        distanceDriven - (_sectionLength - currentSectionData.DistanceRight);
                                    currentSectionData.Right = null;
                                    currentSectionData.DistanceRight = 0;
                                }
                                else if (nextSectionData.Left == null)
                                {
                                    if (nextSection.SectionType.Equals(Section.SectionTypes.Finish))
                                    {
                                        currentSectionData.Right.Laps++;
                                    }

                                    nextSectionData.Left = currentSectionData.Right;
                                    nextSectionData.DistanceLeft =
                                        distanceDriven - (_sectionLength - currentSectionData.DistanceRight);
                                    currentSectionData.Right = null;
                                    currentSectionData.DistanceRight = 0;
                                }
                            }
                            else
                            {
                                currentSectionData.DistanceRight += distanceDriven;
                            }
                        }
                    }
                    else
                    {
                        currentSectionData.Right.Points += _points;
                        _points -= 2;
                        currentSectionData.Right = null;
                    }
                }
            }
            
            // Event, roept de methode OnDriversChanged aan in Visualisation, welke de baan tekent
            DriversChanged?.Invoke(this, dcArgs);
            
            if (_points == 0)
            {
                RaceOver();
            }
        }

        public void checkIfCarBroken(IParticipant p)
        {
            if (!p.Car.IsBroken && _random.Next(1, 100) >= p.Car.Quality)
            {
                p.Car.IsBroken = true;
            }

            if (p.Car.IsBroken && _random.Next(1, 100) >= p.Car.Quality / 2)
            {
                p.Car.IsBroken = false;
                p.Car.Quality--;
            }

            //int i = 0;
            //foreach (IParticipant d in Participants)
            //{
            //    Console.SetCursorPosition(0, i);
            //    Console.WriteLine($"{d.Name}: Speed: {d.car.Speed * d.car.Performance}, Quality: {d.car.Quality}, lap {d.laps}");

            //    i++;
            //}
        }

        public void RaceOver()
        {
            StopTimer();
            DriversChanged = null;
            
            Data.NextRace(Data.Competition);
        }
    }
}
