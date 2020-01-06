using ContactManagement.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ContactManagement.Controllers
{
    public class ContactController : Controller
    {
        string Baseurl = "http://localhost:4829/api/";

        [HttpGet]
        [ActionName("Index")]
        public async Task<ActionResult> Index()
        {
            List<ContactViewModel> contacts = new List<ContactViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("Contact/GetAllContacts");

                if (Res.IsSuccessStatusCode)
                {
                    var contactResponse = Res.Content.ReadAsStringAsync().Result;

                    contacts = JsonConvert.DeserializeObject<List<ContactViewModel>>(contactResponse);
                }
                return View(contacts);
            }
        }

        #region Create new contacts Section
        [HttpGet]
        [ActionName("Create")]
        public ActionResult GetCreate()
        {
            return View(new ContactViewModel());
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<ActionResult> PostCreate(ContactViewModel contact)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(contact);
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.PostAsync("Contact/CreateContact", new StringContent(
                        new JavaScriptSerializer().Serialize(contact), Encoding.UTF8, "application/json"));

                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(contact);
                }
            }
            catch (Exception)
            {
                return View(contact);
            }
        }
        #endregion

        #region Delete contacts Section
        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.GetAsync($"Contact/GetContactById/{id}");

                    if (Res.IsSuccessStatusCode)
                    {
                        var contactResponse = Res.Content.ReadAsStringAsync().Result;
                        ContactViewModel contact = new ContactViewModel();
                        contact = JsonConvert.DeserializeObject<ContactViewModel>(contactResponse);
                        return View(contact);
                    }
                    else
                    {
                        TempData["Message"] = "Contact Not Found for deletion.";
                        TempData["MessageSeverity"] = "Warning";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Delete");
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> PostDelete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.DeleteAsync($"Contact/DeleteContact/{id}");

                if (Res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Contact Deleted Successfully.";
                    TempData["MessageSeverity"] = "Success";
                    return RedirectToAction("Index", "Contact");
                }
                else
                {
                    TempData["Message"] = "Contact cannot be deleted. Please try again later.";
                    TempData["MessageSeverity"] = "Error";
                    return RedirectToAction("Delete", "Contact");
                }
            }
        }
        #endregion

        #region Update Contacts Section
        [HttpGet]
        [ActionName("Edit")]
        public async Task<ActionResult> GetEdit(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.GetAsync($"Contact/GetContactById/{id}");

                    if (Res.IsSuccessStatusCode)
                    {
                        var contactResponse = Res.Content.ReadAsStringAsync().Result;
                        ContactViewModel contact = new ContactViewModel();
                        contact = JsonConvert.DeserializeObject<ContactViewModel>(contactResponse);
                        return View(contact);
                    }
                    else
                    {
                        TempData["Message"] = "Some Internal Server error.";
                        TempData["MessageSeverity"] = "Error";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Contact");
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<ActionResult> PostEdit(int id, ContactViewModel contact)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = "Please fill all the details and Try again.";
                    TempData["MessageSeverity"] = "Warning";
                    return View(contact);
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.PutAsync($"Contact/UpdateContact/{id}", new StringContent(
                        new JavaScriptSerializer().Serialize(contact), Encoding.UTF8, "application/json"));

                    if (Res.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "Contact Updated Successfully.";
                        TempData["MessageSeverity"] = "Success";
                        return RedirectToAction("Index", "Contact");
                    }
                    else
                    {
                        TempData["Message"] = "Contact cannot be Updated. Please try again later.";
                        TempData["MessageSeverity"] = "Error";
                        return View(contact);
                    }
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Unable to Update the Contact. Some Internal Server Error Occured!";
                TempData["MessageSeverity"] = "Warning";
                return View(contact);
            }
        }
        #endregion

        [HttpGet]
        [ActionName("GetContactListWithPagination")]
        public async Task<JsonResult> GetContactListWithPagination(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            List<ContactViewModel> contacts = new List<ContactViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"Contact/GetContactListWithPagination?iDisplayLength={iDisplayLength}&iDisplayStart={iDisplayStart}&iSortCol_0={iSortCol_0}&sSortDir_0={sSortDir_0}&sSearch={sSearch}");

                if (Res.IsSuccessStatusCode)
                {
                    var contactResponse = Res.Content.ReadAsStringAsync().Result;

                    contacts = JsonConvert.DeserializeObject<List<ContactViewModel>>(contactResponse);
                }
                var result = Json(contacts, JsonRequestBehavior.AllowGet);
                return result;
            }
        }
    }
}