using System;
using System.Collections.Generic;
using System.IO;
using BulkDataConsole.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BulkDataConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var dbContext = new PubAccDbContext())
            {
                String filePath = "C:\\src\\BulkData\\PubAccEM.txt";
                List<string> inputLines = File.ReadAllLines(filePath).ToList();


                if (inputLines != null && inputLines.Count > 0)
                {
                    InserData(dbContext, inputLines);
                    Console.WriteLine($"File exists and writing to the database. {inputLines.Count} records written.");
                }
                else
                {
                    Console.WriteLine("No file found.");
                }

                InserData(dbContext, inputLines);
            }
        }
    
        public static void InserData(PubAccDbContext dbContext, List<string> inputLines)
        {
                foreach (var line in inputLines)
                {
                    string[] values = line.Split('|');
                    var parameters = GetParameters(values);

                    dbContext.Database.ExecuteSqlRaw(
                        "INSERT INTO main.pubacc_em (record_type, unique_system_identifier, uls_file_number, ebf_number, " +
                        "call_sign, location_number, antenna_number, frequency_assigned, emission_action_performed, emission_code, " +
                        "digital_mod_rate, digital_mod_type, frequency_number, status_code, status_date, emission_sequence_id) " +
                        "VALUES (@recordType, @usId, @ulsFile, @ebf, @callSign, @location, @antenna, @frequency, @emissionAction, " +
                        "@emissionCode, @modRate, @modType, @frequencyNum, @statusCode, @statusDate, @emissionId)",
                        parameters.ToArray()
                    );     
                }
        }

        private static List<NpgsqlParameter> GetParameters(string[] values)
        {
            var parameterNames = new[]
            {
                "recordType", "usId", "ulsFile", "ebf", "callSign", "location", "antenna", "frequency",
                "emissionAction", "emissionCode", "modRate", "modType", "frequencyNum", "statusCode",
                "statusDate", "emissionId"
            };

            var parameters = new List<NpgsqlParameter>();

            for (int i = 0; i < parameterNames.Length; i++)
            {
                if (parameterNames[i] == "modRate")
                {
                    parameters.Add(new NpgsqlParameter(parameterNames[i],
                        Convert.ToInt32(
                            string.IsNullOrWhiteSpace(values[i]) ? "0" : values[i])
                            )
                        );
                }
                else if (parameterNames[i] == "statusDate")
                {
                    parameters.Add(new NpgsqlParameter(parameterNames[i],
                        Convert.ToDateTime(
                            string.IsNullOrWhiteSpace(values[i]) ? DateTime.MinValue : values[i])
                            )
                        );
                }
                else
                {
                    parameters.Add(new NpgsqlParameter(parameterNames[i], GetParameterValue(values[i])));
                }

            }

            return parameters;
        }

        private static object GetParameterValue(string value)
        {
            if (int.TryParse(value, out int intValue)) return intValue;
            if (double.TryParse(value, out double doubleValue)) return doubleValue;
            if (DateTime.TryParse(value, out DateTime dateValue)) return dateValue;
            if (decimal.TryParse(value, out decimal decimalValue)) return decimalValue;

            return value;
        }
    }
}
