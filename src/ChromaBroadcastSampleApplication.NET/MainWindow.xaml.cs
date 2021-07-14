// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Media;
using ChromaBroadcast;

namespace ChromaBroadcastSampleApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Sample app guid placeholder, guids are not distributed and you must get your own
        /// </summary>
        static readonly Guid ChromaBroadcastSampleApp = Guid.Parse("00000000-0000-0000-0000-000000000000");

        /// <summary>
        /// Creates the main window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            RzResult lResult = RzChromaBroadcastAPI.Init(ChromaBroadcastSampleApp);

            if (lResult == RzResult.Success)
            {
                BroadcastEnabled.IsChecked = true;
                BroadcastStatus.Text = "Initialization Success";

                RzChromaBroadcastAPI.RegisterEventNotification(OnChromaBroadcastEvent);
            }
            else
            {
                BroadcastStatus.Text = "Initialization Failed";
            }
        }

        /// <summary>
        /// The chroma broadcast event callback
        /// </summary>
        /// <param name="type">The broadcast type</param>
        /// <param name="status">The broadcast status</param>
        /// <param name="effect">The broadcast effect</param>
        /// <returns></returns>
        RzResult OnChromaBroadcastEvent(RzChromaBroadcastType type, RzChromaBroadcastStatus? status, RzChromaBroadcastEffect? effect)
        {
            if (type == RzChromaBroadcastType.BroadcastEffect)
            {
                Dispatcher.Invoke(() =>
                {
                    if (BroadcastEnabled.IsChecked == true)
                    {
                        CL1.Fill = new SolidColorBrush(Color.FromRgb(effect.Value.ChromaLink1.R, effect.Value.ChromaLink1.G, effect.Value.ChromaLink1.B));
                        CL2.Fill = new SolidColorBrush(Color.FromRgb(effect.Value.ChromaLink2.R, effect.Value.ChromaLink2.G, effect.Value.ChromaLink2.B));
                        CL3.Fill = new SolidColorBrush(Color.FromRgb(effect.Value.ChromaLink3.R, effect.Value.ChromaLink3.G, effect.Value.ChromaLink3.B));
                        CL4.Fill = new SolidColorBrush(Color.FromRgb(effect.Value.ChromaLink4.R, effect.Value.ChromaLink4.G, effect.Value.ChromaLink4.B));
                        CL5.Fill = new SolidColorBrush(Color.FromRgb(effect.Value.ChromaLink5.R, effect.Value.ChromaLink5.G, effect.Value.ChromaLink5.B));

                        BroadcastStatus.Text = "Chroma Broadcast is Live";
                    }
                });
            }
            else if (type == RzChromaBroadcastType.BroadcastStatus)
            {
                if (status == RzChromaBroadcastStatus.Live)
                {
                    Dispatcher.Invoke(() =>
                    {
                        BroadcastStatus.Text = "Chroma Broadcast is Live";
                    });
                }
                else if (status == RzChromaBroadcastStatus.NotLive)
                {
                    Dispatcher.Invoke(() =>
                    {
                        BroadcastStatus.Text = "Chroma Broadcast is Not Live";
                    });
                }
            }

            return RzResult.Success;
        }

        /// <summary>
        /// Occurs when the window is closing
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">The arguments</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RzChromaBroadcastAPI.UnRegisterEventNotification();
            RzChromaBroadcastAPI.UnInit();
        }
    }
}
