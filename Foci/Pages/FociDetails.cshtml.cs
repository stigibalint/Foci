﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Foci.Models;

namespace Foci.Pages
{
    public class FociDetailsModel : PageModel
    {
        private readonly Foci.Models.FociDbContext _context;

        public FociDetailsModel(Foci.Models.FociDbContext context)
        {
            _context = context;
        }

        public Meccs Meccs { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meccs = await _context.Meccsek.FirstOrDefaultAsync(m => m.Id == id);
            if (meccs == null)
            {
                return NotFound();
            }
            else
            {
                Meccs = meccs;
            }
            return Page();
        }
    }
}
