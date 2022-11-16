using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opposition_Generateur.Models
{
    [Serializable]
    public class Gazette
    {
        public string Num_pub { get; set; }
        public DateTime Date { get; set; }
    }
}