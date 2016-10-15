using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ViedaSlimnicaProject.Models;
using System.ComponentModel.DataAnnotations;

namespace ViedaSlimnicaProject.Models
{
    // You can add User data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ClaimsIdentity GenerateUserIdentity(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
    public partial class Palata
    {
        public int PalatasID { get; set; }
        [StringLength(50)]
        public string Nodala { get; set; }
        [Range(1, 5)]//Nosakam ka kopā mums ir pieci stāvi lai nevar ievadit nepareizus skaitļus
        public Nullable<decimal> Stavs { get; set; }
        [Range(1, 4)]//Palatas ietilpiba nevar but lielaka par 4
        public Nullable<decimal> PalatasIetilpiba { get; set; }
        public int[] GultasNr { get; set; }
        public int[] PacientaID { get; set; }

        // const
        public Palata(int palID, string nod, int sta, int palaIet,int[] gulNr, int[] pacID)
        {
            PalatasID = palID;
            Nodala = nod;
            Stavs = sta;
            GultasNr = gulNr;
            PalatasIetilpiba = palaIet;
            PacientaID = pacID;
        }
    } 
     public partial class Pacients
    {
        public int PacientaID { get; set; }
        [StringLength(50)]
        public string Vards { get; set; }
        [StringLength(50)]
        public string Uzvards { get; set; }
        [StringLength(12)]
        public string PersKods { get; set; }
        [StringLength(50)]
        public string Simptomi { get; set; }
        [StringLength(50)]
        public string Nodala { get; set; } //Tas arī ir definēts klassē palātā bet nevar izmantot to pašu? 
        public int PalatasID { get; set; }
        public DateTime IerasanasDatums { get; set; }

        public Pacients(int pacID, string vard, string uzvar, string perskod, string simp, string nod, int palID, DateTime ier)
        {
            PacientaID = pacID;
            Vards = vard;
            Uzvards = uzvar;
            PersKods = perskod;
            Simptomi = simp;
            Nodala = nod;
            PalatasID = palID;
            IerasanasDatums = ier;
        }




    }
}

#region Helpers
namespace ViedaSlimnicaProject
{
    public static class IdentityHelper
    {
        // Used for XSRF when linking external logins
        public const string XsrfKey = "XsrfId";

        public const string ProviderNameKey = "providerName";
        public static string GetProviderNameFromRequest(HttpRequest request)
        {
            return request.QueryString[ProviderNameKey];
        }

        public const string CodeKey = "code";
        public static string GetCodeFromRequest(HttpRequest request)
        {
            return request.QueryString[CodeKey];
        }

        public const string UserIdKey = "userId";
        public static string GetUserIdFromRequest(HttpRequest request)
        {
            return HttpUtility.UrlDecode(request.QueryString[UserIdKey]);
        }

        public static string GetResetPasswordRedirectUrl(string code, HttpRequest request)
        {
            var absoluteUri = "/Account/ResetPassword?" + CodeKey + "=" + HttpUtility.UrlEncode(code);
            return new Uri(request.Url, absoluteUri).AbsoluteUri.ToString();
        }

        public static string GetUserConfirmationRedirectUrl(string code, string userId, HttpRequest request)
        {
            var absoluteUri = "/Account/Confirm?" + CodeKey + "=" + HttpUtility.UrlEncode(code) + "&" + UserIdKey + "=" + HttpUtility.UrlEncode(userId);
            return new Uri(request.Url, absoluteUri).AbsoluteUri.ToString();
        }

        private static bool IsLocalUrl(string url)
        {
            return !string.IsNullOrEmpty(url) && ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) || (url.Length > 1 && url[0] == '~' && url[1] == '/'));
        }

        public static void RedirectToReturnUrl(string returnUrl, HttpResponse response)
        {
            if (!String.IsNullOrEmpty(returnUrl) && IsLocalUrl(returnUrl))
            {
                response.Redirect(returnUrl);
            }
            else
            {
                response.Redirect("~/");
            }
        }
    }
}
#endregion
