using LilFamStudioNet5.Components;
using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.User;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class UserService
    {
        public ApplicationDbContext _dbc;
        public UserService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }


        public async Task<User> findById(int id)
        {
            return await _dbc.Users.FirstOrDefaultAsync(p => p.id == id);
        }
        public async Task<User> findByUsername(string username)
        {
            return await _dbc.Users.FirstOrDefaultAsync(p => p.username == username);
        }

        public async Task<bool> isUsernameAlreadyExist(string username) => await _dbc.Users.Where(p => p.username == username).AnyAsync();

        public bool isAlreadyExistByUsernameExceptId(int id, string username)
        {
            if (_dbc.Users.Where(p => p.username == username).Where(p => p.id != id).Any())
            {
                return true;
            }
            return false;
        }

        public async Task<User> add(string fio, string phone, string comment)
        {
            User user = new User();
            user.fio = fio;
            user.phone = phone;
            user.comment = comment;
            user.dateOfAdd = DateTime.Now;
            user.authKey = RandomComponent.RandomString(32);

            await _dbc.Users.AddAsync(user);
            await _dbc.SaveChangesAsync();

            return user;
        }


        public async Task<User> add(string username, string password = null)
        {
            User user = new User();
            user.username = username;
            user.dateOfAdd = DateTime.Now;
            //User.password = BCrypt.Net.BCrypt.HashPassword((password != null ? password : RandomComponent.RandomString(6)));
            user.password = null;
            user.authKey = RandomComponent.RandomString(32);

            await _dbc.Users.AddAsync(user);
            await _dbc.SaveChangesAsync();

            return user;
        }

        public async Task<User> add(
            string username, 
            string password, 
            string fio,
            string phone,
            int sex,
            DateTime? dateOfBirthday,
            string parentFio,
            string parentPhone
        )
        {
            User user = new User();
            user.username = username;
            user.fio = fio;
            user.phone = phone;
            user.password = BCrypt.Net.BCrypt.HashPassword((password != null ? password : RandomComponent.RandomString(6)));
            user.sex = sex;
            user.dateOfBirthday = dateOfBirthday;
            user.parentFio = parentFio;
            user.parentPhone = parentPhone;
            user.dateOfAdd = DateTime.Now;
            user.authKey = RandomComponent.RandomString(32);

            await _dbc.Users.AddAsync(user);
            await _dbc.SaveChangesAsync();

            return user;
        }


        public async Task<bool> update(
            User user,
            string fio,
            string phone,
            int sex,
            string username,
            string comment,
            string parentFio,
            string parentPhone,
            string passwordNew
        )
        {
            user.fio = fio;
            user.phone = phone;
            user.sex = sex;
            user.username = username;
            user.comment = comment;
            user.parentFio = parentFio;
            user.parentPhone = parentPhone;
            if (passwordNew != null) { 
                user.password = BCrypt.Net.BCrypt.HashPassword(passwordNew);
                user.authKey = RandomComponent.RandomString(32);
                user.accessToken = RandomComponent.RandomString(32);
            }
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<User> update(
            User user,
            string fio,
            string phone,
            int sex,
            DateTime dateOfBirthday,
            string username,
            string parentFio,
            string parentPhone,
            string passwordNew
        )
        {
            user.fio = fio;
            user.phone = phone;
            user.sex = sex;
            user.dateOfBirthday = dateOfBirthday;
            user.username = username;
            user.parentFio = parentFio;
            user.parentPhone = parentPhone;
            if (passwordNew != null)
            {
                user.password = BCrypt.Net.BCrypt.HashPassword(passwordNew);
                user.authKey = RandomComponent.RandomString(32);
                user.accessToken = RandomComponent.RandomString(32);
            }
            await _dbc.SaveChangesAsync();
            return user;
        }

        public async Task<bool> updateUserStatusOfAdmin(User user, int status)
        {
            if (status != 0 && status != 1) return false;
            user.statusOfAdmin = status;
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> updateLastTimeVisit(User user, DateTime? dateTimeOfVisit = null)
        {
            user.dateOfLastVisit = (dateTimeOfVisit != null ? dateTimeOfVisit : DateTime.Now);
            await _dbc.SaveChangesAsync();
            return true;
        }


        public async Task<bool> updateUserForgetCode(User user, string code)
        {
            user.forgetCode = code;
            if (user.forgetDateOfLastTry.Value.AddMinutes(20) < DateTime.Now)
            {
                user.forgetCount = 1;
            }
            else
            {
                user.forgetCount++;
            }
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> updateCheckQR(User user, string checkQR)
        {
            if (checkQR == null || user == null) return false;
            user.checkQr = checkQR;
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> forgetUpdateCount(User user, int count)
        {
            user.forgetCount = count;
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> forgetCheckCode(int id, string code)
        {
            User user = await findById(id);
            if (user == null) return false;
            if (user.forgetCode != code) return false;
            return true;
        }

        public async Task<bool> updatePassword(User user, string password)
        {
            user.password = BCrypt.Net.BCrypt.HashPassword(password);
            user.authKey = RandomComponent.RandomString(32);
            await _dbc.SaveChangesAsync();
            return true;
        }




        public async Task<List<User>> searchUsers(UserSearchDTO userSearchDTO)
        {
            userSearchDTO.page--;
            int take = 30;
            int skip = userSearchDTO.page * take;
            if (!string.IsNullOrEmpty(userSearchDTO.queryString))
            {
                return await _dbc.Users.FromSql("SELECT * FROM user WHERE username LIKE {0} OR fio LIKE {0} OR phone LIKE {0} ORDER BY Id DESC LIMIT {1}, {2}", "%" + userSearchDTO.queryString + "%", skip, take)
                    .ToListAsync();
            }
            else
            {
                IQueryable<User> q = _dbc.Users.OrderByDescending(p => p.id);
                q = q.Skip(skip).Take(take);
                return await q.ToListAsync();
            }
        }

        public async Task<List<User>> searchUsers(string queryString)
        {
            return await _dbc.Users
                .FromSql("SELECT * FROM user WHERE username LIKE {0} OR fio LIKE {0} OR phone LIKE {0} ORDER BY Id DESC LIMIT 10", "%" + queryString + "%")
                .ToListAsync();
        }


        public async Task<int> searchCount(UserSearchDTO userSearchDTO)
        {
            if (!string.IsNullOrEmpty(userSearchDTO.queryString))
            {
                return await _dbc.Users.FromSql("SELECT * FROM user WHERE username LIKE {0} OR fio LIKE {0} OR phone LIKE {0}", "%" + userSearchDTO.queryString + "%")
                    .CountAsync();
            }
            else
            {
                IQueryable<User> q = _dbc.Users.OrderByDescending(p => p.id);
                return await q.CountAsync();
            }
        }




        public async Task<bool> delete(int id)
        {
            User user = await findById(id);
            if (user == null) return false;
            _dbc.Users.Remove(user);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(User user)
        {
            _dbc.Users.Remove(user);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
