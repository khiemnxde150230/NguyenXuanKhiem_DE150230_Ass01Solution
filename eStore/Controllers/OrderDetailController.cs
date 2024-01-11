using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Repository;
using BusinessObject;

namespace eStore.Controllers
{
    public class OrderDetailController : Controller
    {
        IOrderDetailRepository orderDetailRepository = null;
        public OrderDetailController() => orderDetailRepository = new OrderDetailRepository();
        // GET: OrderDetailController
        public ActionResult Index()
        {
            var orderDetailList = orderDetailRepository.GetOrderDetails();
            return View(orderDetailList);
        }

        // GET: OrderDetailController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var orderDetail = orderDetailRepository.GetOrderDetailByID(id.Value);
            if (orderDetail == null) { return NotFound(); }
            return View(orderDetail);
        }

        // GET: OrderDetailController/Create
        public ActionResult Create() => View();

        // POST: OrderDetailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderDetail orderDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    orderDetailRepository.InsertOrderDetail(orderDetail);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(orderDetail);
            }
        }

        // GET: OrderDetailController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id==null) { return NotFound(); }
            var orderDetail = orderDetailRepository.GetOrderDetailByID(id.Value);
            if (orderDetail == null) { return NotFound(); }
            return View(orderDetail);
        }

        // POST: OrderDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrderDetail orderDetail)
        {
            try
            {
                if (id != orderDetail.OrderId)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    orderDetailRepository.UpdateOrderDetail(orderDetail);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: OrderDetailController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) { return NotFound(); }
            var orderDetail = orderDetailRepository.GetOrderDetailByID(id.Value);
            if (orderDetail == null) { return NotFound(); }
            return View(orderDetail);
        }

        // POST: OrderDetailController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                orderDetailRepository.DeleteOrderDetail(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
