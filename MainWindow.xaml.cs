using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Thread thread { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            thread = new Thread(Copy);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.InitialDirectory = FromFolder.Text;
            dialog.Title = "Select a Directory";
            dialog.Filter = "Directory|*.this.directory";
            dialog.FileName = "select";
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                path = path.Replace("\\select.this.directory", "");
                path = path.Replace(".this.directory", "");
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                FromFolder.Text = path;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.InitialDirectory = IntoFolder.Text;
            dialog.Title = "Select a Directory";
            dialog.Filter = "Directory|*.this.directory";
            dialog.FileName = "select";
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                path = path.Replace("\\select.this.directory", "");
                path = path.Replace(".this.directory", "");
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                IntoFolder.Text = path;
            }
        }

        public void Copy()
        {
            DirectoryInfo source = new DirectoryInfo(FromFolder.Text);
            DirectoryInfo destin = new DirectoryInfo(IntoFolder.Text);

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Progress.Dispatcher.Invoke(() => Progress.Value += 10, DispatcherPriority.Background);
            }

            foreach (var item in source.GetFiles())
            {
                item.CopyTo(destin + @"\" + item.Name, true);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Copy();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            thread.Abort();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            thread.Suspend();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            thread.Resume();
        }
    }
}
