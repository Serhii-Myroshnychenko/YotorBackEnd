using Data_Access_Layer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Configuration
{
    public interface IUnitOfWork
    {
        IBookingRepository Bookings { get; }
        ICarRepository Cars { get; }
        ICustomerRepository Customers { get; }
        IFeedbackRepository Feedbacks { get; }
        ILandlordRepository Landlords { get; }
        IOrganizationRepository Organizations { get; }
        IRestrictionRepository Restrictions { get; }
        IDatabaseRepository Backups { get; }
        

        Task CompleteAsync();
        void Dispose();
    }
}
