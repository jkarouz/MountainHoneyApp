using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MountainHoneyApp.Models;

namespace MountainHoneyApp.Controllers
{
    public class SunrisesController : Controller
    {
        private readonly TestingContext _context;

        public TestingContext Context => _context;

        public SunrisesController(TestingContext context)
        {
            _context = context;
        }

        // GET: Sunrises
        public async Task<IActionResult> Index(string searchString)
        {

            var Sunrise = from Sunrises in this.Context.Sunrises
                          select Sunrises;


            if (!String.IsNullOrEmpty(searchString))
            {
                Sunrise = Sunrise.Where(s => s.Name.Contains(searchString));
            }

            return View(await Sunrise.ToListAsync());
        }

        // GET: Sunrises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sunrises == null)
            {
                return NotFound();
            }

            var sunrise = await _context.Sunrises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sunrise == null)
            {
                return NotFound();
            }

            return View(sunrise);
        }

        // GET: Sunrises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sunrises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sunrise sunrise)
        {
            if (ModelState.IsValid)
            {

                Sunrise sunrise1 = new Sunrise
                {
                    Name = sunrise.Name,
                    Surname = sunrise.Surname,
                    FullName = sunrise.Name + " " + sunrise.Surname,
                    IdNumber = sunrise.IdNumber,
                    Comments = sunrise.Comments,
                    ContactNumber = sunrise.ContactNumber,
                    Place = sunrise.Place,
                    RentAmount = sunrise.RentAmount,
                    Payment = sunrise.Payment,
                    Method = sunrise.Method,
                    OldAmount = sunrise.OldAmount,
                    FullAmount= sunrise.RentAmount-sunrise.OldAmount,
                    DateOnlyTime = sunrise.DateOnlyTime,
                };



                _context.Add(sunrise1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sunrise);
        }

        // GET: Sunrises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sunrises == null)
            {
                return NotFound();
            }

            var sunrise = await _context.Sunrises.FindAsync(id);
            if (sunrise == null)
            {
                return NotFound();
            }
            return View(sunrise);
        }

        // POST: Sunrises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Comments,FullName,IdNumber,ContactNumber,Payment,Place,Method,RentAmount,DateOnlyTime,DateOnly")] Sunrise sunrise)
        {
            if (id != sunrise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sunrise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SunriseExists(sunrise.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sunrise);
        }

        // GET: Sunrises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sunrises == null)
            {
                return NotFound();
            }

            var sunrise = await _context.Sunrises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sunrise == null)
            {
                return NotFound();
            }

            return View(sunrise);
        }

        // POST: Sunrises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sunrises == null)
            {
                return Problem("Entity set 'TestingContext.Sunrises'  is null.");
            }
            var sunrise = await _context.Sunrises.FindAsync(id);
            if (sunrise != null)
            {
                _context.Sunrises.Remove(sunrise);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SunriseExists(int id)
        {
          return (_context.Sunrises?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public IActionResult Export(int id,string Name)
        {
            DataTable dt = new DataTable("Accounts");
            dt.Columns.AddRange(new DataColumn[8] {
                                        new DataColumn("FullName"),
                                        new DataColumn("Comments"),
                                        new DataColumn("RentAmount"),
                                        new DataColumn("Place"),
                                        new DataColumn("Payment"),
                                        new DataColumn("Method"),
                                        new DataColumn("FullAmount"),
                                        new DataColumn("DateOnly")});

            var Sunrise = from Sunrises in this.Context.Sunrises
                          select Sunrises;



            foreach (var sunrise in Sunrise)
            {
              dt.Rows.Add(sunrise.FullName, sunrise.Comments, sunrise.RentAmount, sunrise.Place, sunrise.Payment, sunrise.Method,sunrise.FullAmount,sunrise.DateOnly);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sunrise Accounts.xlsx");
                }
            }

        }
        public ActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("jckarouztemp@gmail.com", "Justin");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "justinK@16";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }
    }
}
