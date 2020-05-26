using Cycloid.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cycloid.Services
{
    public class ProgramsRestService : IProgramsService
    {
        private const string PROGRAMS_URL = "programs_rest/programs";
        private readonly HttpClient _client;
        
        public ProgramsRestService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://teste.cycloid.pt/");
        }

        public async Task<List<Program>> GetAllProgramsAsync()
        {
            List<Program> programs = null;
            HttpResponseMessage response = await _client.GetAsync(PROGRAMS_URL);
            if (response.IsSuccessStatusCode)
            {
                programs = await response.Content.ReadAsAsync<List<Program>>();
            }
            return programs;
        }
    }
}
