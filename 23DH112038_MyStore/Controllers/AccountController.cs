using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Remoting.Messaging;
using System.Runtime.CompilerServices;
using _23DH112038_MyStore.Models.ViewModel;
using System.Web.Security;
using System.Diagnostics;
using _23DH112038_MyStore.Models;
namespace _23DH112038_MyStore.Controllers
{
    public class AccountController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                //kiến tra xem tên đăng nhập đã tồn tại chưa
                var existingUser = db.Users.SingleOrDefault(u => u.Username == model.Username);
                //ktra xem co duy nhat hay khong

                if (existingUser != null) // co roi
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập này đã tồn tại!"); //bao loi
                    return View(model);
                }
                // nếu chưa tồn tại thì tạo bản ghi thông tin tài khoản trong bảng User
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    UserRole = "1"
                };
                db.Users.Add(user);
                // và tạo bản ghi thông tin khách hàng trong bảng Customer



                var customer = new Customer
                {
                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,
                    CustomerPhone = model.CustomerPhone,
                    CustomerAddress = model.CustomerAddress,
                    Username = model.Username,
                };
                db.Customers.Add(customer);
                // lưu thông tin tài khoản và thông tin khách hàng vào CSDL
                //db.SaveChanges();
                try
                {

                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập ngay.";
                    return RedirectToAction("Login", "Account");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }

                    TempData["ErrorMessage"] = "Đã xảy ra lỗi trong quá trình đăng ký. Vui lòng thử lại.";
                }
            }


            return View(model);

        }

        //GET: LOGIN
        public ActionResult Login()
        {
            return View();
        }
        //POST: LOGIN
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == model.Username
                && u.Password == model.Password
                && u.UserRole == "1");
                if (user != null)
                {
                    //Lưu trạng thái đăng nhập vào sesion
                    Session["Username"] = user.Username;
                    Session["UserRole"] = user.UserRole;
                    //Lưu thông tin xác thực người dùng vào cookie
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            return View(model);
        }



        public ActionResult Logout()
        {
            Session.Clear();// xoá session data
            return RedirectToAction("Index", "Home");
        }
    }
}
