using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelAgency.Models;
using TravelAgency.ViewModel;

namespace TravelAgency.Controllers
{
    public class ShoppingController : Controller
    {

        private TravelAgencyDbModel objTravelAgencyDBEntities1;
        private List<ShoppingCartModel> listOfShoppingCartModels;

        public ShoppingController()
        {
            objTravelAgencyDBEntities1 = new TravelAgencyDbModel();
            listOfShoppingCartModels = new List<ShoppingCartModel>();
        }


        public ActionResult Index()
        {

            IEnumerable<ShoppingViewModel> listOfShoppingViewModels = (from objItem in objTravelAgencyDBEntities1.Items
                                                                       join
                                                                          objCate in objTravelAgencyDBEntities1.Categories
                                                                          on objItem.Categoryid equals objCate.Categoryid
                                                                       select new ShoppingViewModel()
                                                                       {
                                                                           ImagePath = objItem.ImagePath,
                                                                           ItemName = objItem.ItemName,
                                                                           Description = objItem.Description,
                                                                           ItemPrice = objItem.ItemPrice,
                                                                           ItemId = objItem.ItemId,
                                                                           Category = objCate.CategoryName,
                                                                           ItemCode = objItem.ItemCode

                                                                       }


                                                         ).ToList();
            return View(listOfShoppingViewModels);
        }
        [HttpPost]
        public JsonResult Index(string ItemId)
        {
            ShoppingCartModel objShoppingCartModel = new ShoppingCartModel();
            Item objItem = objTravelAgencyDBEntities1.Items.Single(model => model.ItemId.ToString() == ItemId);
            if (Session["CartCounter"] != null)
            {
                listOfShoppingCartModels = Session["CartItem"] as List<ShoppingCartModel>;
            }
            if (listOfShoppingCartModels.Any(model => model.ItemId == ItemId))
            {
                objShoppingCartModel = listOfShoppingCartModels.Single(model => model.ItemId == ItemId);
                objShoppingCartModel.Quantity = objShoppingCartModel.Quantity + 1;
                objShoppingCartModel.Total = objShoppingCartModel.Quantity * objShoppingCartModel.UnitPrice;
            }
            else
            {
                objShoppingCartModel.ItemId = ItemId;
                objShoppingCartModel.ImagePath = objItem.ImagePath;
                objShoppingCartModel.ItemName = objItem.ItemName;
                objShoppingCartModel.Quantity = 1;
                objShoppingCartModel.Total = objItem.ItemPrice;
                objShoppingCartModel.UnitPrice = objItem.ItemPrice;
                listOfShoppingCartModels.Add(objShoppingCartModel);
            }

            Session["CartCounter"] = listOfShoppingCartModels.Count;
            Session["CartItem"] = listOfShoppingCartModels;
            return Json(new { Success = true, Counter = listOfShoppingCartModels.Count }, JsonRequestBehavior.AllowGet);
        }




        public ActionResult ShoppingCart()
        {
            listOfShoppingCartModels = Session["CartItem"] as List<ShoppingCartModel>;
            return View(listOfShoppingCartModels);
        }

        [HttpPost]
        public JsonResult AddOrder()
        {
            int OrderId = 0;
            listOfShoppingCartModels = Session["CartItem"] as List<ShoppingCartModel>;
            Order orderObj = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = String.Format("{0:ddmmyyyyHHmm}", DateTime.Now)

            };
            objTravelAgencyDBEntities1.Orders.Add(orderObj);
            objTravelAgencyDBEntities1.SaveChanges();
            OrderId = orderObj.OrderId;
            

            foreach(var item in listOfShoppingCartModels)
            {
                OrderDetail objOrderDetail = new OrderDetail();
                objOrderDetail.Total = item.Total;
                objOrderDetail.ItemId = item.ItemId;
                objOrderDetail.OrderId = OrderId;
                objOrderDetail.Quantity = item.Quantity;
                objOrderDetail.UnitPrice = item.UnitPrice;
                objTravelAgencyDBEntities1.OrderDetails.Add(objOrderDetail);
                objTravelAgencyDBEntities1.SaveChanges();
            }


            return Json("Data Successfully added", JsonRequestBehavior.AllowGet);
        }
    }
}