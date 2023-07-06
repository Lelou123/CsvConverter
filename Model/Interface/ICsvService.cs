using FluentResults;

namespace CsvConverter.Model.Interface
{
    public interface ICsvService
    {
        Task<Result<List<Car>>> CreateCsvObject(IFormFile file);
    }
}
