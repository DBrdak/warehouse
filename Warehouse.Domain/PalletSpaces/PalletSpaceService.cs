using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.PalletSpaces
{
    public sealed class PalletSpaceService
    {
        public static Result<PalletSpace> CreatePalletSpace(
            int number,
            int shelf,
            int rack,
            Sector sector)
        {
            var palletSpaceCreateResult = PalletSpace.Create(number, shelf, rack, sector);

            if (palletSpaceCreateResult.IsFailure)
            {
                return palletSpaceCreateResult.Error;
            }

            var palletSpace = palletSpaceCreateResult.Value;

            sector.AssignPalletSpace(palletSpace);

            return palletSpace;
        }
    }
}
