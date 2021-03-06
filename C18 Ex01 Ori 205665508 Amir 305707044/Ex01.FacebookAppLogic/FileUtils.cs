﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Ex01.FacebookAppLogic
{
    public static class FileUtils
    {
        public static T LoadFromFile<T>(string i_FilePath)
        {
            T settings;

            StreamReader fileToLoad = null;
            fileToLoad = File.OpenText(i_FilePath);
            JsonSerializer serializer = new JsonSerializer();
            settings = (T)serializer.Deserialize(fileToLoad, typeof(T));

            return settings;
        }

        public static void SaveToFile<T>(T i_ObjectTowrite, string i_FilePath)
        {
            using (FileStream fileToSave = File.Open(i_FilePath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fileToSave))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, i_ObjectTowrite);
            }
        }

        public static void DeleteFile(string i_FilePath)
        {
            File.Delete(i_FilePath);
        }
    }
}
