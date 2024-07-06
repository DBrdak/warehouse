using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.UI.ViewModels.Lodge.Requests;

public sealed record AddDriverRequest(string FirstName, string LastName, string VehiclePlate);