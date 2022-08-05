using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.Windows.AppLifecycle;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Activation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RestartTest
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public string restartString = "";
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Restarting...";
            try
            {
                AppActivationArguments activatedArgs = AppInstance.GetCurrent().GetActivatedEventArgs();
                ExtendedActivationKind kind = activatedArgs.Kind;
                if (kind == ExtendedActivationKind.Launch)
                {
                    if (activatedArgs.Data is ILaunchActivatedEventArgs launchArgs)
                    {
                        string argString = launchArgs.Arguments;
                        string[] argStrings = argString.Split();

                        if (argStrings.Length > 1)
                        {
                            argStrings = argStrings.Skip(1).ToArray();
                            restartString = String.Join(",", argStrings.Where(s => !string.IsNullOrEmpty(s)));
                        }
                    }
                }
                AppInstance instance = AppInstance.GetCurrent();
                AppRestartFailureReason restartError = AppInstance.Restart("");

                switch (restartError)
                {
                    case AppRestartFailureReason.RestartPending:
                        //statusText.Text = "Another restart is currently pending.";
                        break;
                    case AppRestartFailureReason.InvalidUser:
                        //statusText.Text = "Current user is not signed in or not a valid user.";
                        break;
                    case AppRestartFailureReason.Other:
                        //statusText.Text = "Failure restarting.";
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
