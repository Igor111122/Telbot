using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using System.Diagnostics;
using System.Text;

namespace Telbot
{
    class Program
    {
        private static string token { get; set; } = "";
        private static TelegramBotClient client;
        static bool new_needed_messege = false;
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]

        static extern bool ShowWindow(IntPtr hWnd, int showCmds);

        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, 0);
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
        }
       

        public static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            
            if (msg.Text != null) {
                if (new_needed_messege)
                {
                    if (msg.Text.Length < 10 && new_needed_messege) {
                        await client.SendTextMessageAsync(msg.Chat.Id, "Вы ввели неправильные данные");
                        new_needed_messege = false;
                        return;
                    }
                    if (msg.Text.Length > 10 && new_needed_messege)
                    {
                        await client.ForwardMessageAsync(878690980, msg.Chat.Id, msg.MessageId);
                        await client.ForwardMessageAsync(524633015, msg.Chat.Id, msg.MessageId);

                        await client.SendTextMessageAsync(msg.Chat.Id, "Для согласования заказа мы с вами свяжемся в ближайшее время");

                        new_needed_messege = false;
                        return;
                    }
                    
                }
                //await client.SendTextMessageAsync(msg.Chat.Id, msg.Text , replyMarkup: GetButtons());
                switch (msg.Text) {
                    case "Доставка":
                        await client.SendTextMessageAsync(msg.Chat.Id, "Введите свой номер, количество бутылей и адрес доставки, в формате."+"\n1.Номер"+ "\n2.Количество бутылей" + "\n3.Вид бутылей" + "\n4.Адрес");
                        new_needed_messege = true;
                        break;
                    case "Справка":
                        await client.SendTextMessageAsync(msg.Chat.Id, "Мы находимся по адресу: Ювілейний проспект, Запоріжжя" + "\nКонтактные телефоны: +380688867624 +380662791741");                  
                        break;
                    case "/start":
                        await client.SendTextMessageAsync(msg.Chat.Id, "Здавствуйте я ZhyvograyBot, я могу помочь вам составить заказ на доставку воды в городе Запорожье", replyMarkup: GetButtons());
                        break;
                    case "Кто-я?":
                        await client.SendTextMessageAsync(msg.Chat.Id, "Здавствуйте я ZhyvograyBot, я могу помочь вам составить заказ на доставку воды в городе Запорожье", replyMarkup: GetButtons());
                        break;
                    case "Цена":
                        await client.SendPhotoAsync(msg.Chat.Id, "https://drive.google.com/file/d/1OVEiTF1V7H9i1-k7BxIHSt0ew_27q4D-/view?usp=sharing", "Вода разливается в селе Беленьком из природного источника, прошла все проверки качества");
                        break;
                }
                
               
            }
        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {

                Keyboard = new List<List<KeyboardButton>>
                {

                    new List<KeyboardButton>{ new KeyboardButton { Text = "Кто-я?" }, new KeyboardButton { Text = "Доставка" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "Цена" }, new KeyboardButton { Text = "Справка" } }

                }



            };
        }
    }
}
