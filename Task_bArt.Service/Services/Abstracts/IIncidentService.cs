using Task_bArt.Data.Entity;
using Task_bArt.Service.Dtos.Request;
using Task_bArt.Service.Dtos.Respons;

namespace Task_bArt.Service.Services.Abstracts
{
    public interface IIncidentService
    {
        public Task<List<ResponseIncidentDto>> GetAllIncidentsAsync();
        public Task<ResponseIncidentDto> GetIncidentAsync(string incedentName);
        public Task<bool> UpdateIncidentAsync(string incedentName, UpdateIncidentDto incidentDto);
        public Task<string> CreateIncidentAsync(int accountId, CreateIncidentDto createIncident);
    }
}
