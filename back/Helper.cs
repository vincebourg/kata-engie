using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

internal static class Helpers
{
    internal static FeatureCollection Deserialize(string jsonText)
    {
        var serializer = GeoJsonSerializer.Create();
        using var stringReader = new StringReader(jsonText);
        using var jsonReader = new JsonTextReader(stringReader);
        var featureCollection = serializer.Deserialize<FeatureCollection>(jsonReader);
        return featureCollection ?? throw new InvalidOperationException("FeatureCollection is null");
    }

    internal static string Serialize(FeatureCollection featureCollection)
    {
        var serializer = GeoJsonSerializer.Create();
        using var stringWriter = new StringWriter();
        using var jsonWriter = new JsonTextWriter(stringWriter);
        serializer.Serialize(jsonWriter, featureCollection);
        return stringWriter.ToString();
    }

    internal static double? ComputeArea(Geometry geometry)
    {
        var transformedGeometry = Transform(geometry);

        return transformedGeometry?.Area;
    }

    private static Geometry? Transform(Geometry geometry)
    {
        var utm = ProjectedCoordinateSystem.WGS84_UTM(UtmZone(geometry.Centroid.X), geometry.Centroid.Y >= 0);
        var geometryFactory = new GeometryFactory(new PrecisionModel(1000), (int)utm.AuthorityCode);
        var transformer = new CoordinateTransformationFactory().CreateFromCoordinateSystems(GeographicCoordinateSystem.WGS84, utm);

        if (geometry is Polygon polygon)
        {
            return TransformPolygon(polygon, transformer, geometryFactory);
        }

        if (geometry is MultiPolygon multiPolygon)
        {
            return TransformMultiPolygon(multiPolygon, transformer, geometryFactory);
        }

        return null;
    }

    private static Polygon TransformPolygon(Polygon polygon, ICoordinateTransformation transformer, GeometryFactory geometryFactory)
    {
        var projectedShell = TransformLinearRing(polygon.Shell, transformer, geometryFactory);
        var projectedHoles = polygon.Holes.Select(h => TransformLinearRing(h, transformer, geometryFactory)
        ).ToArray();

        var projectedPolygon = geometryFactory.CreatePolygon(projectedShell, projectedHoles);
        return projectedPolygon;
    }

    private static MultiPolygon TransformMultiPolygon(MultiPolygon multiPolygon, ICoordinateTransformation transformer, GeometryFactory geometryFactory)
    {
        var projectedPolygons = multiPolygon.Geometries.Select(mp => TransformPolygon((Polygon)mp, transformer, geometryFactory)).ToArray();

        var projectedMultiPolygon = geometryFactory.CreateMultiPolygon(projectedPolygons);
        return projectedMultiPolygon;
    }

    public static LinearRing TransformLinearRing(LinearRing ring, ICoordinateTransformation transformer, GeometryFactory geometryFactory)
    {
        var coordinates = ring.Coordinates
            .Select(c =>
            {
                var (x, y) = transformer.MathTransform.Transform(c.X, c.Y);
                return new Coordinate(x, y);
            })
            .ToArray();
        return geometryFactory.CreateLinearRing(coordinates);
    }

    private static int UtmZone(double longitude)
    {
        return (int)((longitude + 180d) / 6d + 1d);
    }
}