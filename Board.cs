using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace FinalProlectWeb
{
    public class Board
    {
        public int Size { get; set; }
        public bool IsArchive { get; set; }
        public string UserName { get; set; }
        public string FileName { get; set; }
        public string BackGroundColor { get; set; }
        public bool IsDisplay { get; set; }//האם להציג קווי רשת
        public string [][] mat { get; set; }//מטריצה
        
        //פונקציה A--יצירת לוח חדש
        public static  int CreateNewBoard(string UserName, string namefile, int size)
        {
            try
            {
                //string namefilee = namefile + ".txt";
                //File.CreateText("Main_Foldr" + '/' + UserName + '/' + namefilee);
                List<string> lst = new List<string>();
                using (StreamReader SR = File.OpenText("Main_Foldr" + '/' + UserName + '/' + "items.txt"))
                {
                    string text = SR.ReadToEnd();
                    lst = JsonConvert.DeserializeObject<List<string>>(text);
                }
                if(lst==null)
                {
                    lst = new List<string>();
                }
                foreach (var i in lst)
                {
                    if (i == namefile)
                    {
                        return 0;
                    }
                }
                lst.Add(namefile);
                List<Users> lstu = new List<Users>();
                lstu = Users.ReturnUsers();
                foreach (var j in lstu)
                {
                    if (j.UserName == UserName)
                    {
                        j.AmountArts++;
                    }
                }
                string texte = JsonConvert.SerializeObject(lst);
                using (StreamWriter SW = File.CreateText("Main_Foldr" + '/' + UserName + '/' + "items.txt"))
                {
                    SW.WriteLine(texte);
                }
                return 1;
            }
            catch { return -1; }
        }

        //פונקציה B--פתיחת לוח ציור 
        public static Board OpenBoard(string UserName,string filename,bool isarchiv) 
        {
            Board b = new Board();
            string namefilee = filename + ".txt";
            using (StreamReader SR = File.OpenText("Main_Foldr" + '/' + UserName + '/' + namefilee))
            {
                string text = SR.ReadToEnd();
                b = JsonConvert.DeserializeObject<Board>(text);
            }
            if (b == null)
                b = new Board();
            b.IsArchive = isarchiv;
            return b;
        }

        //פונקציה C--מחיקת ציור
        public static int DeleteBoard(string UserName, string filename)
        {
            try {
                if (!File.Exists("Main_Foldr" + '/' + UserName + '/' + filename))
                {
                    List<Users> lstu = new List<Users>();
                    List<string> lst = new List<string>();
                    string filenamee = filename + ".txt";
                    File.Delete("Main_Foldr" + '/' + UserName + '/' + filenamee);
                    using (StreamReader SR = File.OpenText("Main_Foldr" + '/' + UserName + '/' + "items.txt"))
                    {
                        string text = SR.ReadToEnd();
                        lst = JsonConvert.DeserializeObject<List<string>>(text);
                    }
                    if(lst==null)
                    {
                        lst = new List<string>();
                    }
                    lst.Remove(filename);
                    string texte = JsonConvert.SerializeObject(lst);
                    using (StreamWriter SW = File.CreateText("Main_Foldr" + '/' + UserName + '/' + "items.txt"))
                    {
                        SW.WriteLine(texte);
                    }
                    using (StreamReader SR = File.OpenText(@"Main_Foldr/Users.txt"))
                    {
                        string text = SR.ReadToEnd();
                        lstu = JsonConvert.DeserializeObject<List<Users>>(text);
                    }
                    foreach (var item in lstu)
                    {
                        if (item.UserName == UserName)
                        {
                            item.AmountArts--;
                        }
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

        //פונקציה D--שמירת ציור
        public static int Shmor(Board b)
        {
            try
            {
                //string filenamee = b.FileName + ".txt";
                //if (File.Exists("Main_Foldr" + '/' + b.UserName + '/' + filenamee))
                //{
                //    return 1;//קובץ קיים
                //}
                string texte = JsonConvert.SerializeObject(b);
                string filename = b.FileName + ".txt";
                using (StreamWriter SW = File.CreateText("Main_Foldr" + '/' + b.UserName + '/' + filename))
                {
                    SW.WriteLine(texte);
                }

                List<string> lst = new List<string>();
                using (StreamReader SR = File.OpenText("Main_Foldr" + '/' + b.UserName + '/' + "items.txt"))
                {
                    string text = SR.ReadToEnd();
                    lst = JsonConvert.DeserializeObject<List<string>>(text);
                }
                if (lst == null)
                {
                    lst = new List<string>();
                }
                foreach (var i in lst)
                {
                    if (i == b.FileName)
                    {
                        return 3;
                    }
                }
                lst.Add(b.FileName);
                string texte1 = JsonConvert.SerializeObject(lst);
                using (StreamWriter SW = File.CreateText("Main_Foldr" + '/' + b.UserName + '/' + "items.txt"))
                {
                    SW.WriteLine(texte1);
                }
                return 0;//הצלחה             
            }
            catch { return -1; }//תקלה אחרת
        }

        public static int Writee(Board b) //כתיבה לקובץ
        {
            try
            {
                string texte = JsonConvert.SerializeObject(b);
                using (StreamWriter SW = File.CreateText("Main_Foldr" + '/' + b.UserName + '/' + b.FileName))
                {
                    SW.WriteLine(texte);
                }
                return 0;
            }
            catch { return -1; }

        }

    }

}
