using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipment_Manager.BackEnd
{
    class SystemInfo
    {
        public static string GetSystemInfo(string SoftwareName)
        {
            if (UseProcessorID == true)

                SoftwareName += RunQuery("Processor", "ProcessorId");

            if (UseBaseBoardProduct == true)

                SoftwareName += RunQuery("BaseBoard", "Product");

            if (UseBaseBoardManufacturer == true)

                SoftwareName += RunQuery("BaseBoard", "Manufacturer");
            // See more in source code

            SoftwareName = RemoveUseLess(SoftwareName);

            if (SoftwareName.Length < 25)

                return GetSystemInfo(SoftwareName);
            return SoftwareName.Substring(0, 25).ToUpper();
        }
    }
}
