using Furesoft.Proxy.Services.Interfaces;
using LiteDB;
using System;

namespace Furesoft.Proxy.Services
{
    public class DatabaseService : IDatabaseService
    {
        public LiteDatabase Database { get; set; } = new LiteDatabase(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\furesoft.proxy.data.db");

        public LiteCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public LiteDatabase GetDatabase()
        {
            return Database;
        }
    }
}