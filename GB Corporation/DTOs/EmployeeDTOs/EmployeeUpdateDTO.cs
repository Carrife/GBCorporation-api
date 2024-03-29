﻿namespace GB_Corporation.DTOs
{
    public class EmployeeUpdateDTO : BaseDTO
    {
        public string NameRu { get; set; }
        public string SurnameRu { get; set; }
        public string PatronymicRu { get; set; }
        public string NameEn { get; set; }
        public string SurnameEn { get; set; }
        public string? WorkPhone { get; set; }
        public string Phone { get; set; }
        public int? LanguageId { get; set; }
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }
    }
}
