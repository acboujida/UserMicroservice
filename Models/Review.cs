﻿namespace UserMicroservice.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public float Note { get; set; }
        public DateTime PostDate { get; set; }
        public User User { get; set; }
        public Livre Livre { get; set; }
    }
}
