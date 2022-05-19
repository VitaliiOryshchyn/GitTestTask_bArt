using Microsoft.EntityFrameworkCore;
using Task_bArt.Data.Context;
using Task_bArt.Data.Entity;
using Task_bArt.Service.Dtos.Request;
using Task_bArt.Service.Dtos.Respons;
using Task_bArt.Service.Services.Abstracts;

namespace Task_bArt.Service.Services.Implements
{
    public class IncidentService : IIncidentService
    {
        private readonly AppDbContext _db;

        public IncidentService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<ResponseIncidentDto>> GetAllIncidentsAsync() 
        {
            var incidents = await _db.Incidents.ToListAsync();
            if (incidents is null) 
            {
                return null;
            }
            List<ResponseIncidentDto> responseIncidents = new List<ResponseIncidentDto>();
            foreach (var incident in incidents) 
            {
                responseIncidents.Add(new ResponseIncidentDto() 
                {
                    IncidentName = incident.IncidentName,
                    Description = incident.Description
                });
            }
            return responseIncidents;
        }

        public async Task<ResponseIncidentDto> GetIncidentAsync(string incedentName) 
        {
            var incident = await _db.Incidents.FirstOrDefaultAsync(x => x.IncidentName == incedentName);
            if (incident is null) 
            {
                return null;
            }
            var responseIncident = new ResponseIncidentDto() 
            {
                IncidentName = incident.IncidentName,
                Description = incident.Description
            };
            return responseIncident;

        }
        public async Task<bool> UpdateIncidentAsync(string incedentName, UpdateIncidentDto incidentDto)
        {
            var incedent = await _db.Incidents.FirstOrDefaultAsync(x => x.IncidentName == incedentName);
            if (incedent is null)
            {
                return false;
            }

            incedent.Description = incidentDto.Description;
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<string> CreateIncidentAsync(int accountId, CreateIncidentDto createIncident)
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);
            if (account is null)
            {
                return "Error: Account id dosen't exist!";
            }

            await _db.Incidents.AddAsync(new Incident() { Description = createIncident.Description });
            await _db.SaveChangesAsync();

            var newIncident = await _db.Incidents.FirstOrDefaultAsync(x => x.Description == createIncident.Description);
            if (newIncident is null)
            {
                return "Error: Incident dosen't exist!";
            }

            account.Incident = newIncident;
            await _db.SaveChangesAsync();

            return "Incidenta added";
        }
    }
}
