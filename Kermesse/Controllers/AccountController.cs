using Kermesse.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web.Security;

namespace Kermesse.Controllers
{
    public class AccountController : Controller
    {
        private BDKermesseEntities db = new BDKermesseEntities();

        // GET: Account
        public ActionResult Index()
        { 

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.pwd = HashPassword(usuario.pwd);
                db.Usuarios.Add(usuario);
                db.SaveChanges();

                ModelState.Clear();
            }

            ViewBag.Message = "El usuario " + usuario.nombres + " " + usuario.apellidos + " ha sido registrado con éxito.";
            return View();
        }

        public ActionResult Login()
        {
            if (Session["UserID"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario, string ReturnUrl = "/")
        {
           
            var usr = db.Usuarios.SingleOrDefault(u => u.userName == usuario.userName);
            if (usr != null && VerifyHashedPassword(usr.pwd, usuario.pwd))
            {
                Session["UserID"] = usr.idUsuario.ToString();
                //Session["Username"] = usr.userName.ToString();
                Session["Name"] = usr.nombres.ToString().Split(' ')[0];
                Session["Lastname"] = usr.apellidos.ToString().Split(' ')[0];
                FormsAuthentication.SetAuthCookie(usr.idUsuario.ToString(), false);

                return Redirect(ReturnUrl);
            } 
            else
            {
                ModelState.AddModelError("", "Verifique su usuario y su contraseña");
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Login");
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        private static bool ByteArraysEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
    }
} 