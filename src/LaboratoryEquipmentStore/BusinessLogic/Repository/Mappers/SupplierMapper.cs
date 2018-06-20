using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Store.BusinessLogic;

namespace Store.Repository.Mappers
{
    public class SupplierMapper : IMapper<Store.BusinessLogic.Supplier>
    {
        private MySqlConnection conn;

        public SupplierMapper(MySqlConnection newConn)
        {
            conn = newConn;
        }

        public int Create(Supplier item)
        {
            conn.Open();
            string sql = "Insert into `storedb`.`suppliers` (`name`, `address`) values ('" + item.Name + "', '" + item.Address + "');";
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            int newId = Convert.ToInt32(command.LastInsertedId);
            foreach (var product in item.ListOfProducts)
            {
                sql = "Insert into `storedb`.`sup_products` (`id_sup`, `id_prod`) values ('" + newId + "', '" + product.ToString() + "');";
                command = new MySqlCommand(sql, conn);
                command.ExecuteNonQuery();
            }
            conn.Close();
            return newId;
        }

        public List<Supplier> FindAll()
        {
            conn.Open();
            string sql = "SELECT `id`,`name`, `address`,  FROM `storedb`.`suppliers`";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            List<Supplier> answer = new List<Supplier>();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader[0].ToString());
                string name = reader[1].ToString();
                string address = reader[2].ToString();
                List<int> products = new List<int>();
                sql = "SELECT `id_proc`  FROM `storedb`.`sup_products` WHERE id = " + id.ToString();
                command = new MySqlCommand(sql, conn);
                MySqlDataReader readerForProducts = command.ExecuteReader();
                while (readerForProducts.Read())
                {
                    int newId = Convert.ToInt32(readerForProducts[0].ToString());
                    products.Add(newId);
                }
                answer.Add(new Supplier(id, name, address, products));
            }
            reader.Close();
            conn.Close();
            return answer;
        }

        public Supplier FindById(int supId)
        {
            conn.Open();
            string sql = "SELECT `id`,`name`, `address`,  FROM `storedb`.`suppliers` WHERE id = " + supId.ToString();
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            List<Supplier> answer = new List<Supplier>();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader[0].ToString());
                string name = reader[1].ToString();
                string address = reader[2].ToString();
                List<int> products = new List<int>();
                sql = "SELECT `id_proc`  FROM `storedb`.`sup_products` WHERE id = " + id.ToString();
                command = new MySqlCommand(sql, conn);
                MySqlDataReader readerForProducts = command.ExecuteReader();
                while (readerForProducts.Read())
                {
                    int newId = Convert.ToInt32(readerForProducts[0].ToString());
                    products.Add(newId);
                }
                answer.Add(new Supplier(id, name, address, products));
            }
            reader.Close();
            conn.Close();
            return answer[0];
        }

        public void Update(Supplier item)
        {
            conn.Open();
            string sql = "Update `storedb`.`suppliers` set `name` = '" + item.Name + "', `address` ='" + item.Address + "' where id = " + item.Id;
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}
