using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab05
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string cadena = "Server=LAB1507-24\\SQLEXPRESS; Database=Neptuno;User ID=userTecsup;Password=123456;MultipleActiveResultSets=True";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            listarClientes();

            

        }

        // Eliminar
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var cliente = button.DataContext as Cliente;

            if (cliente != null)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(cadena))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand("USP_DeleteClientesById", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@IdCliente",SqlDbType.NVarChar, 50) { 
                            Value = cliente.idcliente
                        });

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            listarClientes();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }


        private void listarClientes()
        {
            List<Cliente> Listaclientes = new List<Cliente>();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("USP_ListarClientes", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        idcliente = reader["idCliente"] != DBNull.Value ? reader["idCliente"].ToString() : string.Empty,
                        nombrecompañia = reader["Nombrecompañia"] != DBNull.Value ? reader["Nombrecompañia"].ToString() : string.Empty,
                        nombrecontacto = reader["Nombrecontacto"] != DBNull.Value ? reader["Nombrecontacto"].ToString() : string.Empty,
                        cargocontacto = reader["CargoContacto"] != DBNull.Value ? reader["CargoContacto"].ToString() : string.Empty,
                        direccion = reader["Direccion"] != DBNull.Value ? reader["Direccion"].ToString() : string.Empty,
                        ciudad = reader["Ciudad"] != DBNull.Value ? reader["Ciudad"].ToString() : string.Empty,
                        codpostal = reader["CodPostal"] != DBNull.Value ? reader["CodPostal"].ToString() : string.Empty,
                        pais = reader["Pais"] != DBNull.Value ? reader["Pais"].ToString() : string.Empty,
                        telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : string.Empty,
                        fax = reader["Fax"] != DBNull.Value ? reader["Fax"].ToString() : string.Empty,
                        enabled = reader["Enabled"] != DBNull.Value ? Convert.ToBoolean(reader["Enabled"]) : null,
                    };
                    Listaclientes.Add(cliente);
                }
            }

            Gridclientes.ItemsSource = Listaclientes;
        }
    }
}