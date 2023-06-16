using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestDiplom.Models;

namespace TestDiplom.Controllers
{
    public class IndexModel : PageModel
    {
        private readonly ReaderBook _context;

        public IndexModel(ReaderBook context)
        {
            _context = context;
        }

        public IList<books> books { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.books != null)
            {
                books = await _context.books.ToListAsync();
            }
        }
    }
}
