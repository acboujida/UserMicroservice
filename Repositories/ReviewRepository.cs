﻿using UserMicroservice.Data;
using UserMicroservice.Interfaces;
using UserMicroservice.Models;

namespace UserMicroservice.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _dataContext;

        public ReviewRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Review GetReview(int id)
        {
            return _dataContext.Reviews.FirstOrDefault(r => r.Id == id);
        }

        public ICollection<Review> GetReviews()
        {
            return _dataContext.Reviews.OrderBy(r => r.Id).ToList();
        }

        public bool ReviewExists(int id)
        {
            return _dataContext.Reviews.Any(r => r.Id == id);
        }

        public User GetUserOfReview(int id)
        {
            return _dataContext.Reviews.Where(r => r.Id == id).Select(r => r.User).FirstOrDefault();
        }

        public string GetBookOfReview(int id)
        {
            return _dataContext.Reviews.Where(r => r.Id == id).Select(r => r.BookId).FirstOrDefault();
        }

        public bool IsUserReviewOwner(int id, string userid)
        {
            return _dataContext.Reviews.Any(r => r.Id == id && r.User.Id == userid);
        }

        public bool CreateReview(Review review)
        {
            _dataContext.Add(review);
            return Save();
        }

        public bool UpdateReview(Review review)
        {
            _dataContext.Update(review);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0;
        }
    }
}
