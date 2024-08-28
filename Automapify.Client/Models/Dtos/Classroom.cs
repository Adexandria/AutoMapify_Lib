using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapify.Client.Models.Dtos
{
    public class Classroom
    {
        public Classroom()
        {
                
        }
        public Classroom(string room)
        {
            Room = room;
        }
        public string Room { get; set; }    
    }
}
