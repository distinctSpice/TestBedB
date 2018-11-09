using CensusProcessing.Enums;

namespace CensusProcessing.Models
{
    public class Census
    {
        public string EffectiveDate { get; set; }
        public string DeletionDate { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public ProcessType ProcessType { get; set; }
    }
}
