﻿using System;
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

        public int Create(Product item)
        {
            conn.Open();
            string sql = "Insert into `storedb`.`products` (`name`, `price`, `count`) values ('" + item.Name + "', '" + item.Price.ToString() + "', '" + item.Count.ToString() + "');";
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
            return Convert.ToInt32(command.LastInsertedId);
        }

        public List<Product> FindAll()
        {
            conn.Open();
            string sql = "SELECT `id`,`name`, `count`, `price`  FROM `storedb`.`products`";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            List<Product> answer = new List<Product>();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader[0].ToString());
                string name = reader[1].ToString();
                int count = Convert.ToInt32(reader[2].ToString());
                double price = Convert.ToDouble(reader[3].ToString());
                answer.Add(new Product(id, name, price, count));
            }
            reader.Close();
            conn.Close();
            return answer;
        }

        public Product FindById(int id)
        {
            conn.Open();
            string sql = "SELECT `id`,`name`, `count`, `price`  FROM `storedb`.`products` WHERE id = " + id.ToString();
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            List<Product> answer = new List<Product>();
            while (reader.Read())
            {
                int newId = Convert.ToInt32(reader[0].ToString());
                string name = reader[1].ToString();
                int count = Convert.ToInt32(reader[2].ToString());
                double price = Convert.ToDouble(reader[3].ToString());
                answer.Add(new Product(id, name, price, count));
            }
            reader.Close();
            conn.Close();
            return answer[0];
        }

        public void Update(Product item)
        {
            conn.Open();
            string sql = "Update `storedb`.`products` set `name` = '" + item.Name + "', `price` ='" + item.Price.ToString() + "', `count`='" + item.Count.ToString() + "' where id = " + item.Id;
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}
