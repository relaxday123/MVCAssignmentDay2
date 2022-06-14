using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using b1.Models;
using b1.Services;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace b1.Controllers
{
    public class RookieController : Controller
    {
        private readonly ILogger<RookieController> _logger;
        private readonly IPersonService _personService;

        public RookieController(ILogger<RookieController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        public IActionResult Index()
        {
            var data = _personService.GetAll();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Person model)
        {
            var member = _personService.Create(model);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int index)
        {
            ViewData["Index"] = index;
            var person = _personService.GetOne(index);
            return View(person);
        }

        [HttpPost]
        public IActionResult Update(Person model, int index)
        {
            var member = _personService.Update(model, index);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int editindex)
        {
            ViewData["EditIndex"] = editindex;
            _personService.Delete(editindex);
            return RedirectToAction("Index");
        }
    }
}