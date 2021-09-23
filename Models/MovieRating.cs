namespace MoviesRatingApp1.Models
{
    using System;
    using System.Collections.Generic;
    public class MovieRating
    {
        public int MovieRatingId { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public float? Rating { get; set; }
    }
}