using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Serilog;
using ExceptionHandling;
using Serilog;
using System;

namespace ConsumerTest
{
    class Program
    {


        //public ILogger TheLogger { get; set; }
        //[ReportAndSwallowException(Log.Logger)]

        [ReportAndSwallowException]
        [Log]
        public static void TryExceptionMethod()
        {
            throw new NotImplementedException();
        }

        [Log]
        public static void TryLogMethod()
        {
            Log.Information("Everything is alright!");
        }
        static void Main(string[] args)
        {
            //TheLogger = new LoggerConfiguration()
            Log.Logger = new LoggerConfiguration()
                    .WriteTo.File(@"Logs\LogByAspect.log", rollingInterval: RollingInterval.Day)
                    .CreateLogger();

            //var theLogger = new LoggerConfiguration()
            //        .WriteTo.File(@"Logs\LogByAspect.log", rollingInterval: RollingInterval.Day)
            //        .CreateLogger();

            LoggingServices.DefaultBackend = new SerilogLoggingBackend(Log.Logger); // or TheLogger

            Console.WriteLine("Hello World!");
            TryLogMethod();
            TryExceptionMethod();

        }
    }
}
