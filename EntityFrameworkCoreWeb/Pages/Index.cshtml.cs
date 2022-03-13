using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EntityFrameworkCoreWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PeopleContext _peopleContext;

        public IndexModel(ILogger<IndexModel> logger, PeopleContext context)
        {
            _logger = logger;
            _peopleContext = context;
        }

        public void OnGet()
        {
            LoadSampleData();
            var people = _peopleContext.People
                .Include(a => a.Addresses)
                .Include(e => e.EmailAddresses)
                .ToList();
        }

        private void LoadSampleData()
        {
            if (_peopleContext.People.Count() == 0)
            {
                string file = System.IO.File.ReadAllText("generated.json");
                var people = JsonSerializer.Deserialize<List<Person>>(file);
                _peopleContext.AddRange(people);
                _peopleContext.SaveChanges();
                return;
            }
        }
    }
}
