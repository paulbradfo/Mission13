using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private BowlersDbContext _context { get; set; }

        public HomeController(BowlersDbContext temp)
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            var bowl = _context.Bowlers.ToList();

            return View(bowl);
        }

        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            var blr = _context.Bowlers
                .Single(x => x.BowlerID == bowlerid);
            return View("Bowlinfo", blr);
        }

        [HttpPost]
        public IActionResult Edit(Bowler blr)
        {
            _context.Update(blr);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int bowlerid)
        {
            var blr = _context.Bowlers.Single(x => x.BowlerID == bowlerid);

            _context.Bowlers.Remove(blr);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Team(int teamid)
        {
            var blrs = _context.Bowlers.FromSqlRaw("SELECT * FROM Bowlers WHERE TeamID =" + teamid).ToList();
            return View("Index", blrs);
        }

        [HttpGet]
        public IActionResult AddBowl()
        {
            return View("Bowlinfo");
        }

        [HttpPost]
        public IActionResult AddBowl(Bowler blr)
        {
            _context.Update(blr);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

