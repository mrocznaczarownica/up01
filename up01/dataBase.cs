using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace up01
{
    public class dataBase
    {
        //Путь к файлу БД
        private static string DB_NAME = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "up01.db");
        //Строка соединения
        private static string CONNECT_STRING = string.Format("Data Source=LAPTOP-GK9EKMOU;Initial Catalog=up01;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", DB_NAME);

        //Метод добавления данных в БД(используется при разработке)
        public static void AddDataToBase(string code, List<byte> data)
        {
            //Строка команды вставки записи
            string insertCmd = string.Format("INSERT INTO CFGTable VALUES('{0}', @data)", code);
            using (SqlConnection cnn = new SqlConnection(CONNECT_STRING))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand(insertCmd, cnn))
                {
                    SqlParameter param = new SqlParameter("@data", DbType.Binary);
                    param.Value = data.ToArray();
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //Метод получения данных об имеющихся в БД записях
        public static List<string> GetAllProductCodes()
        {
            //Строковый выходной массив
            List<string> products = new List<string>();
            //Строка команды выборки
            string selectCmd = "SELECT productCode FROM CFGTable";
            //Выборка данных из БД
            using (SqlConnection cnn = new SqlConnection(CONNECT_STRING))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand(selectCmd, cnn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    //Проверка на наличие данных
                    if (!dr.HasRows)
                    {
                        throw new ArgumentException("Data not present!");
                    }
                    while (dr.Read())
                    {
                        products.Add((string)dr["productCode"]);
                    }
                }
            }
            return products;
        }
        //Метод чтения одного файла данных из БД(в формате строкового массива)
        public static string[] GetAllConfigLines(string code)
        {
            //Строка команды выборки
            string selectCmd = string.Format("SELECT data FROM CFGTable WHERE productCode='{0}'", code);
            //Маассив байт для данных
            List<byte> tempList = new List<byte>();
            //Строковый выходной массив
            List<string> configLines = new List<string>();
            //Выборка данных из БД
            using (SqlConnection cnn = new SqlConnection(CONNECT_STRING))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand(selectCmd, cnn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    //Проверка на наличие данных
                    if (!dr.HasRows)
                    {
                        throw new ArgumentException("Data not present!");
                    }
                    while (dr.Read())
                    {
                        tempList.AddRange((byte[])dr["data"]);
                        break;
                    }
                }
            }
            //Построчное чтение из потока в памяти
            using (MemoryStream ms = new MemoryStream(tempList.ToArray()))
            {
                using (StreamReader sr = new StreamReader(ms, Encoding.Default))
                {
                    while (sr.Peek() != -1)
                    {
                        configLines.Add(sr.ReadLine());
                    }
                }
            }
            //Возврат массива строк
            return configLines.ToArray();
        }
    }
}
