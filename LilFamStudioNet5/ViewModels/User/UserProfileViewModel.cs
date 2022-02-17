using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.User
{
    public class UserProfileViewModel
    {
        public int id { get; set; }
        public string fio { get; set; }
        public string phone { get; set; }
        public int sex { get; set; }
        public string dateOfBirthday { get; set; }
        public string username { get; set; }
        public string comment { get; set; }
        public string parentFio { get;set; }
        public string parentPhone { get;set; }
        public int statusOfAdmin { get; set; }

        public UserProfileViewModel(int id, string fio, string phone, int sex, string dateOfBirthday, string username, string comment, string parentFio, string parentPhone, int statusOfAdmin)
        {
            this.id = id;
            this.fio = fio;
            this.phone = phone;
            this.sex = sex;
            this.dateOfBirthday = dateOfBirthday;
            this.username = username;
            this.comment = comment;
            this.parentFio = parentFio;
            this.parentPhone = parentPhone;
            this.statusOfAdmin = statusOfAdmin;
        }

        public UserProfileViewModel(int id, string fio, string phone, int sex, string dateOfBirthday, string username, string parentFio, string parentPhone)
        {
            this.id = id;
            this.fio = fio;
            this.phone = phone;
            this.sex = sex;
            this.dateOfBirthday = dateOfBirthday;
            this.username = username;
            this.parentFio = parentFio;
            this.parentPhone = parentPhone;
        }
    }
}
