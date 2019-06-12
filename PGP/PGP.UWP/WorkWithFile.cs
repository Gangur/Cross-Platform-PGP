using PGP.PlatformSpecificInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(PGP.UWP.WorkWithFile))]
namespace PGP.UWP
{
    class WorkWithFile : IWorkWithFile
    {
        public async Task DeleteAsync(string filename)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await localFolder.GetFileAsync(filename);
            await storageFile.DeleteAsync();
        }

        public async Task<bool> ExistsAsync(string filename)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                await localFolder.GetFileAsync(filename);
            }
            catch { return false; }
            return true;
        }

        public async Task<IEnumerable<string>> GetFilesAsync()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            IEnumerable<string> filenames = from storageFile in await localFolder.GetFilesAsync()
                                            select storageFile.Name;
            return filenames;
        }

        public async Task<string> LoadTextAsync(string filename)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            // получаем файл
            StorageFile helloFile = await localFolder.GetFileAsync(filename);
            // читаем файл


            string text = await FileIO.ReadTextAsync(helloFile);
            return text;
        }

        public async void SaveTextAsync(string fileWay, string text)
        {
            //Индекс вхождения //
            int index = fileWay.LastIndexOf('\\');
            //Путь директории файла
            string PathFolder = fileWay.Substring(0, index);
            //Имя файла
            string FileName = fileWay.Substring(index+1, fileWay.Length-1-index);
            // получаем локальную папку с файлом
            StorageFolder localFolder = await StorageFolder.GetFolderFromPathAsync(PathFolder);
            Thread.Sleep(100);
            // получаем нужный файл
            StorageFile File = await localFolder.GetFileAsync(FileName);
            Thread.Sleep(100);
            // запись в файл
            await FileIO.WriteTextAsync(File, text);
            Thread.Sleep(100);
        }

        public async void RenameAsync(string fileWay, string newName)
        {
            //Индекс вхождения //
            int index = fileWay.LastIndexOf('\\');
            //Путь директории файла
            string PathFolder = fileWay.Substring(0, index);
            //Имя файла
            string FileName = fileWay.Substring(index + 1, fileWay.Length - 1 - index);
            // получаем локальную папку с файлом
            StorageFolder localFolder = await StorageFolder.GetFolderFromPathAsync(PathFolder);
            Thread.Sleep(100);
            // получаем нужный файл
            StorageFile File = await localFolder.GetFileAsync(FileName);
            Thread.Sleep(100);
            // Переименовываем файл
            await File.RenameAsync(newName);
            Thread.Sleep(100);
        }

        public async void MoveToAsync(string fileWay, string newWay)
        {
            //Индекс вхождения //
            int index = fileWay.LastIndexOf('\\');
            //Путь директории файла
            string PathFolder = fileWay.Substring(0, index);
            //Имя файла
            string FileName = fileWay.Substring(index + 1, fileWay.Length - 1 - index);
            // получаем локальную папку с файлом
            StorageFolder localFolder = await StorageFolder.GetFolderFromPathAsync(PathFolder);
            Thread.Sleep(100);
            // получаем нужный файл
            StorageFile File = await localFolder.GetFileAsync(FileName);
            Thread.Sleep(100);
            //Получаем нужную папку
            StorageFolder newFolder = await StorageFolder.GetFolderFromPathAsync(newWay);
            Thread.Sleep(100);
            //Перемещаем файл в нужную папку
            File.MoveAsync(newFolder);
            Thread.Sleep(100);
        }
    }
}
