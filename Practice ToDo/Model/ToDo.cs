using SQLite;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_ToDo
{
    public class ToDo
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }

        public DateTime Deadline { get; set; }

        public bool Done { get; set; }

        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }

        public string Priority { get; set; }
    }
}

