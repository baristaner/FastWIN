﻿namespace fastwin.Requests
{
    public class GenerateCodesRequest
    {
        public int NumOfCodes { get; set; } = 50;
        public string CharacterSet { get; set; } = "13456789ACDEFHKLMNPQRTVWXYZ";
        public int ExpirationMonths { get; set; } = 3;
        public DateTime? ExpirationDate { get; set; } = null;
    }
}
