using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.API
{
    public interface IAvailableQueueManager
    {
        public Task<bool> IsHolidayAsync(DateTime date);
    }
}
