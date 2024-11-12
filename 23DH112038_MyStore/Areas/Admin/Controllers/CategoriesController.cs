using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _23DH112038_MyStore.Models;

namespace _23DH112038_MyStore.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities(); // Khởi tạo thực thể động (truy vấn vào db)

        // GET: Admin/Categories
        // GET: lấy dữ liệu từ bảng Category trong DB để hiển thị lên       
        public ActionResult Index()
        {
            return View(db.Categories.ToList()); // Trả về danh sách 
        }

        // GET: Admin/Categories/Details/5
        // Details: Lấy chi tiết một bảng ghi có khoá là CategoryID = id
        public ActionResult Details(int? id)
        {
            if (id == null) // Không tìm thấy bản ghi
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // Mã lỗi 400: thiếu giá trị truyền vào
            }
            Category category = db.Categories.Find(id); // Tìm kiếm bảng ghi có id tương ứng
            if (category == null) // Không tìm thấy bản ghi
            {
                return HttpNotFound(); // Mã Lỗi 404
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        // Load form Create
        [HttpGet] // Là phương thức mặc định nên không cần khai báo từ khoá
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // POST: lưu dữ liệu từ form Create vào DB
        [HttpPost]
        [ValidateAntiForgeryToken] //So khoá Token (lưu 1 lần)
        // Bind: Binding dữ liệu
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName")] Category category)
        {
            if (ModelState.IsValid) // is valid
            {
                db.Categories.Add(category); // Thêm một bảng ghi vào DB
                db.SaveChanges(); // Lưu
                return RedirectToAction("Index"); // Chạy về form index sau khi lưu
            }

            return View(category); // nếu sai load lại view
        }

        // GET: Admin/Categories/Edit/5
        // GET: Lấy dữ liệu của một danh mục đã có sao cho CategoryID = id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified; //
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
