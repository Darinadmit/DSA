using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Store.BusinessLogic;

namespace Store.Repository.Mappers
{
    public class ProductMapper : IMapper<Store.BusinessLogic.Product>
    {
        private MySqlConnection conn;

        public ProductMapper(MySqlConnection newConn)
        {
            conn = newConn;
        }

        int IMapper<Product>.Create(Product item)
        {
            conn.Open();
            string sql = "Insert into products (name, price, count) values (" + item.Name + ", " + item.Price.ToString() + ", " + item.Count.ToString() + ")";
            MySqlCommand command = new MySqlCommand(sql, conn);
            sql = "SELECT IDENT_CURRENT('products')";
            command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int id = Convert.ToInt32(reader[0].ToString());
            reader.Close();
            conn.Close();
            return id;
        }

        List<Product> IMapper<Product>.FindAll(string cond)
        {
            conn.Open();
            string sql = "SELECT id, name, count, price  FROM products WHERE id = " + cond;
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            List<Product> answer = new List<Product>();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader[0].ToString());
                string name = reader[1].ToString();
                int count = Convert.ToInt32(reader[2].ToString());
                double price = Convert.ToDouble(reader[3].ToString());
                Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString());
                answer.Add(new Product(id, name, price, count));
            }
            reader.Close();
            conn.Close();
            return answer;
        }

        void IMapper<Product>.Update(Product item)
        {
            conn.Open();
            string sql = "Update products set name = " + item.Name + ", price =" + item.Price.ToString() + ", count=" + item.Count.ToString() + " where id = " + item.Id;
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}
