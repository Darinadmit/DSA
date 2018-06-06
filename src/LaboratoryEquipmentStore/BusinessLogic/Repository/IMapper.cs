using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Store.Repository
{
    interface IMapper<T>
    {
        List<T> FindAll(string cond);
        void Update(T item);
        int Create(T item);
    }
}
