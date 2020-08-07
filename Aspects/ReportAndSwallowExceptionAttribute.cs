using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Aspects.Dependencies;
using PostSharp.Serialization;
using Serilog;
using System;
using System.Text;

namespace ExceptionHandling
{
    [AspectTypeDependency(AspectDependencyAction.Order, AspectDependencyPosition.After,
      typeof(AddContextOnExceptionAttribute))]
    [PSerializable]
    public sealed class ReportAndSwallowExceptionAttribute : OnExceptionAspect //,IInstanceScopedAspect
    {
        //[ImportMember("TheLogger", IsRequired = true)]
        //public Property<ILogger> LoggerProperty;
        //public ILogger TheLogger { get; set; }

        //public ReportAndSwallowExceptionAttribute(ILogger logger)
        //{
        //    TheLogger = logger;
        //}
        public override void OnException(MethodExecutionArgs args)
        {
            //LoggerProperty.Get().Error("Oops!!!! Error Happened ....");
            Log.Error("Oh a very bad error happened!");
            //TheLogger.Error("Error happened ...");
            // Write the default exception information.
            Console.WriteLine("Exception information");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine(args.Exception.ToString());
            Console.WriteLine("--------------------------------------------------------------");
            var additionalContext = (StringBuilder)args.Exception.Data["Context"];

            // Write the additional information that was gathered by AddContextOnExceptionAttribute.
            if (additionalContext != null)
            {
                Console.WriteLine("Additional context information (call stack with parameter values)");
                Console.WriteLine("--------------------------------------------------------------");
                Console.Write(additionalContext.ToString());
                Console.WriteLine("--------------------------------------------------------------");
            }

            // Ignore the exception.
            Console.WriteLine("*** Ignoring the exception ***");
            args.FlowBehavior = FlowBehavior.Continue;
        }

        //object IInstanceScopedAspect.CreateInstance(AdviceArgs adviceArgs)
        //{
        //    return this.MemberwiseClone();
        //}

        //void IInstanceScopedAspect.RuntimeInitializeInstance()
        //{
        //}
    }
}