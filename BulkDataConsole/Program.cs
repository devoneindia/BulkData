using LoadDataProject.DbContexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BulkDataConsole;

public class Program
{
    public static void Main(string[] args)
    {
        using (var dbContext = new BulkDbContext())
        {
            string filename = "D:\\PipeDelimitedFile\\PubAccEM.text";

            //Console.WriteLine("Enter the file path:");
            //string filename = Console.ReadLine();

            InserData(dbContext, filename);
        }
    }

    private static void InserData(BulkDbContext dbContext, string filename)
    {
        if (File.Exists(filename))
        {
            string[] ReadFile = File.ReadAllLines(filename);

            foreach (string line in ReadFile)
            {
                string[] values = line.Split('|');

                var columRecordType = "record_type";
                var columnrecordvlaue = new NpgsqlParameter("columnrecordvlaue", values[0]);

                var colUSIdRecordType = "unique_system_identifier";
                var colusidrecordvalue = new NpgsqlParameter("colusidrecordvalue", ParseInt(values[1]));

                var colUlsFileRecordType = "uls_file_number";
                var colulsfilerecordvalue = new NpgsqlParameter("colulsfilerecordvalue", values[2]);

                var colEbfRecordType = "ebf_number";
                var colebfrecordvalue = new NpgsqlParameter("colebfrecordvalue", values[3]);

                var colCallSignRecordType = "call_sign";
                var colcallsignrecordvalue = new NpgsqlParameter("colcallsignrecordvalue", values[4]);

                var colLocationRecordType = "location_number";
                var collocationrecordvalue = new NpgsqlParameter("collocationrecordvalue", ParseInt(values[5]));

                var colAntenaNumberRecordType = "antenna_number";
                var colantenanumberrecordvalue = new NpgsqlParameter("colantenanumberrecordvalue", ParseInt(values[6]));

                var colFrequencyRecordType = "frequency_assigned";
                var colfrequencyrecordvalue = new NpgsqlParameter("colfrequencyrecordvalue", (decimal?)ParseDouble(values[7]));

                var colEActionRecordType = "emission_action_performed";
                var colemmactionrecordvalue = new NpgsqlParameter("colemmactionrecordvalue", values[8]);

                var colECodeRecordType = "emission_code";
                var colcodeerecordvalue = new NpgsqlParameter("colcodeerecordvalue", values[9]);

                var colDMTypeRecordType = "digital_mod_rate";
                var coldmtyperecordvalue = new NpgsqlParameter("coldmtyperecordvalue", ParseInt(values[10]));

                var colDMRateRecordType = "digital_mod_type";
                var coldmraterecordvalue = new NpgsqlParameter("coldmraterecordvalue", values[11]);

                var colFrequencyNumRecordType = "frequency_number";
                var colfrequencynumrecordvalue = new NpgsqlParameter("colfrequencynumrecordvalue", ParseInt(values[12]));

                var colStatusCodeRecordType = "status_code";
                var colstatuscoderecordvalue = new NpgsqlParameter("colstatuscoderecordvalue", values[13]);

                var colStatusDateRecordType = "status_date";
                var colstatusdaterecordvalue = new NpgsqlParameter("colstatusdaterecordvalue", ParseDateTime(values[14]));

                var colESIdRecordType = "emission_sequence_id";
                var colesidrecordvalue = new NpgsqlParameter("colesidrecordvalue", ParseInt(values[15]));


                var publicEms = dbContext.Database.ExecuteSqlRaw($"INSERT INTO main.pubacc_em ({columRecordType}, {colUSIdRecordType},{colUlsFileRecordType}, {colEbfRecordType}, {colCallSignRecordType}, " +
                 $"{colLocationRecordType}, {colAntenaNumberRecordType},{colFrequencyRecordType},{colEActionRecordType},{colECodeRecordType},{colDMTypeRecordType},{colDMRateRecordType},{colFrequencyNumRecordType}," +
                 $"{colStatusCodeRecordType},{colStatusDateRecordType},{colESIdRecordType})" +
                 $" VALUES (@columnrecordvlaue," +
                 $"@colusidrecordvalue,@colulsfilerecordvalue, @colebfrecordvalue, @colcallsignrecordvalue,@collocationrecordvalue, " +
                 $"@colantenanumberrecordvalue, @colfrequencyrecordvalue, @colemmactionrecordvalue, @colcodeerecordvalue, @coldmtyperecordvalue, @coldmraterecordvalue,@colfrequencynumrecordvalue," +
                 $"@colstatuscoderecordvalue, @colstatusdaterecordvalue, @colesidrecordvalue )",
                 columnrecordvlaue, colusidrecordvalue, colulsfilerecordvalue, colebfrecordvalue, colcallsignrecordvalue,
                 collocationrecordvalue, colantenanumberrecordvalue, colfrequencyrecordvalue, colemmactionrecordvalue, colcodeerecordvalue, coldmtyperecordvalue, coldmraterecordvalue,
                 colfrequencynumrecordvalue, colstatuscoderecordvalue, colstatusdaterecordvalue, colesidrecordvalue);

            }
        }
        else
        {
            Console.WriteLine("File not found: " + filename);
        }
    }

    private static int ParseInt(string value)
    {
        int result;
        return int.TryParse(value, out result) ? result : 0;
    }

    private static double ParseDouble(string value)
    {
        double result;
        return double.TryParse(value, out result) ? result : 0.0;
    }

    private static DateTime ParseDateTime(string value)
    {
        DateTime result;
        return DateTime.TryParse(value, out result) ? result : DateTime.MinValue;
    }
    private static decimal ParseDecimal(string value)
    {
        decimal result;
        return decimal.TryParse(value, out result) ? result : 0.0m;
    }
}
