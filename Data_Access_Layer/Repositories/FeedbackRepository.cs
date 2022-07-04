using Data_Access_Layer.Contracts;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        
        public FeedbackRepository() : base()
        {
            
        }

        public async Task CreateFeedbackAsync(Guid user_id, string name, DateTime date, string text)
        {
            var feedback = new Feedback();
            feedback.FeedbackId = Guid.NewGuid();
            feedback.UserId = user_id;
            feedback.Name = name;
            feedback.Date = date;
            feedback.Text = text;
            await dbSet.AddAsync(feedback);
            await _yotorDatabase.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(Guid id)
        {
            var feedback = await dbSet.Where(f => f.FeedbackId == id).FirstOrDefaultAsync();
            if (feedback != null)
            {
                dbSet.Remove(feedback);
                await _yotorDatabase.SaveChangesAsync();
            }
        }

    }
}
