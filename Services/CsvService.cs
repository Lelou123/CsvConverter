using CsvConverter.Model;
using CsvConverter.Model.Interface;
using CvsConverter.Model;
using FluentResults;

namespace CsvConverter.Services
{
    public class CsvService : ICsvService
    {
        
        public CsvService() { }


        public async Task<Result<List<Car>>> CreateCsvObject(IFormFile file)
        {
            try
            {
                // Ordem esperada das colunas
                string[] expectedColumns = { "Brand", "Model", "Color" };

                CsvFileReader<Car> csvFileReader = new(expectedColumns);

                var result = await csvFileReader.ReadCsvFileAsync(file);

                if (result == null)
                {
                    return Result.Fail("Can't convert csv file");
                }
                else
                {
                    return Result.Ok(result);
                }                    
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
