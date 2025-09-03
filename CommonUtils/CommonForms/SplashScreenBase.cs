using DevExpress.CodeParser;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonForms
{
    public partial class SplashScreenBase : SplashScreen
    {
        public SplashScreenBase()
        {
            InitializeComponent();
            this.labelCopyright.Text = "Copyright © 1998-" + DateTime.Now.Year.ToString();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
            var splashCommand = (SplashScreenCommand)cmd;
            switch (splashCommand)
            {

                case SplashScreenCommand.SetStatus:
                    labelStatus.Text = (string)arg;
                    break;
                case SplashScreenCommand.SetLogo:
                    peLogo.Image = (Image)arg;
                    break;
                case SplashScreenCommand.SetTitleImage:
                    peImage.Image = (Image)arg;
                    break;
                default:
                    throw new NotImplementedException("Неизвестная команда");
                    break;
            }
            Application.DoEvents();
        }

        #endregion

        public enum SplashScreenCommand
        {
            SetStatus = 1,
            SetLogo = 2,
            SetTitleImage = 3,
        }
    }
}