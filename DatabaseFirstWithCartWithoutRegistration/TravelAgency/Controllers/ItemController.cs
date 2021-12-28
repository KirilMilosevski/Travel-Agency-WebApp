using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelAgency.Models;
using TravelAgency.ViewModel;


namespace TravelAgency.Controllers
{
    public class ItemController : Controller
    {

        private TravelAgencyDbModel objTravelAgencyDBEntities;
        public ItemController()
        {
            objTravelAgencyDBEntities = new TravelAgencyDbModel();
        }
        // GET: Item
        public ActionResult Index()
        {

            ItemViewModel objItemViewModel = new ItemViewModel();
            objItemViewModel.CategorySelectListItem = (from objCat in objTravelAgencyDBEntities.Categories
                                                       select new SelectListItem()
                                                       {
                                                           Text = objCat.CategoryName,
                                                           Value = objCat.Categoryid.ToString(),
                                                           Selected = true
                                                       });
            return View(objItemViewModel);
        }

        [HttpPost]
        public JsonResult Index(ItemViewModel objItemViewModel)
        {
            string NewImage = Guid.NewGuid() + Path.GetExtension(objItemViewModel.ImagePath.FileName);
            objItemViewModel.ImagePath.SaveAs(Server.MapPath("~/Images/"+NewImage));

            Item objItem = new Item();
            objItem.ImagePath = "~/Images/" + NewImage;
            objItem.Categoryid = objItemViewModel.CategoryId;
            objItem.Description = objItemViewModel.Description;
            objItem.ItemCode = objItemViewModel.ItemCode;
            objItem.ItemId = Guid.NewGuid();
            objItem.ItemName = objItemViewModel.ItemName;
            objItem.ItemPrice = objItemViewModel.ItemPrice;
            objTravelAgencyDBEntities.Items.Add(objItem);
            objTravelAgencyDBEntities.SaveChanges();

            return Json(new { Success = true, Message = "Item is added!"},JsonRequestBehavior.AllowGet);
        }


    }
}