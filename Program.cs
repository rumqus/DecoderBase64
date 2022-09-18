using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace DecoderBase64
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "files";
            string[] files = Directory.GetFiles(path);
            
            // Получаем файлы из папки
            void GetFilesToDecode() 
            {
                string message = "";
                string rawBase64 = "";
                for (int i = 0; i < files.Length; i++)
                {                    
                    XmlDocument xml = new XmlDocument(); // xml документ для чтения
                    xml.Load($@"{path}\{files[i]}");
                    XmlNodeList nodeList = xml.GetElementsByTagName("message");
                    message += nodeList[i].InnerText;
                    Console.WriteLine(nodeList[i].InnerText);                  
                }
                rawBase64 = message;
                Decode(rawBase64);
            }
            // Раскодируем base64
            void Decode(string file) 
            {
                byte[] data = Convert.FromBase64String(file);
                string messageDecoded = Encoding.UTF8.GetString(data);
                WriteToFile(messageDecoded); // вызываем метод записи
            }
            // записываем файл
            void WriteToFile(string data) 
            {
                string nameFile = "DecodedText.xml";
                using (StreamWriter writer = new StreamWriter(data, true, Encoding.Default))
                {
                    writer.WriteLine(data);
                    Console.WriteLine("Файл сохранен");
                }
            }
            
            Console.ReadLine();
        }
    }
}
