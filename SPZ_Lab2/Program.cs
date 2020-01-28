using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPZ_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            RecordingStudio studio = new RecordingStudio("909", "LudvigaSvobody", 100, 250, 50, 10000, 10, 2);
            studio.AddWorker();
            studio.AddWorker();
            studio.AddWorker();
            studio.AddWorker();
            studio.AddRoom();
            studio.AddRoom();
            var result = studio.ProfitForMonth();

            RecordingStudio studio2 = (RecordingStudio)studio.Clone();
            studio2.AddWorker();
            studio2.AddRoom();

            for(int i = 0; i <= studio.AmountRooms; i++)
            {
                studio.RemoveRoom();
            }

        }
    }
}
