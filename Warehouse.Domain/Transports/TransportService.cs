using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Clients;
using Warehouse.Domain.Drivers;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Warehousemen;

namespace Warehouse.Domain.Transports
{
    public sealed class TransportService
    {
        public static Result<Transport> HandleTransport(
            int number,
            string type,
            Warehouseman warehouseWorker,
            Driver driver,
            Client client,
            DateTime? handledAt = null)
        {
            var transportCreateResult = Transport.Create(
                number,
                type,
                warehouseWorker,
                driver,
                client,
                handledAt);

            if (transportCreateResult.IsFailure)
            {
                return transportCreateResult.Error;
            }

            var transport = transportCreateResult.Value;

            var bookingResult = client.BookTransport(transport);

            if (bookingResult.IsFailure)
            {
                return bookingResult.Error;
            }

            var deliverTransportResult = driver.DeliverTransport(transport);

            if (deliverTransportResult.IsFailure)
            {
                return deliverTransportResult.Error;
            }

            var handleResult = warehouseWorker.HandleTransport(transport);

            if (handleResult.IsFailure)
            {
                return handleResult.Error;
            }

            return transport;
        }
    }
}
