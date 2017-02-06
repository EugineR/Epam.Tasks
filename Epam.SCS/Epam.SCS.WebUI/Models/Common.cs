using System;
using Epam.SCS.LogicContracts;
using Epam.SCS.Logic;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Epam.SCS.Entities;

namespace Epam.SCS.WebUI.Models
{
    public static class Common
    {
         
        private static IUserLogic userLogic = new UserLogic();

        private static ISkillLogic skillLogic = new SkillLogic();

        private static IAccountLogic accountLogic = new AccountLogic();

        private static IPhotoLogic photoLogic = new PhotoLogic();

        public static IFilterLogic filterLogic = new FilterLogic();

        private static string dateFormat = ConfigurationManager.AppSettings["DateFormat"];
        public static IFilterLogic FilterBll
        {
            get
            {
                return filterLogic;
            }
        }

        public static IUserLogic UserBll
        {
            get
            {
                return userLogic;
            }
        }

        public static ISkillLogic SkillBll
        {
            get
            {
                return skillLogic;
            }
        }

        public static IAccountLogic AccountBll
        {
            get
            {
                return accountLogic;
            }
        }

        public static IPhotoLogic PhotoBll
        {
            get
            {
                return photoLogic;
            }
        }

        public static string DateFormat
        {
            get
            {
                return dateFormat;
            }
        }
    }
}