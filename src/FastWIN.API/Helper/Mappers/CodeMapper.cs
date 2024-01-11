using fastwin.Models;
using fastwin.Requests;
using Microsoft.Data.SqlClient;
using System;
using System.Data; // Add this namespace for SqlDbType

namespace fastwin.Helper.Mappers
{
    public class CodeMapper
    {
        public static Codes MapCodes(GenerateCodesRequest generateCodesRequest)
        {
            return new Codes
            {
                // for now it's empty
            };
        }
    }
}
