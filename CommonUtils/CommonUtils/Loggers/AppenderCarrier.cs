//using System;
//using log4net.Appender;
//using log4net.Core;
//using log4net.Filter;
//using log4net.Layout;
//using log4net.Repository.Hierarchy;

//namespace CommonUtils.Loggers
//{
//    public abstract class AppenderCarrier
//    {
//        //private readonly Hierarchy _repository;
//        public AppenderSkeleton Appender { get; protected set; }
//        public string LayoutPattern
//        {
//            get => Layout.ConversionPattern;
//            set
//            {
//                Layout.ConversionPattern = value;
//                Layout.ActivateOptions();
//                Active = false;
//            }
//        }
//        //private string _layoutPattern;
//        protected PatternLayout Layout { get; }
//        protected AppenderCarrier(AppenderSkeleton appender)
//        {
//            Appender = appender ?? throw new ArgumentNullException(nameof(appender),"Не задан appender");
//            Threshold = Level.All;
//            Layout=new PatternLayout( );
//            Layout.AddConverter("MachineName", typeof(MachinePatternConverter));
//            Layout.AddConverter("Operation", typeof(OperationPatternConverter));
//            Layout.AddConverter("User", typeof(UserNamePatternConverter));
//            Layout.AddConverter("Metod", typeof(MetodPatternConverter));
//            Layout.AddConverter("ApplicationName", typeof(ApplicationNamePatternConverter));
//            Appender.Layout = Layout;
//            Appender.Name = Appender.GetType().ToString() + RandomGenerator.Next();
//            LayoutPattern = Logger.MainLayoutPattern;
//            Active = false;
            
//        }

//        public bool Active { get; protected set; }
//        public Level Threshold
//        {
//            get => Appender.Threshold;
//            set
//            {
//                Appender.Threshold = value;
//                Active = false;
//            }
            
//        }

//        public IAppender InitAppender()
//        {
//            Appender.ActivateOptions();
//            Active = true;
//            return Appender;
//        }

//    }
//}