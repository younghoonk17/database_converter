using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace bible
{
    class Program
    {
        static int previous_chapter = 0;
        static string previous_book = "";
        static int previous_verse = 1;
        static string final_string = "{\"bible\":{";
        static void Main(string[] args)
        {
            //filtering();
            readfile();


        }

        static private void filtering()
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"E:\Git\bible\bible.txt");
            line = file.ReadLine();
            string[] tmp = Regex.Split(line, @"\d");
            System.Console.WriteLine(tmp[0]);
        }

        static private string regexer(string input) {

            string[] chapterVerseRegex = Regex.Split(input, @"\D+");
            string[] bookRegex = Regex.Split(input, @"\d+");
            string[] textRegex = Regex.Split(input, @":\d+\s");

            var bible = new Bible(); 

            bible.book = Abbreviator(bookRegex[0]);
            bible.chapter = Int32.Parse(chapterVerseRegex[1]);
            bible.verse = Int32.Parse(chapterVerseRegex[2]);
            bible.text = textRegex[1];
            string output ="";

            //Add bible book names
            if (bible.chapter == 1 && bible.verse == 1 && previous_book == "")
            {
                output += "\"" + bible.book + "\":{";
                previous_book = bible.book;

            }
            else if (previous_book != bible.book)
            {
                output += "}},\"" + bible.book + "\":{";
                previous_book = bible.book;
            }

            //Add bible chapter names
            if (bible.verse == 1 && bible.chapter == 1)
            {
                output += "\"" + bible.chapter + "\":{";
                previous_chapter = bible.chapter;
            }
            else if (bible.verse == 1)
            {
                output += "},\"" + bible.chapter + "\":{";
                previous_chapter = bible.chapter;
            }
            
            //Add bible verse names
            if (bible.verse ==1 )
            {
                output += "\"" + bible.verse + "\":" + JsonConvert.SerializeObject(bible);
                previous_verse = bible.verse;
            }
            else
            {
                output += ",\"" + bible.verse + "\":" + JsonConvert.SerializeObject(bible);
                previous_verse = bible.verse;
            }

            return output;
        }

        static private string Abbreviator(string input)
        {
            Dictionary<string, string> bible_dic = new Dictionary<string, string>();

            bible_dic.Add("창", "창세기");
            bible_dic.Add("출","출애굽기");
            bible_dic.Add("레","레위기");
            bible_dic.Add("민","민수기");
            bible_dic.Add("신","신명기");
            bible_dic.Add("수","여호수아");
            bible_dic.Add("삿","사사기");
            bible_dic.Add("룻","룻기");
            bible_dic.Add("삼상", "사무엘상");
            bible_dic.Add("삼하", "사무엘하");
            bible_dic.Add("왕상", "열왕기상");
            bible_dic.Add("왕하", "열왕기하");
            bible_dic.Add("대상", "역대상");
            bible_dic.Add("대하", "역대하");
            bible_dic.Add("스", "에스라");
            bible_dic.Add("느", "느헤미야");
            bible_dic.Add("에", "에스더");
            bible_dic.Add("욥", "욥기");
            bible_dic.Add("시", "시편");
            bible_dic.Add("잠", "잠언");
            bible_dic.Add("전", "전도서");
            bible_dic.Add("아", "아가");
            bible_dic.Add("사", "이사야");
            bible_dic.Add("렘", "예레미야");
            bible_dic.Add("애", "예레미야애가");
            bible_dic.Add("겔", "에스겔");
            bible_dic.Add("단", "다니엘");
            bible_dic.Add("호", "호세아");
            bible_dic.Add("욜", "요엘");
            bible_dic.Add("암", "아모스");
            bible_dic.Add("옵", "오바댜");
            bible_dic.Add("욘", "요나");
            bible_dic.Add("미", "미가");
            bible_dic.Add("나", "나훔");
            bible_dic.Add("합", "하박국");
            bible_dic.Add("습", "스바냐");
            bible_dic.Add("학", "학개");
            bible_dic.Add("슥", "스가랴");
            bible_dic.Add("말", "말라기");
            bible_dic.Add("마", "마태복음");
            bible_dic.Add("막", "마가복음");
            bible_dic.Add("눅", "누가복음");
            bible_dic.Add("요", "요한복음");
            bible_dic.Add("행", "사도행전");
            bible_dic.Add("롬", "로마서");
            bible_dic.Add("고전", "고린도전서");
            bible_dic.Add("고후", "고린도후서");
            bible_dic.Add("갈", "갈라디아서");
            bible_dic.Add("엡", "에베소서");
            bible_dic.Add("빌", "빌립보서");
            bible_dic.Add("골", "골로새서");
            bible_dic.Add("살전", "데살로니가전서");
            bible_dic.Add("살후", "데살로니가후서");
            bible_dic.Add("딤전", "디모데전서");
            bible_dic.Add("딤후", "디모데후서");
            bible_dic.Add("딛", "디도서");
            bible_dic.Add("몬", "빌레몬서");
            bible_dic.Add("히", "히브리서");
            bible_dic.Add("약","야고보서");
            bible_dic.Add("벧전", "베드로전서");
            bible_dic.Add("벧후", "베드로후서");
            bible_dic.Add("요일", "요한일서");
            bible_dic.Add("요이", "요한이서");
            bible_dic.Add("요삼", "요한삼서");
            bible_dic.Add("유", "유다서");
            bible_dic.Add("계", "요한계시록");

            return bible_dic.GetValueOrDefault(input);
            
        }

        static private void readfile()
        {
            int counter = 0;
            string line; 

            System.IO.StreamReader file = new System.IO.StreamReader(@".\bible.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = regexer(line);

                final_string += output;

                counter++;
            }
            final_string += "}}}}";
            using (System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(@"C:\Users\Yh\Desktop\output.json", true))
            {
                fileWriter.WriteLine(final_string);
            }
            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);
        }
    }
}
