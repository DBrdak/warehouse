using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;

namespace Warehouse.Domain.Freights
{
    public sealed class FreightService
    {
        public static Result<Freight> ReceiveFreight(
            Transport import,
            PalletSpace palletSpace,
            string name,
            string type,
            decimal quantity,
            string unit)
        {
            var freightCreateResult = Freight.Create(name, type, quantity, unit, palletSpace, import);

            if (freightCreateResult.IsFailure)
            {
                return freightCreateResult.Error;
            }

            var freight = freightCreateResult.Value;

            var transportAddFrightResult = import.AddFreight(freight);

            if (transportAddFrightResult.IsFailure)
            {
                return transportAddFrightResult.Error;
            }

            var placeFreightResult = palletSpace.PlaceFreight(freight);

            if (placeFreightResult.IsFailure)
            {
                return placeFreightResult.Error;
            }

            return freight;
        }

        public static Result ReleaseFreight(Freight freight, Transport export)
        {
            var releaseResult = freight.Release(export);

            if (releaseResult.IsFailure)
            {
                return releaseResult.Error;
            }

            var transportAddFreightResult = export.AddFreight(freight);

            if (transportAddFreightResult.IsFailure)
            {
                return transportAddFreightResult.Error;
            }

            var takeFreightResult = freight.PalletSpace.TakeFreight(freight);

            if (takeFreightResult.IsFailure)
            {
                return takeFreightResult.Error;
            }

            return Result.Success();
        }
    }
}
