using System.Net; //добавить
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class Server
{
    HttpListener server;
    bool flag = true;
    static Store.Facade.IFacade model;

    static void Main(string[] args)
    {
        //ресурс, который будет запрашивать пользователь
        model = new Store.Facade.Facade();
        string uri = @"http://localhost:8080/test/";
        new Server().StartServer(uri);
    }
    private void StartServer(string prefix)
    {
        server = new HttpListener();
        // текущая ос не поддерживается
        if (!HttpListener.IsSupported) return;
        //добавление префикса (say/)
        //обязательно в конце должна быть косая черта
        if (string.IsNullOrEmpty(prefix))
            throw new ArgumentException("prefix");
        server.Prefixes.Add(prefix);
        //запускаем север
        server.Start();
        //сервер запущен? Тогда слушаем входящие соединения
        while (server.IsListening)
        {
            //ожидаем входящие запросы
            HttpListenerContext context = server.GetContext();
            //получаем входящий запрос
            HttpListenerRequest request = context.Request;
            //обрабатываем POST запрос
            //запрос получен методом POST (пришли данные формы)
            if (request.HttpMethod == "POST")
            {
                //показать, что пришло от клиента
                ShowRequestData(request);
                //завершаем работу сервера
                if (!flag) return;
            }
            //формируем ответ сервера:
            //динамически создаём страницу
            string responseString =  model.GetAllProductsAsJSON();
            //отправка данных клиенту
            HttpListenerResponse response = context.Response;
            response.ContentType = "text/html; charset=UTF-8";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            using (Stream output = response.OutputStream)
            {
                output.Write(buffer, 0, buffer.Length);
            }
        }
    }

    private void ShowRequestData(HttpListenerRequest request)
    {
        //есть данные от клиента?
        if (!request.HasEntityBody) return;
        
    }
}