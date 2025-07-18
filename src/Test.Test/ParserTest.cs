using Newtonsoft.Json.Linq;
using Test.Classes;
using Test.Core.Extensions;
using Test.Core.Extensions.Parsers;
using Test.Core.Repositories.Implementations;
using Xunit.Abstractions;

namespace Test.Test
{
    public class ParserTest
    {
        private readonly ITestOutputHelper _output;
        private readonly string _centroidsPath;
        private readonly string _fieldsPath;

        public ParserTest(ITestOutputHelper output)
        {
            _output = output;
            string rootPath = ProjectRootPath.GetProjectRootPath();
            _centroidsPath = Path.Combine(rootPath, "Points", "centroids.kml");
            _fieldsPath = Path.Combine(rootPath, "Points", "fields.kml");
        }

        [Fact]
        public async void TestPrintAllFieldsInfo()
        {
            FileKmlParser parser = new FileKmlParser();

            List<Field> fields = await parser.Parse(_centroidsPath, _fieldsPath);
            
            foreach (var field in fields)
            {
                _output.WriteLine($"Id: {field.Id}");
                _output.WriteLine($"Name: {field.Name}");
                _output.WriteLine($"Size: {field.Size:F2}");

                _output.WriteLine("Locations:");
                _output.WriteLine($"\tCenter: lat = {field.Location.Center.Latitude}, lng = {field.Location.Center.Longitude}");

                _output.WriteLine("\tPolygon:");
                foreach (var pt in field.Location.Polygon)
                {
                    _output.WriteLine($"\t\t- lat = {pt.Latitude}, lng = {pt.Longitude}");
                }

                _output.WriteLine(new string('-', 50));
            }
        }

        /// <summary>
        /// Дистанция от левого верхнего угла до центра первого поля примерно 1 километр
        /// </summary>
        [Fact]
        public async void TestDistance1KM()
        {
            FieldRepository repository = new(); 

            Field? field = await repository.GetByIdAsync(1);
            Point point = new Point()
            {
                Latitude = 41.33468,
                Longitude = 45.7074
            };

            double distance = DistanceCalculator.Calculate(point, field.Location.Center);

            Assert.Equal(1000, Math.Floor(distance / 1000) * 1000);
        }

        /// <summary>
        /// Поля с индентификатором -1 не существует
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task NullExceptionTestDistance()
        {
            FieldRepository repository = new();

            Field? field = await repository.GetByIdAsync(-1);
            Point point = new Point()
            {
                Latitude = 41.33468,
                Longitude = 45.7074
            };

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                double distance = DistanceCalculator.Calculate(point, field.Location.Center);
            });
        }

        [Fact]
        public async Task TestPointInside1Id()
        {
            FieldRepository repository = new();

            List<Field>? fields = await repository.GetAllAsync();
            Point point = new Point()
            {
                Latitude = 41.33473,
                Longitude = 45.68522
            };

            var field = InsideChecker.Check(point, fields);

            Assert.Equal(1, field.Id);
        }

        [Fact]
        public async Task TestPointNotInside()
        {
            FieldRepository repository = new();

            List<Field>? fields = await repository.GetAllAsync();
            Point point = new Point()
            {
                Latitude = 100,
                Longitude = 100
            };

            var field = InsideChecker.Check(point, fields);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                Assert.Equal(1, field.Id);
            });
        }

    }
}
