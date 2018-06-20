using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Store.BusinessLogic;

namespace Store.Repository.Mappers
{
    public class OrderMapper : IMapper<Store.BusinessLogic.Order>
    {
        private MySqlConnection conn;

        public OrderMapper(MySqlConnection newConn)
        {
            conn = newConn;
        }

        public int Create(Order item)
        {
            conn.Open();
            string sql = "Insert into `storedb`.`orders` (`status`, `price`, `id_man`, `id_cl`, `id_prod`, `count`) values ('" + item.OrderStatus + "', '" + item.Price + "', '" + item.OrderManager + "', '" + item.OrderClient + "', '" + item.OrderProduct + "', '" + item.Count + "');";
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
            return Convert.ToInt32(command.LastInsertedId);
        }

        public List<Order> FindAll()
        {
            conn.Open();
            string sql = "SELECT `id`, `status`, `price`, `id_man`, `id_cl`, `id_prod`, `count` FROM `storedb`.`orders`";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            List<Order> answer = new List<Order>();
            while (reader.Read())
            {
                int newId = Convert.ToInt32(reader[0].ToString());
                GeneralInfo.OrderStatus st = (GeneralInfo.OrderStatus)Convert.ToInt32(reader[1].ToString());
                double price = Convert.ToDouble(reader[2].ToString());
                Manager mgId = GeneralInfo.DataProvider.GetManager(Convert.ToInt32(reader[3].ToString()));
                Client clId = GeneralInfo.DataProvider.GetClient(Convert.ToInt32(reader[4].ToString()));
                Product prId = GeneralInfo.DataProvider.GetProduct(Convert.ToInt32(reader[5].ToString()));
                int count = Convert.ToInt32(reader[6].ToString());
                answer.Add(new Order(newId, st, price, mgId, clId, prId, count));
            }
            reader.Close();
            conn.Close();
            return answer;
        }

        public Order FindById(int id)
        {
            conn.Open();
            string sql = "SELECT `id`, `status`, `price`, `id_man`, `id_cl`, `id_prod`, `count` FROM `storedb`.`orders` WHERE id = " + id.ToString();
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            List<Order> answer = new List<Order>();
            while (reader.Read())
            {
                int newId = Convert.ToInt32(reader[0].ToString());
                GeneralInfo.OrderStatus st = (GeneralInfo.OrderStatus)Convert.ToInt32(reader[1].ToString());
                double price = Convert.ToDouble(reader[2].ToString());
                Manager mgId = GeneralInfo.DataProvider.GetManager(Convert.ToInt32(reader[3].ToString()));
                Client clId = GeneralInfo.DataProvider.GetClient(Convert.ToInt32(reader[4].ToString()));
                Product prId = GeneralInfo.DataProvider.GetProduct(Convert.ToInt32(reader[5].ToString()));
                int count = Convert.ToInt32(reader[6].ToString());
                answer.Add(new Order(newId, st, price, mgId, clId, prId, count));
            }
            reader.Close();
            conn.Close();
            return answer[0];
        }

        public void Update(Order item)
        {
            conn.Open();
            string sql = "Update `storedb`.`orders` set `status` = '" + (int)item.OrderStatus + "' where id = " + item.Id;
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}
