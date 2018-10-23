using Furesoft.Proxy.Models;
using System;

namespace Furesoft.Proxy.Service
{
    public static class ServiceLocator
    {
        public static LiteDB.LiteDatabase db = new LiteDB.LiteDatabase(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\proxy.db");

    }
}