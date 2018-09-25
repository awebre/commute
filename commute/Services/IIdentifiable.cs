using System;
using SQLite;

namespace commute.Services
{
    public interface IIdentifiable
    {
        [PrimaryKey, AutoIncrement]
        int Id { get; set; }
    }
}
