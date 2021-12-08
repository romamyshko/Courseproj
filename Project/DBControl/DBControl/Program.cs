using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DBControl.Models;

namespace DBControl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string jsFilepath = "../../../../../../data/application.json";
            DbConnectionInfo connection = GetConnectionFromJson(jsFilepath);

            UniversityContext context = new UniversityContext(connection);

            context.Add(new Faculty());
        }

        static DbConnectionInfo GetConnectionFromJson(string filePathToJson)
        {
            return JsonSerializer.Deserialize<DbConnectionInfo>(new StreamReader(filePathToJson).ReadToEnd());
        }
    }
}
