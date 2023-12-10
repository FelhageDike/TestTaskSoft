using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DAL.DbModels;

public class TestModel
{
    public Guid Id { get; set; }
    public int RandomInt { get; set; }
}