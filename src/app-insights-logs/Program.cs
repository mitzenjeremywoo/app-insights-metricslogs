using Azure;
using Azure.Identity;
using Azure.Monitor.Query;
using Azure.Monitor.Query.Models;

string resourceId =
    "/subscriptions/<tenant-id>/resourceGroups/<resource>>/providers/Microsoft.Storage/storageAccounts/your-storage-account";

var client = new MetricsQueryClient(new DefaultAzureCredential());

Response<MetricsQueryResult> results = await client.QueryResourceAsync(
    resourceId,
    new[] { "Availability", "Availability" }
);

foreach (MetricResult metric in results.Value.Metrics)
{
    Console.WriteLine(metric.Name);
    foreach (MetricTimeSeriesElement element in metric.TimeSeries)
    {
        Console.WriteLine("Dimensions: " + string.Join(",", element.Metadata));

        foreach (MetricValue value in element.Values)
        {
            Console.WriteLine(value);
        }
    }
}