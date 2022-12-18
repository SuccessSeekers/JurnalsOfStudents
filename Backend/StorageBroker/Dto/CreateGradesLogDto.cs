using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageBroker.Models;

namespace StorageBroker.Dto
{
    public class CreateGradesLogDto
    {
        public TypeOfGrade Grade { get; set; }
        public int StudentId { get; set; }
        public int GroupId { get; set; }
    }
}