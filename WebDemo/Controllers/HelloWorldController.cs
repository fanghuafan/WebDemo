using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    public class HelloWorldController : Controller
    {
        //
        // GET: /HelloWorld/Index
        public string Index()
        {
            //string domainAndName = Page.User.Identity.Name;
            //string[] infoes = domainAndName.Split(new char[1] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            //string userDomainName = "";
            //string userName = "";
            //if (infoes.Length > 1)
            //{
            //    userDomainName = infoes[0];
            //    userName = infoes[1];
            //}

            // 判断域用户是否登录
            var context = new System.Web.HttpContextWrapper(System.Web.HttpContext.Current);
            if (context.User.Identity.IsAuthenticated)
            {
                return "用戶已经登录！";
            }

            UserInfo userInfo = UserHelper.GetCurrentUserInfo(System.Web.HttpContext.Current);

            return "This is my <b>Default</b> action method..." + GetUserLoginName(System.Web.HttpContext.Current);
        }

        //
        // GET: /HelloWorld/Welcome
        public string Welcome()
        {
            return "This is the Welcome action method...";
        }

        /// <summary>
        /// 根据指定的HttpContext对象，获取登录名。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUserLoginName(HttpContext context)
        {
            if (context == null)
                return null;

            if (context.Request.IsAuthenticated == false)
                return null;

            string userName = context.User.Identity.Name;
            // 此时userName的格式为：UserDomainName\LoginName
            // 我们只需要后面的LoginName就可以了。

            string[] array = userName.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length == 2)
                return array[1];

            return null;
        }



    }
}