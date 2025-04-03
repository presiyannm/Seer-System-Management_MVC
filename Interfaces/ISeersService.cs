﻿using Система_за_управление_на_гадатели_MVC.Models;

namespace Система_за_управление_на_гадатели_MVC.Interfaces
{
    public interface ISeersService
    {
        public Task<ICollection<Seer>> GetAllSeersAsync();

        public Task<ICollection<Enquiry>> GetAllSeerEnquriesAsync(string userId);

        public Task<string> UpdateEnquiryById(int enquiryId, string userId, string? answer);

        public Task<Seer> GetSeerByIdAsync(int seerId);

        public Task AddSeerRatingAsync(int seerId, double rating);
    }
}
