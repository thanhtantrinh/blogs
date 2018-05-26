using Common;
using Model.Repository;
using Model.ViewModel;
using OnlineShop.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Filters.AuthLog(Roles = UserRoles.Admin)]
    public class OrderController : BaseController
    {
        protected OrderRepo _orderRepo = new OrderRepo();
        // GET: Admin/Order
        public ActionResult Index(int page = 1, int pageSize = 20, string sortby = "")
        {
            OrderFilter filter = (OrderFilter)Session[SessionName.OrderFilter];
            if (filter == null)
            {
                filter = new OrderFilter();
                Session[SessionName.OrderFilter] = filter;
            }
            var model = _orderRepo.GetOrderPaging(filter, page, pageSize, sortby);
            ViewBag.PageNumber = page;
            ViewBag.Title = "Danh sách đơn hàng";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(OrderFilter filter)
        {
            if (filter != null)
                Session[SessionName.OrderFilter] = filter;
            else
                Session[SessionName.OrderFilter] = new OrderFilter();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SetCancelledOrder(long orderId = 0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;

            if (orderId > 0)
            {
                var result=_orderRepo.UpdateStatusOrder(orderId, nameof(eOrderStatusUI.Cancelled));
                if (result)
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, "Hủy đơn hàng số " + orderId + " thành công");
                    actionStatus.ActionStatus = ResultSubmit.success;
                    //Session["ACTION_STATUS"] = actionStatus;
                }
                else
                {                  
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, "Hủy đơn hàng số " + orderId + " không thành công.");
                }
            }
            else
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, "Hủy đơn hàng không thành công. Vui lòng kiểm tra số đơn hàng");
            }
   
            Session[SessionName.ActionStatusLog] = actionStatus;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SetCompletedOrder(long orderId = 0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;

            if (orderId > 0)
            {
                var result = _orderRepo.UpdateStatusOrder(orderId, nameof(eOrderStatusUI.Completed));
                if (result)
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, "Cập nhật đã giao hàng số " + orderId + " thành công");
                    actionStatus.ActionStatus = ResultSubmit.success;
                    //Session["ACTION_STATUS"] = actionStatus;
                }
                else
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, "Cập nhật đã giao hàng số " + orderId + " không thành công.");
                }
            }
            else
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, "Cập nhật đã giao hàng không thành công. Vui lòng kiểm tra số đơn hàng");
            }

            Session[SessionName.ActionStatusLog] = actionStatus;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SetPendingOrder(long orderid = 0)
        {
            var actionStatus = new ActionResultHelper();
            actionStatus.ActionStatus = ResultSubmit.failed;

            if (orderid > 0)
            {
                var result = _orderRepo.UpdateStatusOrder(orderid, nameof(eOrderStatusUI.Pending));
                if (result)
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_SUCCESS, "Cập nhật trạng thái đơn hàng số "+ orderid + " thành công");
                    actionStatus.ActionStatus = ResultSubmit.success;
                    //Session["ACTION_STATUS"] = actionStatus;
                }
                else
                {
                    actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, "Cập nhật trạng thái đơn hàng số " + orderid + " không thành công");
                }
            }
            else
            {
                actionStatus.ErrorReason = String.Format(SiteResource.HTML_ALERT_ERROR, "Cập nhật đã giao hàng không thành công. Vui lòng kiểm tra số đơn hàng");
            }

            Session[SessionName.ActionStatusLog] = actionStatus;
            return RedirectToAction("Index");
        }
    }
}