using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KindaGoodUtility
{
    public static class JsonHelp
    {
        public static string DefaultDatapath { get { return Application.dataPath + "/Data/"; } }
        /// <summary>
        /// Creates a Directory for the Data
        /// </summary>
        /// <param name="dataPath">The data path of the directory</param>
        public static void CreateDirectory(string dataPath)
        {
            if (string.IsNullOrEmpty(dataPath))
            {
                dataPath = Application.dataPath + "/Data/";
            }

            // Creates Directory for the Data if it doesnt exist
            if (!File.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
        }
        /// <summary>
        /// Creates a Directory for the Data and an empty File
        /// </summary>
        /// <param name="dataPath">The data path of the directory</param>
        /// <param name="fileName">the name of the created json file</param>
        public static void CreateDirectory(string dataPath, string fileName)
        {
            // Creates Directory for the Data if it doesnt exist
            if (string.IsNullOrEmpty(dataPath))
            {
                dataPath = Application.dataPath + "/Data/";
            }

            if (!File.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }


            // creates a .json file if there is none
            if (File.Exists(dataPath + fileName)) return;

            File.WriteAllText(dataPath + fileName, "");
        }
        /// <summary>
        /// Saves the data of an object inside a json file
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="obj">the object</param>
        /// <param name="dataPath">the data path of the directory</param>
        /// <param name="fileName">the file Name</param>
        public static void SaveJsonFile<T>(this T obj, string dataPath, string fileName)
        {
            if (string.IsNullOrEmpty(dataPath))
            {
                dataPath = Application.dataPath + "/Data/";
                Debug.LogWarning("Created new Directory for File");
            }

            string json = JsonUtility.ToJson(obj);
            File.WriteAllText(dataPath + fileName, json);
        }
        /// <summary>
        /// Reads a json file and returns the data as the type T
        /// </summary>
        /// <typeparam name="T">The type in which the data is returned in</typeparam>
        /// <param name="dataPath">the path of the directory</param>
        /// <param name="fileName">the name of the file</param>
        /// <returns>The data of the json file as T</returns>
        public static T ReadJsonFile<T>(string dataPath, string fileName)
        {
            if (string.IsNullOrEmpty(dataPath))
            {
                dataPath = Application.dataPath + "/Data/";
                Debug.LogWarning("Created new Directory for File");
            }
            if (!File.Exists(dataPath + fileName))
            {
                throw new NullReferenceException("File doesn't exist");
            }

            return JsonUtility.FromJson<T>(File.ReadAllText(dataPath + fileName));
        }
    }
}
