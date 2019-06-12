using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace PGP.PlatformSpecificInterface
{
    public interface IWorkWithFile
    {
        Task<bool> ExistsAsync(string fileWay); // проверка существования файла
        void SaveTextAsync(string fileWay, string text);   // сохранение текста в файл
        Task<string> LoadTextAsync(string fileWay);  // загрузка текста из файла
        Task<IEnumerable<string>> GetFilesAsync();  // получение файлов из определнного каталога
        Task DeleteAsync(string fileWay);  // удаление файла
        void RenameAsync(string fileWay, string newName);  // Переименовывать файла
        void MoveToAsync(string fileWay, string newWay);
    }
}
