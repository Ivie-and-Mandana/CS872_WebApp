using CS872_WebApp.DataAccessLayer;
using CS872_WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CS872_WebApp.Controllers
{
    public class StandardUserController : Controller
    {

        private MySqlDBContext mySqlDBContext;
        //private 

        // GET: StandardUserController
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "UserAccount");
            }
            return RedirectToAction("Index", "UserAccount");
        }

        // GET: StandardUserController/Details/5
        public async Task<ActionResult> Details(BillViewModel model)
        {
            mySqlDBContext = new MySqlDBContext();
            var conn = mySqlDBContext.getConnection();

            using (conn)
            {

                BillViewModel data = await mySqlDBContext.getBill(model);
                if (data != null)
                { return View(data); }
            }

            return null;
            //return View();
        }

        // GET: StandardUserController/Create
        public ActionResult Create()
        {
            return View();
        }


        // To specify that this will be 
        // invoked when post method is called
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int billID, BillViewModel model)
        {
            mySqlDBContext = new MySqlDBContext();
            var conn = mySqlDBContext.getConnection();

            using (conn)
            {
                
                var bill = await mySqlDBContext.getBill(billID); //UpdateBill.Bill.FirstOrDefault(x => x.billID == billID);

                if (bill != null)
                {
                    bill.billID = model.billID;
                    bill.emailAddress = model.emailAddress;
                    bill.billDateTIme = model.billDateTIme;
                    bill.amount = model.amount;
                    bill.houseArea = model.houseArea;
                    bill.numberOfRooms = model.numberOfRooms;
                    bill.numberOfChildren = model.numberOfChildren;
                    bill.numberOfPeople = model.numberOfPeople;
                    bill.isAirCondtion = model.isAirCondtion;
                    bill.isTelevision = model.isTelevision;
                    bill.isFlat = model.isFlat;
                    bill.isTelevision = model.isTelevision;
                    bill.isUrban = model.isUrban;
                    bill.billStatus = model.billStatus;

                    var response = (ResponseModel<string>) await mySqlDBContext.SaveChanges(bill);

                    if (response.resultCode == 200)
                    {
                        return RedirectToAction("Read");
                    }
                   
                }

            }

            return View();
        }


        [HttpGet] // Set the attribute to Read
        public async Task<ActionResult> Read(string emailAddress)
        {
            mySqlDBContext = new MySqlDBContext();
            var conn = mySqlDBContext.getConnection();

            using (conn)
            {

                List<BillViewModel> data = await mySqlDBContext.getBills(emailAddress);
                if (data != null)
                { return View(data); }
            }

            return null;
        }



        // GET: StandardUserController/Edit/5
        public ActionResult Edit(string emailAddress)
        {
            return View();
        }

        // POST: StandardUserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int billID, BillViewModel model)
        {
            mySqlDBContext = new MySqlDBContext();
            var conn = mySqlDBContext.getConnection();

            using (conn)
            {

                var bill = await mySqlDBContext.getBill(billID); //UpdateBill.Bill.FirstOrDefault(x => x.billID == billID);

                if (bill != null)
                {
                    bill.billID = model.billID;
                    bill.emailAddress = model.emailAddress;
                    bill.billDateTIme = model.billDateTIme;
                    bill.amount = model.amount;
                    bill.houseArea = model.houseArea;
                    bill.numberOfRooms = model.numberOfRooms;
                    bill.numberOfChildren = model.numberOfChildren;
                    bill.numberOfPeople = model.numberOfPeople;
                    bill.isAirCondtion = model.isAirCondtion;
                    bill.isTelevision = model.isTelevision;
                    bill.isFlat = model.isFlat;
                    bill.isTelevision = model.isTelevision;
                    bill.isUrban = model.isUrban;
                    bill.billStatus = model.billStatus;

                    var response = (ResponseModel<string>)await mySqlDBContext.SaveChanges(bill);

                    if (response.resultCode == 200)
                    {
                        return RedirectToAction("Read");
                    }

                }

            }

            return View();
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: StandardUserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StandardUserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int billID, BillViewModel model)
        {
            mySqlDBContext = new MySqlDBContext();
            var conn = mySqlDBContext.getConnection();

            using (conn)
            {

                var bill = await mySqlDBContext.getBill(billID); //UpdateBill.Bill.FirstOrDefault(x => x.billID == billID);

                if (bill != null)
                {
                    bill.billID = model.billID;
                    bill.emailAddress = model.emailAddress;
                    bill.billDateTIme = model.billDateTIme;
                    bill.amount = model.amount;
                    bill.houseArea = model.houseArea;
                    bill.numberOfRooms = model.numberOfRooms;
                    bill.numberOfChildren = model.numberOfChildren;
                    bill.numberOfPeople = model.numberOfPeople;
                    bill.isAirCondtion = model.isAirCondtion;
                    bill.isTelevision = model.isTelevision;
                    bill.isFlat = model.isFlat;
                    bill.isTelevision = model.isTelevision;
                    bill.isUrban = model.isUrban;
                    bill.billStatus = model.billStatus;

                    var response = (int) await mySqlDBContext.DeleteBill(bill);

                    if (response == 200)
                    {
                        return RedirectToAction("Read");
                    }

                }

            }
            return View(model);
            //try
            //{
            //    return RedirectToAction("Read");
            //}
        }
    }
}
