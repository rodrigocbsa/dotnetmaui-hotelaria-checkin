using CsvHelper;
using Hospede.Models;
using Hospede.Paths;
using Net.Codecrete.QrCodeGenerator;
using System.Globalization;

namespace Hospede.Methods;

internal static class CSVMethods
{
    internal static void DeFichaParaCSV(Ficha ficha)
    {
        List<Ficha> records = new()
        {
            ficha
        };

        using var writer = new StreamWriter(SharedApplicationPaths.CsvPath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(records);
    }

    internal static Ficha DeCSVParaFicha()
    {
        using var reader = new StreamReader(SharedApplicationPaths.CsvPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<Ficha>();

        records = new List<Ficha>();
        return records.FirstOrDefault();
    }

    internal static string DeCSVParaString()
    {
        return File.ReadAllText(SharedApplicationPaths.CsvPath);
    }

    public static void GenerateQrCodeFromCSVData()
    {
        var qr = QrCode.EncodeText(DeCSVParaString(), QrCode.Ecc.Low);
        qr.SaveAsPng(SharedApplicationPaths.QrPath, 10, 4);
    }
}
