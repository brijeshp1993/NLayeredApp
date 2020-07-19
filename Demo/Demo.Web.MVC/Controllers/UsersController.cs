using System;
using System.Net;
using System.Web.Mvc;
using Demo.Application.Contract.DTOs;
using Demo.Application.UserModule.Services;

namespace Demo.Web.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            _userService = userService;
        }

        // GET: Users
        public ActionResult Index()
        {
            var usersList = _userService.GetAll();
            return View(usersList);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userDto = _userService.GetUserById((int)id);
            if (userDto == null)
            {
                return HttpNotFound();
            }
            return View(userDto);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EmailId,Password,FirstName,LastName")] UserDTO userDto)
        {
            if (ModelState.IsValid)
            {
                var success = _userService.InsertUser(userDto);
                if (success)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(userDto);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userDto = _userService.GetUserById((int)id);
            return View(userDto);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmailId,Password,FirstName,LastName")] UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userDto);
            }
            var success = _userService.UpdateUser(userDto);
            if (success)
            {
                return RedirectToAction("Index");
            }
            return View(userDto);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userDto = _userService.GetUserById((int)id);
            if (userDto == null)
            {
                return HttpNotFound();
            }
            return View(userDto);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var success = _userService.DeleteUser(id);
            if (success)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Delete", new { id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
