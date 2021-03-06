﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace LAB12
{
    public partial class Window1 : Window
    {
        System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
        DispatcherTimer _timer = new DispatcherTimer();

        public Window1()
        {
            InitializeComponent();
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += OnTimerTick;
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            fileDialog.ShowDialog();
            MediaElementPlayer.Source = new Uri(fileDialog.FileName);
            MediaElementPlayer.Play();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            // The Play method will begin the media if it is not currently active or 
            // resume media if it is paused. This has no effect if the media is
            // already running.
            MediaElementPlayer.Play();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            // The Pause method pauses the media if it is currently running.
            // The Play method can be used to resume.
            MediaElementPlayer.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            // The Stop method stops and resets the media to be played from
            // the beginning.
            MediaElementPlayer.Stop();
        }

        private void MediaElement1_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MediaElementNameLabel.Content = $"Unable to Play: {Path.GetFileName(fileDialog.FileName)} from: {Path.GetDirectoryName(fileDialog.FileName)}";

            MessageBox.Show($"Media loading unsuccessful. {e.ErrorException.Message}",
                caption: "Error Loading Media File",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error);

            ColorAnimation animation = new ColorAnimation
            {
                To = Colors.White,
                Duration = new Duration(TimeSpan.FromSeconds(3))
            };
            OpenButton.Background = new SolidColorBrush(Colors.Red);
            OpenButton.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);

        }

        private void MediaElement1_MediaOpened(object sender, RoutedEventArgs e)
        {
            _timer.Start();

            // Update progress bar
            var position = MediaElementPlayer.NaturalDuration.HasTimeSpan ? MediaElementPlayer.NaturalDuration.TimeSpan : TimeSpan.FromSeconds(0);
            MediaElementProgressBar.Minimum = 0;
            MediaElementProgressBar.Maximum = position.TotalSeconds;

            // Update media name on media file openened.
            MediaElementNameLabel.Content = $"Playing: {Path.GetFileName(fileDialog.FileName)} from: {Path.GetDirectoryName(fileDialog.FileName)}";

            // Display this animation on media open success. 
            ColorAnimation animation = new ColorAnimation
            {
                To = Colors.White,
                Duration = new Duration(TimeSpan.FromSeconds(3))
            };

            OpenButton.Background = new SolidColorBrush(Colors.Green);
            OpenButton.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);

        }

        void OnTimerTick(object sender, EventArgs e)
        {
            MediaElementProgressBar.Value = MediaElementPlayer.Position.TotalSeconds;
        }

        private void MediaElementProgressBar_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ProgressBarLabel.Content = TimeSpan.FromSeconds(MediaElementProgressBar.Value);
        }
    }
}
