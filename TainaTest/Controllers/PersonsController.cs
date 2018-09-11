using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TainaTest.BusinessLogic;
using TainaTest.Common;
using TainaTest.Model;

namespace TainaTest.Controllers
{
    public class PersonsController : Controller
    {
        private LoggingHandler _loggingHandler;

        public PersonsController()
        {
            _loggingHandler = new LoggingHandler();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_loggingHandler != null)
                {
                    _loggingHandler.Dispose();
                    _loggingHandler = null;
                }
            }

            base.Dispose(disposing);
        }

        // GET: Persons
        public ActionResult Index()
        {
            return View();
        }

        // GET: Persons/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Persons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Persons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                InsertPerson(int.Parse(collection["personId"]),
                                collection["Firstname"],
                                collection["Surname"],
                                collection["Gender"],
                                collection["EmailAddress"],
                                collection["PhoneNumber"]);

                return RedirectToAction("ListAll");
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        // GET: Persons/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var Person = SelectPersonById(id);
                return View(Person);
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        // POST: Persons/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                UpdatePerson(int.Parse(collection["personId"]),
                                collection["Firstname"],
                                collection["Surname"],
                                collection["Gender"],
                                collection["EmailAddress"],
                                collection["PhoneNumber"]);

                return RedirectToAction("ListAll");
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        // GET: Persons/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                DeletePerson(id);

                return RedirectToAction("ListAll");
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        // POST: Persons/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("ListAll");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ListAll()
        {
            try
            {
                var Persons = from e in ListAllPersons()
                                orderby e.PersonId
                                select e;
                return View(Persons);
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        #region Private Methods

        private List<PersonsEntity> ListAllPersons()
        {
            try
            {
                using (var Persons = new PersonsBusiness())
                {
                    return Persons.SelectAllPersons();
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }

        private PersonsEntity SelectPersonById(int id)
        {
            try
            {
                using (var Persons = new PersonsBusiness())
                {
                    return Persons.SelectPersonById(id);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }

        private void InsertPerson(int personsid, string firstname, string surname, string gender, string emailaddress, string phonenumber)
        {
            try
            {
                using (var Persons = new PersonsBusiness())
                {
                    var entity = new PersonsEntity();
                    entity.PersonId = personsid;
                    entity.Firstname = firstname;
                    entity.Surname = surname;
                    entity.Gender = gender;
                    entity.EmailAddress = emailaddress;
                    entity.PhoneNumber = phonenumber;
                    var opSuccessful = Persons.InsertPerson(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }

        private void UpdatePerson(int personsid, string firstname, string surname, string gender, string emailaddress, string phonenumber)
        {
            try
            {
                using (var Persons = new PersonsBusiness())
                {
                    var entity = new PersonsEntity();
                    entity.PersonId = personsid;
                    entity.Firstname = firstname;
                    entity.Surname = surname;
                    entity.Gender = gender;
                    entity.EmailAddress = emailaddress;
                    entity.PhoneNumber = phonenumber;
                    var opSuccessful = Persons.InsertPerson(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }

        private void DeletePerson(int id)
        {
            try
            {
                using (var Persons = new PersonsBusiness())
                {
                    var opSuccessful = Persons.DeletePersonById(id);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }


        #endregion

    }
}
