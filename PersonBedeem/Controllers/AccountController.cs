using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PersonBedeem.Models;
using Facebook;
using System.Web.Security;

namespace PersonBedeem.Controllers
{
    public class AccountController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "1539813469663309",
                client_secret = "0883fd6699f9f387a575e12d28391751",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email,rsvp_event,user_likes,user_birthday, user_friends" // Add other permissions as needed
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "1539813469663309",
                client_secret = "0883fd6699f9f387a575e12d28391751",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            // Store the access token in the session
            Session["AccessToken"] = accessToken;

            // update the facebook client with the access token so 
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;

            // Get the user's information
            dynamic me = fb.Get("me?fields=first_name,last_name,id,email, friends, likes");
            Console.WriteLine(me);
            // Set the auth cookie
            FormsAuthentication.SetAuthCookie(me.email, false);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}