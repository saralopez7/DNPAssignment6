using System;
using System.Windows;

namespace LAB12
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            fileDialog.ShowDialog();
            MediaElement1.Source = new Uri(fileDialog.FileName);
            MediaElement1.Play();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            MediaElement1.Play();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            MediaElement1.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            MediaElement1.Stop();
        }

        private void MediaElement1_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show("Media loading unsuccessful. " + e.ErrorException.Message);
        }
    }
}
