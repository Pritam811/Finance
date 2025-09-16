using BankTransaction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BankTransaction.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionDbContext _context;
        public TransactionController(TransactionDbContext context)
        {
            _context = context;
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
