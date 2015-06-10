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

            //Command (start {GitDir} {FilePath})
            //Command (stop {FilePath})
            default_main(args);
        }

        //Command (start {GitDir} {FilePath})
        //Command (stop {FilePath})
        static public void default_main(string[] args)
        {
            if (args.Length < 2)
                return;
            switch (args[0])
            {
                case "start":
                    if (args.Length < 3)
                        return;
                    int version = 0;
                    #region getVersion
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.WorkingDirectory = args[1];
                    p.StartInfo.FileName = "git";
                    p.StartInfo.Arguments = "rev-list HEAD --count";
                    p.Start();
                    string output = p.StandardOutput.ReadToEnd();
                    p.WaitForExit();
                    //Console.WriteLine(output);
                    version = Int32.Parse(output);
                    #endregion getVersion
                    #region createAssembleInfo
                    if (!File.Exists(args[2])) return;
                    File.Move(args[2], args[2] + ".backup");
                    using (TextReader tr = new StreamReader(args[2] + ".backup"))
                    {
                        string text = tr.ReadToEnd();
                        text = text.Replace("{GitCommitsCount}", version.ToString());
                        using (TextWriter tw = new StreamWriter(args[2]))
                        {
                            tw.Write(text);
                        }
                    }
                    #endregion createAssembleInfo
                    break;
                case "end":
                    if (!File.Exists(args[1] + ".backup")) return;
                    if (File.Exists(args[1])) File.Delete(args[1]);
                    File.Move(args[1] + ".backup", args[1]);
                    break;
            }
        }

    }
}
