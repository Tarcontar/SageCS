﻿using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    class LoadSubsystem
    {
        public string Loader;
        public List<string> initFiles = new List<string>();
        public List<string> initPaths = new List<string>();
        public List<string> extensions = new List<string>(); //unused
        public List<string> initFilesDebug = new List<string>();
        public List<string> excludePaths = new List<string>();
        public List<string> includePathsCinematics = new List<string>();

        public void LoadFiles()
        {
            foreach (string s in initFiles)
            {
                new INIParser(FileSystem.Open(s));
            }
            foreach (string s in initPaths)
            {
                List<Stream> files = FileSystem.OpenAll(s, excludePaths);
                foreach(Stream st in files)
                {
                    new INIParser(st);
                }
            }
        }

        public void AddInitFile(string file)
        {
            initFiles.Add(file);
        }

        public void AddInitFileDebug(string file)
        {
            initFilesDebug.Add(file);
        }

        public void AddInitPath(string path)
        {
            initPaths.Add(path);
        }

        public void AddIncludePathCinematics(string path)
        {
            includePathsCinematics.Add(path);
        }

        public void AddExcludePath(string path)
        {
            excludePaths.Add(path);
        }

    }
}