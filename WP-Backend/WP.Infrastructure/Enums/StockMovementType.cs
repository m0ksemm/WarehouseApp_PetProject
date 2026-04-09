using System;
using System.Collections.Generic;
using System.Text;

namespace WP.Infrastructure.Enums
{
    public enum StockMovementType
    {
        Incoming = 1,
        Outgoing = 2,
        Transfer = 3,
        Adjustment = 4,
        Reservation = 5,
        ReleaseReservation = 6
    }
}
