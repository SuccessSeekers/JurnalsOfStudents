using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StorageBroker.Models;

public class StudentGroup
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    [ForeignKey(nameof(StudentId))] public Student Student { get; set; }
    public int GroupId { get; set; }
    [ForeignKey(nameof(GroupId))] public Group Group { get; set; }
}