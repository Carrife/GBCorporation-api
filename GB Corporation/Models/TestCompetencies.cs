﻿namespace GB_Corporation.Models
{
    public class TestCompetencies : BaseEntity
    {
        public string Title { get; set; }
        public int? TestResult { get; set; }
        public int EmployeeId { get; set; }
        public int StatusId { get; set; }
        public Employee Employee { get; set; }
        public SuperDictionary Status { get; set; }
        public DateTime? Date { get; set; }
    }
}
