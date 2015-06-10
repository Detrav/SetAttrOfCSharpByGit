using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetAttrOfCSharpByGit
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length>0)
            {
                switch(args[0])
                {
                    case "JustCommitCount":
                        if (args.Length > 2)
                        {
                            if (justCommitCount(args[1], args[2]))
                                return;
                        }
                        break;
                    case "Backup":
                        if (args.Length > 1)
                            if (backup(args[1]))
                                return;
                        break;
                    case "ByTagName":
                        if (args.Length > 3)
                            if (byTagName(args[1], args[2],args[3]))
                                return;
                        break;
                }
            }
            Console.WriteLine("SetAttrOfCSharpByGit - Полуавтоматическое назначение версий");
            Console.WriteLine("SetAttrOfCSharpByGit JustCommitCount {Working Dir} {File Path} - Замена подстроки в файле на количество коммитов");
            Console.WriteLine("SetAttrOfCSharpByGit Backup {File Path} - Востановление файла после компиляции");
            Console.WriteLine("SetAttrOfCSharpByGit ByTagName {Working Dir} {Result File} {File Patch} - Замена результрующего файла на патч файл и на текущую версию в теге.");
            Console.WriteLine(@"Подробная инструкция: https://github.com/Detrav");
        }

        private static bool justCommitCount(string p1, string p2)
        {
            int version = 0;
            #region getVersion
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.WorkingDirectory = p1;
            p.StartInfo.FileName = "git";
            p.StartInfo.Arguments = "rev-list HEAD --count";
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            //Console.WriteLine(output);
            version = Int32.Parse(output);
            #endregion getVersion
            #region createAssembleInfo
            if (!File.Exists(p1)) return false;
            File.Move(p2, p2 + ".backup");
            using (TextReader tr = new StreamReader(p2 + ".backup"))
            {
                string text = tr.ReadToEnd();
                text = text.Replace("{GitCommitsCount}", version.ToString());
                using (TextWriter tw = new StreamWriter(p2))
                {
                    tw.Write(text);
                }
            }
            #endregion createAssembleInfo
            return true;
        }

        static public bool backup(string p1)
        {
            if (!File.Exists(p1 + ".backup")) return false;
            if (File.Exists(p1)) File.Delete(p1);
            File.Move(p1 + ".backup", p1);
            return true;
        }

        static public bool byTagName(string p1,string p2,string p3)
        {
            #region getVersion
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.WorkingDirectory = p1;
            p.StartInfo.FileName = "git";
            p.StartInfo.Arguments = "describe --tags --exact-match";
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            Console.WriteLine(p.ExitCode);
            //Console.WriteLine(output);
            //version = Int32.Parse(output);
            #endregion getVersion
            return true;
        }

    }
}
