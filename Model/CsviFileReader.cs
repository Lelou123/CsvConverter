using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CvsConverter.Model;

public class CsvFileReader<T> where T : class
{
    private readonly string[] _expectedColumns;

    public CsvFileReader(string[] expectedColumns)
    {
        _expectedColumns = expectedColumns;
    }

    public async Task<List<T>> ReadCsvFileAsync(IFormFile file)
    {
        var resultList = new List<T>();
        string[] allowedExtension = new string[] { ".csv" };
        if (!allowedExtension.Contains(Path.GetExtension(file.FileName)))
            throw new Exception("Extensao Invalida");

        try
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            { MissingFieldFound = null }))
            {
                csv.Read();
                csv.ReadHeader();

                if (!csv.HeaderRecord.SequenceEqual(_expectedColumns))
                {
                    throw new Exception("As colunas do arquivo não estão na ordem esperada");
                }

                while (csv.Read())
                {
                    var record = csv.GetRecord<T>();
                    resultList.Add(record);
                }
            }

            return resultList;
        }
        catch (Exception ex)
        {
            throw new Exception($"Falha ao criar a lista de objetos a partir do arquivo CSV: {ex.Message}");
        }
    }
}
