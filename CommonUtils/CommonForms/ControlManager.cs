//using DevExpress.XtraSplashScreen;
//using System.Windows;
//using static CommonForms.SplashScreenBase;

//namespace CommonForms
//{
//    public static class ControlManager
//    {
//        //private static SplashScreenBase _splashScreen;
//        private static bool _splashIsActive;
//        private static Image _logoImage;
//        private static Image _titleImage;
//        static ControlManager() 
//        {
//            _splashIsActive=false;
            
//        }
//        public static void SetLogoImage(Image logoImage) 
//        {
//            _logoImage=logoImage;
//        }
//        public static void SetTitleImage(Image titleImage) 
//        {
//            _titleImage=titleImage;
//        }
//        public static void ShowSplash(string state,  Form parentForn = null, Image titleImage = null, Image logoImage = null)
//        {
//            if(_splashIsActive) 
//                CloseSplash();
            
//            if(parentForn != null) 
//                SplashScreenManager.ShowForm(parentForn, typeof(SplashScreenBase));
//            else
//                SplashScreenManager.ShowForm(typeof(SplashScreenBase));


//            SetSplash(state, titleImage , logoImage);
//            //
            
//            _splashIsActive =true;
//        }
//        public static void SetSplash(string state, Image titleImage = null, Image logoImage = null)
//        {

//            SplashScreenManager.Default.SendCommand(SplashScreenCommand.SetStatus, state);
//            if (titleImage == null)
//                titleImage = _titleImage;
//            if (titleImage != null)
//                SplashScreenManager.Default.SendCommand(SplashScreenCommand.SetTitleImage, titleImage);
//            if (logoImage == null)
//                logoImage = _logoImage;
//            if (logoImage != null)
//                SplashScreenManager.Default.SendCommand(SplashScreenCommand.SetTitleImage, logoImage);
//        }
//        public static void CloseSplash()
//        {
//            SplashScreenManager.CloseForm();
//            _splashIsActive = false;
//        }
//    }
//}
