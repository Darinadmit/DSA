using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Store.BusinessLogic;

namespace Store.Repository.Mappers
{
    public class ManagerMapper : IMapper<Store.BusinessLogic.Manager>
    {
        private MySqlConnection conn;

        public ManagerMapper(MySqlConnection newConn)
        {
            conn = newConn;
        }

        public int Create(Manager item)
        {
            conn.Open();
            string sql = "Insert into `storedb`.`managers` (`name`) values ('" + item.Name + "');";
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
            return Convert.ToInt32(command.LastInsertedId);
        }

        public List<Manager> FindAll()
        {
            conn.Open();
            string sql = "SELECT `id`,`name`  FROM `storedb`.`managers`";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            List<Manager> answer = new List<Manager>();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader[0].ToString());
                string name = reader[1].ToString();
                answer.Add(new Manager(id, name));
            }
            reader.Close();
            conn.Close();
            return answer;
        }

        public Manager FindById(int id)
        {
            conn.Open();
            string sql = "SELECT `id`,`name`  FROM `storedb`.`managers` WHERE id = " + id.ToString();
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            List<Manager> answer = new List<Manager>();
            while (reader.Read())
            {
                int newId = Convert.ToInt32(reader[0].ToString());
                string name = reader[1].ToString();
                answer.Add(new Manager(newId, name));
            }
            reader.Close();
            conn.Close();
            return answer[0];
        }

        public void Update(Manager item)
        {
            conn.Open();
            string sql = "Update `storedb`.`Managers` set `name` = '" + item.Name + "' where id = " + item.Id;
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}
