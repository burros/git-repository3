using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAddressBook;

namespace MyLogger
{
    public enum TypeLogger
    {
        Console, File
    }
    public interface ILogger
    {
        void Info(object sender, UserEventArgs args);
        void Debug(object sender, UserEventArgs args);
        void Warning(object sender, UserEventArgs args);
        void Error(object sender, UserEventArgs args);
    }
    public sealed class ConsoleLogger:ILogger
    {
        private static readonly Lazy<ConsoleLogger> _consoleLogger = new Lazy<ConsoleLogger>(()=>new ConsoleLogger());
        public static ConsoleLogger Instance { get { return _consoleLogger.Value; } }

        private ConsoleLogger()
        {
        }

        public void Info(object sender, UserEventArgs args)
        {
            Console.WriteLine("Info message. {0}",args.Message);
        }

        public void Debug(object sender, UserEventArgs args)
        {
            Console.WriteLine("Debug message. {0}", args.Message);
        }

        public void Warning(object sender, UserEventArgs args)
        {
            Console.WriteLine("Warning message. {0}", args.Message);
        }

        public void Error(object sender, UserEventArgs args)
        {
            Console.WriteLine("Error message. {0}", args.Message);
        }
    }

    public sealed class FileLogger : ILogger
    {
        private static readonly Lazy<FileLogger> _fileLogger = new Lazy<FileLogger>(() => new FileLogger());
        private FileStream _fileStream;
        private StreamWriter _writer;
        private StreamReader _reader;
        public static FileLogger Instance { get { return _fileLogger.Value; } }

        private FileLogger()
        {
            try
            {
                // FileMode.Create for create new log file
                _fileStream = File.Open(@"Log.txt", FileMode.Create, FileAccess.Write, FileShare.Write);
            }
            catch (Exception)
            {
                Console.WriteLine("New log not create.");
                // Як варіант у блоках catch цього класу можливо згенерувати 
                // throw;
                // щоб передати виключення на наступний рівень для подальшої обробки виключення 
            }
            finally
            {
                _fileStream.Close();
            }
        }

        public void Info(object sender, UserEventArgs args)
        {
            try
            {
                _fileStream = File.Open(@"Log.txt", FileMode.Append, FileAccess.Write, FileShare.Write);
                _writer = new StreamWriter(_fileStream);
                _writer.WriteLine("Info message. {0}", args.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Data is not written to the log");
            }
            finally
            {
                // close StreamWriter and FileStream
                _writer.Close();
            }
        }

        public void Debug(object sender, UserEventArgs args)
        {
            try
            {
                _fileStream = File.Open(@"Log.txt", FileMode.Append, FileAccess.Write, FileShare.Write);
                _writer = new StreamWriter(_fileStream);
                _writer.WriteLine("Debug message. {0}", args.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Data is not written to the log");
            }
            finally
            {
                _writer.Close();
            }
        }

        public void Warning(object sender, UserEventArgs args)
        {
            try
            {
                _fileStream = File.Open(@"Log.txt", FileMode.Append, FileAccess.Write, FileShare.Write);
                _writer = new StreamWriter(_fileStream);
                _writer.WriteLine("Warning message. {0}", args.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Data is not written to the log");
            }
            finally
            {
                _writer.Close();
            }
        }

        public void Error(object sender, UserEventArgs args)
        {
            try
            {
                _fileStream = File.Open(@"Log.txt", FileMode.Append, FileAccess.Write, FileShare.Write);
                _writer = new StreamWriter(_fileStream);
                _writer.WriteLine("Error message. {0}", args.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Data is not written to the log");
            }
            finally
            {
                _writer.Close();
            }
        }

        public void ShowLogMessages()
        {

            try
            {
                _fileStream = File.Open(@"Log.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
                _reader = new StreamReader(_fileStream);
                Console.WriteLine(_reader.ReadToEnd());
            }
            catch (Exception)
            {
                Console.WriteLine("Error open log.");
            }
            finally
            {
                _reader.Close();
            }
        }
    }
}
