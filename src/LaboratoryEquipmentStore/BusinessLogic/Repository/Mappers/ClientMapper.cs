using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Store.BusinessLogic;

namespace Store.Repository.Mappers
{
    public class ClientMapper : IMapper<Store.BusinessLogic.Client>
    {
        private MySqlConnection conn;

        public ClientMapper(MySqlConnection newConn)
        {
            conn = newConn;
        }

        public int Create(Client item)
        {
            conn.Open();
            string sql = "Insert into `storedb`.`clients` (`name`, `address`) values ('" + item.Name + "', '" + item.Address + "');";
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
            return Convert.ToInt32(command.LastInsertedId);
        }

        public List<Client> FindAll()
        {
            conn.Open();
            string sql = "SELECT `id`,`name`, `address`  FROM `storedb`.`clients`";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            List<Client> answer = new List<Client>();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader[0].ToString());
                string name = reader[1].ToString();
                string address = reader[2].ToString();
                answer.Add(new Client(id, name, address));
            }
            reader.Close();
            conn.Close();
            return answer;
        }

        public Client FindById(int id)
        {
            conn.Open();
            string sql = "SELECT `id`,`name`, `address`  FROM `storedb`.`clients` WHERE id = " + id.ToString();
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            List<Client> answer = new List<Client>();
            while (reader.Read())
            {
                int newId = Convert.ToInt32(reader[0].ToString());
                string name = reader[1].ToString();
                string address = reader[2].ToString();
                answer.Add(new Client(newId, name, address));
            }
            reader.Close();
            conn.Close();
            return answer[0];
        }

        public void Update(Client item)
        {
            conn.Open();
            string sql = "Update `storedb`.`clients` set `name` = '" + item.Name + "', `address` ='" + item.Address + "' where id = " + item.Id;
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}
