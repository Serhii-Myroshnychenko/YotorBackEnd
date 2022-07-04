using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Contracts
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        Task CreateFeedbackAsync(Guid user_id, string name, DateTime date, string text);

    }
}
