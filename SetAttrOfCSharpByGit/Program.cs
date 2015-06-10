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
            if (args.Length == 3)
            {
                byTagName(args[0], args[1], args[2]);
            }
            if (args.Length == 1)
            {
                backup(args[0]);
            }
        }

        static public bool backup(string p1)
        {
            if (!File.Exists(p1 + ".backup")) return false;
            if (File.Exists(p1)) File.Delete(p1);
            File.Move(p1 + ".backup", p1);
            return true;
        }

        static char[] digits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', };

        static public bool byTagName(string p1, string p2, string p3)
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
            if (p.ExitCode != 0) return false;
            //version = Int32.Parse(output);

            string[] vers = output.Split('.');
            if (vers.Length < 3) return false;
            int magor = getEndIntFromString(vers[0]);
            int minor = getIntFromString(vers[1]);
            int build = getStartIntFromString(vers[2]);
            #region Revison
            p.StartInfo.Arguments = "rev-list HEAD --count";
            p.Start();
            output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            int revision = Int32.Parse(output);
            #endregion Revision
            string version = string.Format("{0}.{1}.{2}.{3}", magor, minor, build, revision);
            #endregion getVersion
            if (!File.Exists(p2)) return false;
            if (!File.Exists(p3)) return false;
            if (!File.Exists(p2 + ".backup"))
                File.Move(p2, p2 + ".backup");
            else
                File.Delete(p2);
            using (TextReader tr = new StreamReader(p3))
            {
                string text = tr.ReadToEnd();
                text = text.Replace("{GitTagVersion}", version.ToString());
                using (TextWriter tw = new StreamWriter(p2))
                {
                    tw.Write(text);
                }
            }
            return true;
        }

        static public int getEndIntFromString(string str)
        {
            int i = str.Length;
            do
            {
                i--;
            } while (digits.Contains(str[i]) && i >= 0);
            if (Int32.TryParse(str.Substring(i + 1), out i))
                return i;
            return 0;
        }

        static public int getStartIntFromString(string str)
        {
            int i = -1;// 10-rc // 10
            do
            {
                i++;
            } while (digits.Contains(str[i]) && i < str.Length);
            if (Int32.TryParse(str.Substring(0, i), out i))
                return i;
            return 0;
        }

        static public int getIntFromString(string str)
        {
            int i = 0;
            if (Int32.TryParse(str, out i))
                return i;
            return 0;
        }
    }
}
