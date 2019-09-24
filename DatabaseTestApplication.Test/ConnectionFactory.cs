using DatabaseTestApplication.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseTestApplication.Test
{
    class ConnectionFactory : IDisposable
    {
        private bool disposedValue = false;

        public BlogDBContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<BlogDBContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database").Options;

            var context = new BlogDBContext(option);

            if(context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        public BlogDBContext CreateContextForSQLite()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var option = new DbContextOptionsBuilder<BlogDBContext>().UseSqlite(connection).Options;

            var context = new BlogDBContext(option);

            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }
    }
}
