using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceToTextBot.Extensions
{
    public static class DirectoryExtension
    {
        /// <summary>
        /// Получаем путь до каталога с .sln файлом
        /// </summary>
        public static string GetSolutionRoot()
        {
            var dir = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            var fullname = Directory.GetParent(dir).FullName;
            return fullname; 
        }
    }
}
