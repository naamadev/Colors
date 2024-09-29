using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace FinalProlectWeb
{
    public class Palette 
    {
        public string UserName { get; set; }
        public List<string> Colors { get; set; }

        //פונקציה A קבלת רשימת צבעים לפי שם משתמש
        public static List<string> MyPalette(string username)//קבלת רשימת צבעים לפי שם משתמש
        {
            List<string> lst = new List<string>();
            using (StreamReader Sr = File.OpenText("Main_Foldr" + '/' + username + '/' + "pallete.txt"))
            {
                string text = Sr.ReadToEnd();
                lst = JsonConvert.DeserializeObject<List<string>>(text);
            }
            return lst;
        }

        //פונקציה B //יוצרת רשימת צבעים
        public static bool CreatePalette(string username,List<string> lst)//מה הבדיקה למדוד הצלחה?
        {
            string texte = JsonConvert.SerializeObject(lst);
            using (StreamWriter SW = File.CreateText("Main_Foldr" + '/' + username + '/' + "pallete.txt"))
            {
                SW.WriteLine(texte);
            }
            return true;           
        }


        //פונקציה C//הוספת צבע בודד לרשימה
        public static bool AddColor(string username, string color)
        {
            List<string> lst = new List<string>();
            using (StreamReader Sr = File.OpenText("Main_Foldr" + '/' + username + '/' + "pallete.txt"))
            {
                string text = Sr.ReadToEnd();
                lst = JsonConvert.DeserializeObject<List<string>>(text);
            }
            if (lst == null)
            {
                lst = new List<string>();
                lst.Add(color);
            }
            lst.Add(color);
            string texte = JsonConvert.SerializeObject(lst);
            using (StreamWriter SW = File.CreateText("Main_Foldr" + '/' + username + '/' + "pallete.txt"))
            {
                SW.WriteLine(texte);
            }
            return true;
        }


        //פונקציה D--מחיקת צבע בודד
        public static int RemoveColor(string username, string color)
        {
            try
            {
                List<string> lst = new List<string>();
                using (StreamReader Sr = File.OpenText("Main_Foldr" + '/' + username + '/' + "pallete.txt"))
                {
                    string text = Sr.ReadToEnd();
                    lst = JsonConvert.DeserializeObject<List<string>>(text);
                }
                foreach (var item in lst)
                {
                    if (item == color)
                    {
                        lst.Remove(color);
                        string texte = JsonConvert.SerializeObject(lst);
                        using (StreamWriter SW = File.CreateText("Main_Foldr" + '/' + username + '/' + "pallete.txt"))
                        {
                            SW.WriteLine(texte);
                        }
                        return 1;
                    }
                }
                return 0;
            }
            catch { return -1; }
        }

        static int degel = 0;
        // פונקציה E פלטת צבעים בסיסית
        public static void BasicPalette()
        {
            List<string> lst = new List<string>();
            lst.Add("ff0000");
            lst.Add("0f47e2");
            lst.Add("ffd800");
            if (degel == 0)
            {
                Users.CreateNewUser("General", "123456789");
                CreatePalette("General", lst);
                degel = 1;//תמומש פעם אחת
            }
        }
    }
}