using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        string directory = @"C:\\Users\\User\\Desktop\\Безопасное ПО";

        while (true)
        {
            Console.WriteLine("\nДействия:");
            Console.WriteLine("1) Информация о логических дисках");
            Console.WriteLine("2) Список файлов");
            Console.WriteLine("3) Написать .txt");
            Console.WriteLine("4) Прочитать .txt");
            Console.WriteLine("5) Удалить .txt");
            Console.WriteLine("6) Создать .json");
            Console.WriteLine("7) Прочитать .json");
            Console.WriteLine("8) Удалить .json");
            Console.WriteLine("9) Создать .xml");
            Console.WriteLine("10) Прочитать .xml");
            Console.WriteLine("11) Удалить .xml");
            Console.WriteLine("12) Заархивировать в .zip");
            Console.WriteLine("13) Разархивировать");
            Console.WriteLine("14) Завершить работу программы");

            Console.Write("Выберите действие (1-14): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayDriveInfo();
                    break;
                case "2":
                    ListFiles(directory);
                    break;
                case "3":
                    WriteTxt(directory);
                    break;
                case "4":
                    ReadTxt(directory);
                    break;
                case "5":
                    DeleteTxt(directory);
                    break;
                case "6":
                    CreateJson(directory);
                    break;
                case "7":
                    ReadJson(directory);
                    break;
                case "8":
                    DeleteJson(directory);
                    break;
                case "9":
                    CreateXml(directory);
                    break;
                case "10":
                    ReadXml(directory);
                    break;
                case "11":
                    DeleteXml(directory);
                    break;
                case "12":
                    CreateZip(directory);
                    break;
                case "13":
                    ExtractZip(directory);
                    break;
                case "14":
                    Console.WriteLine("Завершение работы программы.");
                    return;
                default:
                    Console.WriteLine("Выбранного действия не существует.");
                    break;
            }
        }
    }

    static void DisplayDriveInfo()
    {
        Console.WriteLine("Информация о логических дисках:");
        foreach (var drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady)
            {
                Console.WriteLine($"Диск: {drive.Name}");
                Console.WriteLine($"  Метка тома: {drive.VolumeLabel}");
                Console.WriteLine($"  Тип файловой системы: {drive.DriveFormat}");
                Console.WriteLine($"  Размер: {drive.TotalSize / (1024 * 1024 * 1024)} ГБ");
                Console.WriteLine($"  Доступно: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} ГБ");
            }
            else
            {
                Console.WriteLine($"Диск: {drive.Name} не готов.");
            }
        }
    }

    static void ListFiles(string directory)
    {
        Console.WriteLine("--------Чтение файлов--------");
        if (!Directory.Exists(directory))
        {
            Console.WriteLine("Директория не существует.");
            return;
        }

        var files = Directory.GetFiles(directory).Where(f => !f.EndsWith(".zip"));

        if (!files.Any())
        {
            Console.WriteLine("Нет доступных файлов.");
        }
        else
        {
            Console.WriteLine("Найденные файлы:");
            foreach (var file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }
    }

    static void WriteTxt(string directory)
    {
        Console.Write("Введите название файла: ");
        string filename = Console.ReadLine();
        Console.Write("Введите содержимое файла: ");
        string content = Console.ReadLine();

        string filePath = Path.Combine(directory, filename + ".txt");
        File.WriteAllText(filePath, content);
        Console.WriteLine($"Файл {filename}.txt успешно создан.");
    }

    static void ReadTxt(string directory)
    {
        Console.Write("Введите название файла (без расширения): ");
        string filename = Console.ReadLine();
        string filePath = Path.Combine(directory, filename + ".txt");

        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath);
            Console.WriteLine("Содержимое файла:");
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }
    }

    static void DeleteTxt(string directory)
    {
        Console.Write("Введите название файла (без расширения): ");
        string filename = Console.ReadLine();
        string filePath = Path.Combine(directory, filename + ".txt");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Console.WriteLine("Файл успешно удалён.");
        }
        else
        {
            Console.WriteLine("Файл не существует.");
        }
    }

    static void CreateJson(string directory)
    {
        Console.Write("Введите название файла: ");
        string filename = Console.ReadLine();
        var data = new { name = "John Doe", age = 30, email = "john.doe@example.com" };

        string filePath = Path.Combine(directory, filename + ".json");
        File.WriteAllText(filePath, JsonSerializer.Serialize(data));
        Console.WriteLine($"JSON файл {filename}.json успешно создан.");
    }

    static void ReadJson(string directory)
    {
        Console.Write("Введите название файла (без расширения): ");
        string filename = Console.ReadLine();
        string filePath = Path.Combine(directory, filename + ".json");

        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath);
            Console.WriteLine("Содержимое JSON файла:");
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }
    }

    static void DeleteJson(string directory)
    {
        Console.Write("Введите название файла (без расширения): ");
        string filename = Console.ReadLine();
        string filePath = Path.Combine(directory, filename + ".json");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Console.WriteLine("JSON файл успешно удалён.");
        }
        else
        {
            Console.WriteLine("Файл не существует.");
        }
    }

    static void CreateXml(string directory)
    {
        Console.Write("Введите название файла: ");
        string filename = Console.ReadLine();
        Console.Write("Введите содержимое для XML: ");
        string content = Console.ReadLine();

        string filePath = Path.Combine(directory, filename + ".xml");
        var xml = new XElement("root", new XElement("data", content));
        xml.Save(filePath);

        Console.WriteLine($"XML файл {filename}.xml успешно создан.");
    }

    static void ReadXml(string directory)
    {
        Console.Write("Введите название файла (без расширения): ");
        string filename = Console.ReadLine();
        string filePath = Path.Combine(directory, filename + ".xml");

        if (File.Exists(filePath))
        {
            var xml = XElement.Load(filePath);
            Console.WriteLine("Содержимое XML файла:");
            Console.WriteLine(xml);
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }
    }

    static void DeleteXml(string directory)
    {
        Console.Write("Введите название файла (без расширения): ");
        string filename = Console.ReadLine();
        string filePath = Path.Combine(directory, filename + ".xml");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Console.WriteLine("XML файл успешно удалён.");
        }
        else
        {
            Console.WriteLine("Файл не существует.");
        }
    }

    static void CreateZip(string directory)
    {
        Console.Write("Введите название zip-файла: ");
        string zipName = Console.ReadLine();
        string zipPath = Path.Combine(directory, zipName + ".zip");

        Console.WriteLine("Выберите файлы для добавления в архив:");
        var files = Directory.GetFiles(directory);

        for (int i = 0; i < files.Length; i++)
        {
            Console.WriteLine($"{i + 1}) {Path.GetFileName(files[i])}");
        }

        Console.Write("Введите номера файлов через пробел: ");
        var selected = Console.ReadLine()?.Split(' ').Select(int.Parse);

        using (var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
        {
            foreach (var index in selected)
            {
                var file = files[index - 1];
                zip.CreateEntryFromFile(file, Path.GetFileName(file));
                Console.WriteLine($"Файл {Path.GetFileName(file)} добавлен в архив.");
            }
        }

        Console.WriteLine($"Архив {zipName}.zip создан.");
    }

    static void ExtractZip(string directory)
    {
        Console.Write("Введите название zip-файла для разархивирования: ");
        string zipName = Console.ReadLine();
        string zipPath = Path.Combine(directory, zipName + ".zip");

        if (File.Exists(zipPath))
        {
            ZipFile.ExtractToDirectory(zipPath, directory);
            Console.WriteLine($"Архив {zipName}.zip успешно разархивирован.");
        }
        else
        {
            Console.WriteLine("Архив не найден.");
        }
    }
}
