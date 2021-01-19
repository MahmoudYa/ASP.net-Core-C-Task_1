using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MvcTemplate.Rename
{
    public static class Program
    {
        public static void Main()
        {
            String project;
            String password;
            String templateName = "MvcTemplate";

            Console.Write("Enter root namespace name: ");
            while ((project = Console.ReadLine()?.Trim() ?? "").Length == 0) { }

            Console.Write("Enter new site admin user password (32 symbols max): ");
            while ((password = Console.ReadLine()?.Trim() ?? "").Length == 0) { }

            Int32 port = new Random().Next(1000, 19175);
            Int32 sslPort = new Random().Next(44300, 44400);
            String passhash = BCrypt.Net.BCrypt.HashPassword(password.Length <= 32 ? password : password.Substring(0, 32), 13);

            String[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.*", SearchOption.AllDirectories);
            Regex adminPassword = new("Passhash = \"\\$2b\\$.*\", // Will be generated on project rename");
            Regex aspNetSslConfig = new("(\"ASPNETCORE_HTTPS_PORT\": )\"\\d+\"");
            Regex applicationUrl = new("(\"applicationUrl\": .*:)\\d+(.*\")");
            Regex version = new("<Version>\\d+\\.\\d+\\.\\d+</Version>");
            Regex sslConfig = new("(\"sslPort\": )\\d+");

            Console.WriteLine();
            Console.WriteLine();

            for (Int32 i = 0; i < files.Length; i++)
            {
                Console.CursorTop--;
                Console.WriteLine($"Renaming content...     {(i + 1) * 100 / files.Length,3}%");

                String extension = Path.GetExtension(files[i]);

                if (extension == ".cs" ||
                    extension == ".cshtml" ||
                    extension == ".config" ||
                    extension == ".gitignore" ||
                    extension == ".sln" ||
                    extension == ".csproj" ||
                    extension == ".json")
                {
                    String content = File.ReadAllText(files[i]);
                    content = content.Replace(templateName, project);
                    content = sslConfig.Replace(content, $"${{1}}{sslPort}");
                    content = version.Replace(content, "<Version>0.1.0</Version>");
                    content = applicationUrl.Replace(content, $"${{1}}{port}${{2}}");
                    content = aspNetSslConfig.Replace(content, $"${{1}}\"{sslPort}\"");
                    content = adminPassword.Replace(content, $"Passhash = \"{passhash}\",");

                    File.WriteAllText(files[i], content);
                }
            }

            String[] directories = Directory.GetDirectories(Directory.GetCurrentDirectory(), $"*{templateName}*", SearchOption.AllDirectories);
            directories = directories.Where(directory => !directory.StartsWith(Path.Combine(Directory.GetCurrentDirectory(), "tools"))).ToArray();

            for (Int32 i = 0; i < directories.Length; i++)
            {
                Console.CursorLeft = 0;
                Console.Write($"Renaming directories... {(i + 1) * 100 / directories.Length,3}%");

                String projectDir = Path.Combine(Directory.GetParent(directories[i])!.FullName, directories[i].Split('\\').Last().Replace(templateName, project));
                Directory.Move(directories[i], projectDir);
            }

            Console.WriteLine();

            files = Directory.GetFiles(Directory.GetCurrentDirectory(), $"*{templateName}*", SearchOption.AllDirectories);
            files = files.Where(file => !file.Contains($"{templateName}.Rename.cmd")).ToArray();

            for (Int32 i = 0; i < files.Length; i++)
            {
                Console.CursorLeft = 0;
                Console.Write($"Renaming files...       {(i + 1) * 100 / files.Length,3}%");

                String projectFile = Path.Combine(Directory.GetParent(files[i])!.FullName, files[i].Split('\\').Last().Replace(templateName, project));
                File.Move(files[i], projectFile);
            }

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
