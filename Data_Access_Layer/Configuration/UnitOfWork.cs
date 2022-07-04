using Data_Access_Layer.Contracts;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Configuration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly YotorDatabaseContext _yotorDatabase;
        public ICustomerRepository Customers { get; private set; }
        public IBookingRepository Bookings { get; private set; }
        public ICarRepository Cars { get; private set; }
        public IFeedbackRepository Feedbacks { get; private set; }
        public ILandlordRepository Landlords { get; private set; }
        public IOrganizationRepository Organizations { get; private set; }
        public IRestrictionRepository Restrictions { get; private set; }
        public IDatabaseRepository Backups { get; private set; }


        public UnitOfWork()
        {
            _yotorDatabase = new YotorDatabaseContext();
            Customers = new CustomerRepository();
            Bookings = new BookingRepository();
            Cars = new CarRepository();
            Feedbacks = new FeedbackRepository();
            Landlords = new LandlordRepository();
            Organizations = new OrganizationRepository();
            Restrictions = new RestrictionRepository();
            Backups = new DatabaseRepository();

        }
        public async Task CompleteAsync()
        {
            await _yotorDatabase.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _yotorDatabase.DisposeAsync();
        }
    }
}
