using BankTransaction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace BankTransaction.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionDbContext _context;
        public TransactionController(TransactionDbContext context)
        {
            _context = context;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        public IActionResult Register(Register model)
        {
            if (ModelState.IsValid) {
                var user = new Register
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Password2 = model.Password2
                };
                _context.Registers.Add(user);
                _context.SaveChanges();
                return RedirectToAction("LogIn","Transaction");
            }
            return View(model);
        }
        public IActionResult Dashboard()
        {
            var myRecord = _context.Transactions.FirstOrDefault(m => m.AccountNumber == "4925");
            if(myRecord == null)
            {
                ViewBag.CurrentAmount = 0;
                ViewBag.Loan = 0;
            }
            else
            {
                ViewBag.CurrentAmount = myRecord.Amount;
                ViewBag.Loan = myRecord.Loan;
            }
                return View();
        }
        public IActionResult Index()
        {
            var Transactions = _context.Transactions.ToList();
            return View(Transactions);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var transaction = _context.Transactions.Find(id);
            if(transaction == null)
            {
                return RedirectToAction("Index");
            }
            
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return View(transaction);
            }
            var currentTransactionData = _context.Transactions.Find(transaction.ID);
            if (currentTransactionData == null)
            {
                return NotFound();
            }
            currentTransactionData.AccountNumber = transaction.AccountNumber;
            currentTransactionData.ClientName = transaction.ClientName;
            currentTransactionData.BankName = transaction.BankName;
            currentTransactionData.Amount = transaction.Amount;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            var transaction = _context.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }
            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
            return RedirectToAction("Index");
        
        }
    }

    
}
