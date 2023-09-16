#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace BuildUtility
{
    internal static class BuildPlayer
    {
        /// <summary>
        /// <para>日期格式</para>
        /// <para>详情见：<see href="https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings"/></para>
        /// </summary>
        private const string M_DATA_FORMAT = "yyyy-MM-dd-HH-mm-ss";

        /// <summary>
        /// Build主函数
        /// </summary>
        [MenuItem("Tools/Build")]
        private static void Build()
        {
            // 初始化日志文件
            string date = DateTime.Now.ToString(M_DATA_FORMAT); //获取日期，并按M_DATA_FORMAT格式化
            string logFileName = $"{Application.persistentDataPath}/BuildLog_{date}.txt";   // 日志文件路径
            FileStream log = File.Open(logFileName, FileMode.OpenOrCreate); // 创建日志文件
            
            LogLine(log, "Start Build");
            LogLine(log, $"Build Time: {date}");
            LogLine(log, "");

            // 获取环境变量
            string[] args = Environment.GetCommandLineArgs();
            LogStringArray(log, args, "Arguments: ");

            // 获取scenes参数
            string[] scenes = GetArgument(args, "scenes", log);
            if (scenes == null || scenes.Length <= 0) return;

            // 获取targetPath参数
            string[] targetPath = GetArgument(args, "targetPath", log);
            if (targetPath == null || targetPath.Length <= 0) return;
            
            // 设置打包参数
            BuildPlayerOptions options = new BuildPlayerOptions();
            options.target = BuildTarget.StandaloneWindows64;
            options.scenes = scenes;
            options.locationPathName = targetPath[0];
            BuildPipeline.BuildPlayer(options);
        }

        /// <summary>
        /// 获取某个参数
        /// </summary>
        /// <param name="args">全部的命令行参数</param>
        /// <param name="name">要获取的参数名称</param>
        /// <param name="fs">日志文件</param>
        /// <param name="shouldLog">是否在日志中输出参数</param>
        /// <returns>所求参数</returns>
        private static string[] GetArgument(string[] args, string name, FileStream fs, bool shouldLog = true)
        {
            int start = Array.FindIndex(args, arg => arg == $"-{name}");
            if (start < 0)
            {
                LogLine(fs, $"Can not find argument: {name}");
                return null;
            }
            start++;
            int end = Array.FindIndex(args, start, arg => arg[0] == '-');
            if (end < 0) end = args.Length;
            int count = end - start;
            if (count <= 0)
            {
                LogLine(fs, $"Can not find argument value: {name}, Count: {count}, Start: {start}, End: {end}");
                return null;
            }

            string[] result = args.Skip(start).Take(count).ToArray();
            if(shouldLog) LogStringArray(fs, result, $"Argument [{name}]: ");
            return result;
        }

        #region Log Utility

        /// <summary>
        /// 通过FileStream输出日志
        /// </summary>
        /// <param name="fs">日志文件</param>
        /// <param name="text">内容</param>
        private static void Log(FileStream fs, string text)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(text);
            fs.Write(info, 0, info.Length);
        }

        /// <summary>
        /// 按行输出日志
        /// </summary>
        private static void LogLine(FileStream fs, string text) => Log(fs, $"{text}\n");

        /// <summary>
        /// 按行输出数组中的值，一个值一行
        /// </summary>
        /// <param name="fs">日志文件</param>
        /// <param name="array">数组</param>
        /// <param name="title">数组名称</param>
        private static void LogStringArray(FileStream fs, string[] array, string title)
        {
            LogLine(fs, $"{title}");
            for (int i = 0; i < array.Length; i++)
            {
                LogLine(fs, $"{array[i]}");
            }
            LogLine(fs, "");
        }

        #endregion
    }
}


#endif