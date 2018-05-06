using AutoMapper;
using Common;
using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaticResources;

namespace Model.Dao
{
    public class ContactDao
    {
        Entities db = null;
        public ContactDao()
        {
            db = new Entities();
        }

        public Contact GetActiveContact()
        {
            return db.Contacts.Single(x => x.Status == true);
        }

        public int InsertFeedBack(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }

        public ContactModel AddFeedback(ContactModel model, out string message)
        {
            ContactModel reModel = new ContactModel();
            message = null;
            try
            {
                var feedback = new Feedback();
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ContactModel, Feedback>();
                    cfg.CreateMap<Feedback, ContactModel>();
                });
                IMapper mapper = config.CreateMapper();
                feedback = mapper.Map<Feedback>(model);
                feedback.CreatedDate = DateTime.Now;
                var result = db.Feedbacks.Add(feedback);
                if (result!=null)
                {
                    reModel = mapper.Map<ContactModel>(result);
                }
                else
                {
                    message = Resources.SYSTEM_ERROR_ADDING_FEEDBACK_HAS_BEEN_FINISHED;
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at AddContact at ContactDao at  Model.Dao";
                message += subject + StringHelper.Parameters2ErrorString(ex);
            }
            return reModel;
        }

        public ContactModel GetFeedback(int Id=0)
        {
            var result = new ContactModel();            
            try
            {
                var model = db.Feedbacks.FirstOrDefault(w => w.ID == Id);
                if (model!=null)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<Feedback, ContactModel>();
                    });
                    IMapper mapper = config.CreateMapper();
                    result = mapper.Map<ContactModel>(model);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetFeedback at ContactDao at  Model.Dao";
                string message = subject + StringHelper.Parameters2ErrorString(ex);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return result;
        }
    }
}
