using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MathApp.Models;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MathApp.Controllers
{
    public class MathController : Controller
    {
        private readonly MathDbContext _context;

        public MathController(MathDbContext context)
        {
            _context = context;
        }

        public IActionResult Calculate()
        {
            List<SelectListItem> operations = new List<SelectListItem> {
                new SelectListItem { Value = "1", Text = "+" },
                new SelectListItem { Value = "2", Text = "-" },
                new SelectListItem { Value = "3", Text = "*" },
                new SelectListItem { Value = "4", Text = "/" },

                };

            ViewBag.Operations = operations;
    
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calculate(decimal? FirstNumber, decimal? SecondNumber,int Operation)
        {
            MathCalculation mathCalculation = new MathCalculation();
            mathCalculation.FirstNumber = FirstNumber;
            mathCalculation.SecondNumber = SecondNumber;
            mathCalculation.Operation = Operation;

            switch (Operation)
            {
                case 1:
                    mathCalculation.Result = FirstNumber + SecondNumber;
                    break;
                case 2:
                    mathCalculation.Result = FirstNumber - SecondNumber;
                    break;
                case 3:
                    mathCalculation.Result = FirstNumber * SecondNumber;
                    break;
                default:
                    if (SecondNumber != 0)
                        mathCalculation.Result = FirstNumber / SecondNumber;
                    break;
            }

            if (ModelState.IsValid)
            {
                _context.Add(mathCalculation);
                await _context.SaveChangesAsync();
                
            }
            ViewBag.Result = mathCalculation.Result;
            return View();

            
            // return RedirectToAction("Calculate");
            
        }

        public async Task<IActionResult> History()
        {
            return View(await _context.MathCalculations.ToListAsync());
        }

        public IActionResult Clear()
        {
            _context.MathCalculations.RemoveRange(_context.MathCalculations);
            _context.SaveChangesAsync();

            return RedirectToAction("History");
        }
    }
}
