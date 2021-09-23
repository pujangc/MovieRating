namespace MoviesRatingApp1.Models
{
    using System;
    public class Movie
    {
        public int MovieId { get; set; }
        public String Title { get; set; }
        public int YearOfRelease { get; set; }
        public String Genre { get; set; }
        public float RunningTime { get; set; }
        public float? AverageRating { get; set; }
    }
}