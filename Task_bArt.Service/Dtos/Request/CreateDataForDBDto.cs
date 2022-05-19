using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_bArt.Service.Dtos.Request
{
    public class CreateDataForDBDto
    {
        public string AccountName { get; set; }
        public string ContactFirastName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public string IncidentDescription { get; set; }
    }
}
