using AutoMapper;
using Business_Logic_Layer.Constructors;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.BLL
{
    public class FeedbackBLL
    {
        private readonly Data_Access_Layer.Configuration.IUnitOfWork _iUnitOfWorkDAL;
        private readonly Mapper _feedbackMapper;

        public FeedbackBLL()
        {
            _iUnitOfWorkDAL = new Data_Access_Layer.Configuration.UnitOfWork();
            _feedbackMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Feedback, FeedbackModel>().ReverseMap()));
        }

        public async Task<IEnumerable<FeedbackModel>> GetFeedbacksAsync()
        {
            IEnumerable<FeedbackModel> feedbackModels = null;
            var feedbacks = await _iUnitOfWorkDAL.Feedbacks.GetAllAsync();
            feedbackModels = _feedbackMapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackModel>>(feedbacks);
            return feedbackModels;
        }

        public async Task<FeedbackModel> GetFeedbackAsync(Guid id,Guid userId)
        {
            FeedbackModel feedbackModel = null;
            bool isAdmin = await _iUnitOfWorkDAL.Customers.IsAdminAsync(userId);
            if (isAdmin == true)
            {
                var feedback = await _iUnitOfWorkDAL.Feedbacks.GetByIdAsync(id);
                feedbackModel = _feedbackMapper.Map<Feedback, FeedbackModel>(feedback);
            }
            return feedbackModel;
        }

        public async Task<string> CreateFeedbackAsync(FeedbackConstructor feedbackConstructor, Guid userId)
        {
            try
            {
                DateTime time = DateTime.Now;
                DateTime date1 = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.Millisecond);
                //string a = time.ToString("yyyy-MM-dd HH:mm:ss.fff");
                await _iUnitOfWorkDAL.Feedbacks.CreateFeedbackAsync(userId, feedbackConstructor.Name, date1, feedbackConstructor.Text);
                return "Ok";
            }
            catch(Exception ex) { return ex.Message; } 
        }
        public async Task<string> DeleteFeedbackAsync(Guid id, Guid userId)
        {
            try
            {
                bool isAdmin = await _iUnitOfWorkDAL.Customers.IsAdminAsync(userId);
                if (isAdmin == true)
                {
                    await _iUnitOfWorkDAL.Feedbacks.DeleteAsync(id);
                    return "Ok";
                }
                else
                {
                    return "Вы не являетесь администратором";
                }
            }
            catch(Exception ex) { return ex.Message; }
        }
    }
}
