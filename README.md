# prometheus-net-extensions
This library contains an extension for OWIN applications over the [prometheus-net](https://github.com/andrasm/prometheus-net) library, which will let you define an endpoint that exposes metrics that can be scraped by a Prometheus server.

# Usage
Firstly configure your OWIN application startup to use Prometheus:
```cs
/// This is the OWIN startup class
public class Startup
{

    public void Configuration(IAppBuilder app)
    {
        // This will make available a /metrics endpoint in your OWIN application
        app.UsePrometheus();        
    }
}
```
Once configured, you can make a HTTP GET request over the /metrics endpoint. You will see something similar to the following:
```
# HELP process_windows_virtual_bytes Process virtual memory size
# TYPE process_windows_virtual_bytes GAUGE
process_windows_virtual_bytes 379420672
# HELP process_windows_num_threads Total number of threads
# TYPE process_windows_num_threads GAUGE
process_windows_num_threads 22
# HELP process_windows_working_set Process working set
# TYPE process_windows_working_set GAUGE
process_windows_working_set 73125888
# HELP process_windows_processid Process ID
# TYPE process_windows_processid GAUGE
process_windows_processid 84236
# HELP dotnet_totalmemory Total known allocated memory
# TYPE dotnet_totalmemory GAUGE
dotnet_totalmemory 6019864
# HELP process_start_time_seconds Start time of the process since unix epoch in seconds
# TYPE process_start_time_seconds GAUGE
process_start_time_seconds 1478603191.40726
# HELP process_windows_open_handles Number of open handles
# TYPE process_windows_open_handles GAUGE
process_windows_open_handles 637
# HELP process_windows_private_bytes Process private memory size
# TYPE process_windows_private_bytes GAUGE
process_windows_private_bytes 49774592
# HELP dotnet_collection_count_total GC collection count
# TYPE dotnet_collection_count_total COUNTER
dotnet_collection_count_total{generation="1"} 2
dotnet_collection_count_total{generation="0"} 5
dotnet_collection_count_total{generation="2"} 1
# HELP dotnet_collection_errors_total Total number of errors that occured during collections
# TYPE dotnet_collection_errors_total COUNTER
# HELP process_cpu_seconds_total Total user and system CPU time spent in seconds
# TYPE process_cpu_seconds_total COUNTER
process_cpu_seconds_total 3.3125

```
Now you can add your application custom metrics as explained in the [prometheus-net](https://github.com/andrasm/prometheus-net) documentation
