using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using TestApplication.Models;

namespace TestApplication.Processors
{   

    public class ProcessFile : IProcessFile
    {
        public string ParseFile(string fileContent)
        {
            var uniqueNavHistory = SortFileContent(fileContent);

            var result = ExportToCsv(uniqueNavHistory);

            return result;
        }

        private List<NavhistoryData> SortFileContent(string fileContent)
        {
            var navHistoryModelList = JsonSerializer.Deserialize<NavHistoryModel>(fileContent);

            if (navHistoryModelList == null || navHistoryModelList.NavHistory == null
                || navHistoryModelList.NavHistory.NavHistory == null || navHistoryModelList.NavHistory.NavHistory.Length == 0)
            {
                return null;
            }

            var uniqueNavHistory = navHistoryModelList.NavHistory.NavHistory
               .GroupBy(item => item.NavDate)
               .Select(g => g.OrderByDescending(item => item.SourcePriority)
                             .ThenByDescending(item => item.VolumeRank)
                             .ThenByDescending(item => item.ReceivedTime)
                             .First())
               .ToList();

            return uniqueNavHistory;
        }

        private string ExportToCsv(List<NavhistoryData> uniqueNavHistory)
        {
            var csvBuilder = new StringBuilder();

            csvBuilder.AppendLine("Nav,NavDate,ReceivedTime");

            foreach (var item in uniqueNavHistory)
            {
                csvBuilder.AppendLine($"{item.Nav},{item.NavDate},{item.ReceivedTime}");
            }

            return csvBuilder.ToString();
        }
    }
}
