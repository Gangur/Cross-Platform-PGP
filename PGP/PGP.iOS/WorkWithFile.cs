﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using PGP.PlatformSpecificInterface;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PGP.iOS.WorkWithFile))]
namespace PGP.iOS
{
    class WorkWithFile : IWorkWithFile
    {
        public Task DeleteAsync(string filename)
        {
            File.Delete(GetFilePath(filename));
            return Task.FromResult(true);
        }

        public Task<bool> ExistsAsync(string filename)
        {
            string filepath = GetFilePath(filename);
            bool exists = File.Exists(filepath);
            return Task<bool>.FromResult(exists);
        }

        public Task<IEnumerable<string>> GetFilesAsync()
        {
            IEnumerable<string> filenames = from filepath in Directory.EnumerateFiles(GetDocsPath())
                                            select Path.GetFileName(filepath);
            return Task<IEnumerable<string>>.FromResult(filenames);
        }

        public async Task<string> LoadTextAsync(string filename)
        {
            string filepath = GetFilePath(filename);
            using (StreamReader reader = File.OpenText(filepath))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public async void SaveTextAsync(string filename, string text)
        {
            string filepath = GetFilePath(filename);
            using (StreamWriter writer = File.CreateText(filepath))
            {
                await writer.WriteAsync(text);
            }
        }

        string GetFilePath(string filename)
        {
            return Path.Combine(GetDocsPath(), filename);
        }
        string GetDocsPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
    }
}