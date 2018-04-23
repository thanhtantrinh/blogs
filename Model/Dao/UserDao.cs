using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;
using System.Configuration;
using Common;

namespace Model.Dao
{
    public class UserDao
    {
        OnlineShopEntities db = null;
        public UserDao()
        {
            db = new OnlineShopEntities();
        }

        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public long InsertForFacebook(User entity)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
            if (user == null)
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return user.ID;
            }

        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }

        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        public List<string> GetListCredential(string userName)
        {
            var user = db.Users.Single(x => x.UserName == userName);
            var data = (from a in db.Credentials
                        join b in db.UserGroups on a.Id equals b.Id
                        join c in db.Roles on a.RoleID equals c.Id
                        where b.Id == user.GroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID                            
                        });

            return new List<string>() { "Admin" };

        }
        public StatusLogin Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return StatusLogin.CustomerNotExist;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupID == GroupID.ADMIN || result.GroupID == GroupID.MOD)
                    {
                        if (result.Status != nameof(StatusEntity.Active))
                        {
                            return StatusLogin.NotActive;
                        }
                        else
                        {
                            if (result.Password == passWord)
                                return StatusLogin.Successful;
                            else
                                return StatusLogin.WrongPassword;
                        }
                    }
                    else
                    {
                        return StatusLogin.CustomerNotInGroup;
                    }
                }
                else
                {
                    if (result.Status != nameof(StatusEntity.Active))
                    {
                        return StatusLogin.NotActive;
                    }
                    else
                    {
                        if (result.Password == passWord)
                            return StatusLogin.Successful;
                        else
                            return StatusLogin.WrongPassword;
                    }
                }
            }
        }

        public string ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = nameof(StatusEntity.Locked);
            db.SaveChanges();
            return user.Status;
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
    }
}
