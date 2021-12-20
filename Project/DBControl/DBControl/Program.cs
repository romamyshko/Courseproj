using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using DBControl.Controllers;
using DBControl.Models;
using DBControl.Views;

namespace DBControl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string jsFilepath = "../../../../../../data/application.json";
            DbConnectionInfo connection = GetConnectionFromJson(jsFilepath);

            UniversityContext context = new UniversityContext(connection);
            StartWindow startWindow = new StartWindow(context);
            startWindow.Run();
        }

        static DbConnectionInfo GetConnectionFromJson(string filePathToJson)
        {
            return JsonSerializer.Deserialize<DbConnectionInfo>(new StreamReader(filePathToJson).ReadToEnd());
        }
    }
}
