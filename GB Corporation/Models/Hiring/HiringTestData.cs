namespace GB_Corporation.Models
{
    public class HiringTestData : BaseEntity
    {
        public int? ForeignLanguageTestId { get; set; }
        public int? LogicTestId { get; set; }
        public int? ProgrammingTestId { get; set; }
        public int HiringDataId { get; set; }
        public ApplicantForeignLanguageTest ForeignLanguageTest { get; set; }
        public ApplicantLogicTest LogicTest { get; set; }
        public ApplicantProgrammingTest ProgrammingTest { get; set; }
        public HiringData HiringData { get; set; }
    }
}
