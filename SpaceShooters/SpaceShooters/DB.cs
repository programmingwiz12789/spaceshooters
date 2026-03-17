using System.Data.SqlClient;

namespace SpaceShooters
{
    class DB
    {
        public static SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=SpaceShooters;User ID=sa;Password=root;Encrypt=False");
    }
}
