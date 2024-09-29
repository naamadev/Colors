using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace FinalProlectWeb
{
    public class Model
    {
        public void Create_Folder()
        {
            Directory.CreateDirectory(@"Main_Foldr");
            using (StreamWriter SW = File.CreateText(@"Main_Foldr/Users.txt"))
            {
                SW.WriteLine();
            }

        }
    }
    public class Users
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DateCreate { get; set; }
        public int AmountArts { get; set; }
        public static string ManagerName { get; set; } = "Naama";
        public static int ManagerCode { get; set; } = 214312837;

        //פונקציה A יצירת משתמש
        public static int CreateNewUser(string UserName, string Password)
        {
            List<Users> lstu = new List<Users>();
            Users u = new Users();
            u.UserName = UserName;
            u.Password = Password;
            u.AmountArts = 0;
            u.DateCreate = DateTime.Now;
            try
            {
                if (!File.Exists(@"/Main_Foldr" + '/' + UserName + "/pallete.txt"))
                {
                    Directory.CreateDirectory("Main_Foldr" + '/' + UserName);
                    using (StreamWriter SW = File.CreateText("Main_Foldr" + '/' + UserName + '/' + "pallete.txt"))
                    {
                    }
                    using (StreamWriter SW = File.CreateText("Main_Foldr" + '/' + UserName + '/' + "items.txt"))
                    {
                    }
                    using (StreamReader SR = File.OpenText(@"Main_Foldr/Users.txt"))
                    {
                        string text = SR.ReadToEnd();
                        lstu = JsonConvert.DeserializeObject<List<Users>>(text);
                        if (lstu == null)
                        {
                            lstu = new List<Users>();
                        }
                    }
                    lstu.Add(u);
                    string texte = JsonConvert.SerializeObject(lstu);
                    using (StreamWriter SW = File.CreateText(@"Main_Foldr/Users.txt"))
                    {
                        SW.WriteLine(texte);
                    }
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch { return -1; }
        }


        //פונקציה B אימות משתמש
        public static bool Check(string username, string password)
        {
            bool degel = false;
            string text = " ";
            List<Users> lstu = new List<Users>();
            using (StreamReader SR = File.OpenText(@"Main_Foldr/Users.txt"))
            {
                text = SR.ReadToEnd();
            }
            lstu = JsonConvert.DeserializeObject<List<Users>>(text);
            foreach (var i in lstu)
            {
                if (i.UserName == username && i.Password == password)
                {
                    degel = true;
                    break;
                }
            }
            return degel;
        }

        //פונקציה C קבלת רשימת משתמשים
        public static List<Users> ReturnUsers()
        {
            List<Users> lstu = new List<Users>();
            using (StreamReader SR = File.OpenText(@"Main_Foldr/Users.txt"))
            {
                string text = SR.ReadToEnd();
                lstu = JsonConvert.DeserializeObject<List<Users>>(text);
            }
            return lstu;
        }

        //פונקציה D קבלת ציורים לפי שם משתמש
        public static List<Board> ReturnDraws(string UserName)
        {
            List<string> lst = new List<string>();
            List<Board> lstb = new List<Board>();
            using (StreamReader SR = File.OpenText("Main_Foldr" + '/' + UserName + '/' + "items.txt"))
            {
                string text = SR.ReadToEnd();
                lst = JsonConvert.DeserializeObject<List<string>>(text);
            }
            if (lst == null)
                return null;
            foreach(var x in lst)
            {
               lstb.Add(Board.OpenBoard(UserName, x, true));
            }
            return lstb;
        }

        //הפונקציה E קבלת כל הציורים השמורים
        public static List<Board> AllDraws()
        {
            List<Board> lstb = new List<Board>();
            List<Board> lstb2 = new List<Board>();
            List<Users> lstu = new List<Users>();
            using (StreamReader SR = File.OpenText(@"Main_Foldr/Users.txt"))
            {
                string text = SR.ReadToEnd();
                lstu = JsonConvert.DeserializeObject<List<Users>>(text);
            }
            foreach(var t in lstu)
            {
                lstb2 = ReturnDraws(t.UserName);
                if (lstb2 == null)
                    continue;
                lstb.AddRange(lstb2);
            }
            return lstb;
        }

        //פונקציה F מחיקת משתמש
        public static int DeleteUser(string UserName, string managername, int managercode)
        {
            int degel = 0;
            List<Users> lstu = new List<Users>();
            try
            {
                if (ManagerName == managername && ManagerCode == managercode)
                {
                    using (StreamReader SR = File.OpenText(@"Main_Foldr/Users.txt"))
                    {
                        string text = SR.ReadToEnd();
                        lstu = JsonConvert.DeserializeObject<List<Users>>(text);
                    }
                    foreach (var i in lstu)
                    {
                        if (i.UserName == UserName)
                        {
                            degel = 1;
                            lstu.Remove(i);
                            break;
                        }
                    }
                    if (degel == 0)
                    {
                        return 3;
                    }
                    string texte = JsonConvert.SerializeObject(lstu);
                    using (StreamWriter SW = File.CreateText(@"Main_Foldr/Users.txt"))
                    {
                        SW.WriteLine(texte);
                    }
                    List<string> l = new List<string>();
                    using (StreamReader SR = File.OpenText("Main_Foldr" + '/' + UserName + '/' + "items.txt"))
                    {
                        string text = SR.ReadToEnd();
                        l = JsonConvert.DeserializeObject<List<string>>(text);
                    }
                    foreach(var a in l)
                    {
                        var x = a + ".txt";
                        File.Delete("Main_Foldr/" + UserName +'/'+ x);
                    }
                    File.Delete("Main_Foldr/" + UserName + "/items.txt");
                    File.Delete("Main_Foldr/" + UserName + "/pallete.txt");
                    Directory.Delete("Main_Foldr/" + UserName);
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch (Exception e)
            {
                var x = e.Message;
                return 4;
            }
        }

        public static bool CheckManager(string managername, int managercode)//אימות מנהל
        {
            if (ManagerName == managername && ManagerCode == managercode)
                return true;
            return false;
        }
    }
}
