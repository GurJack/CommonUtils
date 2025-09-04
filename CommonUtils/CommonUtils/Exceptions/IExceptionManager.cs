//using System;

//namespace CommonUtils.Exceptions
//{
//    / <summary>
//    / Interface for exceptions catching and logging.
//    / </summary>
//    public interface IExceptionManager
//    {
//        / <summary>
//        / Determinates is the exception will be catched by this exception manager.
//        / </summary>
//        / <param name = "ex" > Specified exception.</param>
//        / <returns>Returns true if exception will be catched; else returns false.</returns>
//        bool IsCatcher(Exception ex);

//        / <summary>
//        / Logging specified exception and all inner exceptions.
//        / </summary>
//        / <param name = "ex" > Specified exception.</param>
//        void Log(Exception ex);

//        / <summary>
//        / Shows information about exception.
//        / </summary>
//        / <param name = "ex" > Specified exception.</param>
//        void Show(Exception ex);

//        / <summary>
//        / Sends the exception information to the developers.
//        / </summary>
//        / <param name = "ex" > Specified exception.</param>
//        void Feedback(Exception ex);
//    }
//}