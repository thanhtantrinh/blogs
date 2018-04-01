using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PagedList;
using Common;

namespace Model.Repository
{
    public class ContentRepo: BaseRepository
    {

        public ContentRepo() : base()
        {

        }

        public IEnumerable<v_Content> GetContentPaging(ContentFilter filter, int pageIndex = 1, int pageSize = 20, string sortby = "")
        {
            IQueryable<v_Content> model = entities.v_Content;

            try
            {
                if (!string.IsNullOrEmpty(filter.SearchString))
                {
                    string searchString = filter.SearchString.Trim();
                    model = model.Where(x => x.Name.Contains(searchString));
                }

                if (filter.CategoryID > 0)
                {
                    model = model.Where(x => x.CategoryID == filter.CategoryID);
                }

                if (!String.IsNullOrWhiteSpace(filter.Status))
                {
                    switch (filter.Status.Trim())
                    {
                        case nameof(StatusEntity.Active):
                            model = model.Where(w => w.Status == nameof(StatusEntity.Active));
                            break;
                        case nameof(StatusEntity.Locked):
                            model = model.Where(w => w.Status == nameof(StatusEntity.Locked));
                            break;
                        case nameof(StatusEntity.Deleted):
                            model = model.Where(w => w.Status == nameof(StatusEntity.Deleted));
                            break;
                        default:
                            model = model.Where(w => w.Status != nameof(StatusEntity.Deleted));
                            break;
                    }
                }

                if (!String.IsNullOrWhiteSpace(sortby))
                {
                    model = model.OrderByDescending(x => x.CreatedDate);
                }
                else
                {
                    model = model.OrderByDescending(x => x.CreatedDate);
                }
            }
            catch (Exception ex)
            {
                string subject = "Error " + SiteSetting.SiteName + " at GetContentPaging at ContentRepo at Model.Repository";
                string message = StringHelper.Parameters2ErrorString(ex, conn);
                MailHelper.SendMail(SiteSetting.EmailAdmin, subject, message);
            }
            return model.ToPagedList(pageIndex, pageSize);

        }
    }
}
