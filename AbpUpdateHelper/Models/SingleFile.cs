﻿using System;
using System.IO;

namespace AbpUpdateHelper
{
    public class SingleFile
    {
        private readonly string _projectName;
        private string[] _fileContentLines;

        public SingleFile(FileInfo fileInfo, string abpProjectName)
        {
            File = fileInfo;

            _projectName = abpProjectName;
        }

        public FileInfo File { get; }

        public string FileContent => string.Join('\n', FileContentLines);

        public string[] FileContentLines => _fileContentLines ?? (_fileContentLines = System.IO.File.ReadAllLines(File.FullName));

        public string RelativeDirectory => File.DirectoryName?.Split($"\\{_projectName}\\")[1].ToLower();

        public string RelativePath => RelativeDirectory + "\\" + File.Name.ToLower();

        public bool IsAutoGenerated()
        {
            if (FileContent.IndexOf("// <auto-generated />", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            if (FileContent.IndexOf("AUTOGENERATED", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            return false;
        }
    }
}