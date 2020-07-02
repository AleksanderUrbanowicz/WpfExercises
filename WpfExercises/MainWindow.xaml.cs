using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Data.Common;
using System.Configuration;

namespace WpfExercises
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string provider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);
            using (DbConnection connection =
                factory.CreateConnection())
            {
                if(connection==null)
                {

                    return;
                }
                connection.ConnectionString = connectionString;
                connection.Open();
                DbCommand command = factory.CreateCommand();
                if(command==null)
                {
                    return;
                }
                command.Connection = connection;
                command.CommandText = "Select * From Tasks";
                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    while(dataReader.Read())
                    {
                        //Console.WriteLine($"{dataReader["Id"]}" + $"{dataReader["Name"]}");

                    }
                }
                //Console.ReadLine();
            }


                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            Title = position.ToString();
            if (position.X > 700)
            {
                TabControlMain.SelectedItem = 0;

            }
        }
       
    }

 
}
