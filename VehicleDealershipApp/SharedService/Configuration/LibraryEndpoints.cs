using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedService.Configuration
{
    public class VehicleDealershipEndpoints
    {
        public string Base_url { get; set; }
        public string GetVehiclesEndpoint { get; set; }
        public string GetVehicleEndpoint { get; set; }
        public string UpdateVehicleEndpoint { get; set; }
        public string DeleteVehicleEndpoint { get; set; }
        public string AddVehicleEndpoint { get; set; }

    }
}
