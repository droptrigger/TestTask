using Test.Classes;
using Test.Core.Extensions;
using Test.Core.Extensions.Parsers;

namespace Test.Core.Services
{
    public class FieldService
    {
        private readonly FileKmlParser _parser;
        private readonly string _centroidsPath;
        private readonly string _fieldsPath;

        public FieldService() 
        { 
            _parser = new FileKmlParser();
            string rootPath = ProjectRootPath.GetProjectRootPath();
            _centroidsPath = Path.Combine(rootPath, "Points", "centroids.kml");
            _fieldsPath = Path.Combine(rootPath, "Points", "fields.kml");

        }

        public async Task<List<Field>> GetAllFieldsAsync() 
        {
            return await _parser.Parse(_centroidsPath, _fieldsPath);
        }

        public async Task<double> GetSizeWithIdAsync(int id)
        {
            var fields = await _parser.Parse(_centroidsPath, _fieldsPath);

            var result = fields.FirstOrDefault(f => f.Id == id);

            if (result is null)
                return 0.0;

            return result.Size;
        }

    }
}
